using Microsoft.Extensions.Logging;
using MPCalcHub.Domain.Interfaces.Infrastructure;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MPCalcHub.Infrastructure.Messaging
{
    public class RabbitMQConsumer : IRabbitMQConsumer, IDisposable
    {
        private readonly IRabbitMQConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private IConnection _connection;
        private bool _disposed;

        public RabbitMQConsumer(IRabbitMQConnectionFactory connectionFactory, ILogger<RabbitMQConsumer> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public void StartConsuming(string queueName, Func<string, Task> messageHandler, CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                try
                {
                    if (_connection == null || !_connection.IsOpen)
                    {
                        _connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);
                        if (_connection == null || !_connection.IsOpen)
                        {
                            throw new InvalidOperationException("Conexão RabbitMQ não está aberta ou inicializada.");
                        }
                        _logger.LogDebug("Nova conexão RabbitMQ criada para consumo. IsOpen: {_connection.IsOpen}", _connection.IsOpen);
                    }

                    using var channel = await _connection.CreateChannelAsync(null, cancellationToken);
                    if (channel == null)
                    {
                        throw new InvalidOperationException("Falha ao criar canal RabbitMQ.");
                    }

                    _logger.LogDebug("Canal RabbitMQ criado com sucesso para fila {QueueName}", queueName);

                    await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.ReceivedAsync += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        try
                        {
                            await messageHandler(message);
                            await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Erro ao processar mensagem da fila {QueueName}: {Message}", queueName, ex.Message);
                            await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
                        }
                    };

                    await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);

                    cancellationToken.Register(() =>
                    {
                        channel.CloseAsync();
                        channel.Dispose();
                    });

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        await Task.Delay(1000, cancellationToken); // Pequena pausa para evitar uso excessivo de CPU
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao iniciar consumo na fila {QueueName}: {Message}", queueName, ex.Message);
                    throw;
                }
            }, cancellationToken);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _connection?.Dispose();
                _disposed = true;
            }
        }
    }
}