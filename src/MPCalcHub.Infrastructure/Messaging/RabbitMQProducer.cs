using Microsoft.Extensions.Logging;
using MPCalcHub.Domain.Interfaces.Infrastructure;
using RabbitMQ.Client;
using System.Text;

namespace MPCalcHub.Infrastructure.Messaging
{
    public class RabbitMQProducer : IRabbitMQProducer, IDisposable
    {
        private readonly IRabbitMQConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMQProducer> _logger;
        private IConnection _connection;
        private bool _disposed;

        public RabbitMQProducer(IRabbitMQConnectionFactory connectionFactory, ILogger<RabbitMQProducer> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task PublishAsync(string queueName, object message, CancellationToken cancellationToken = default)
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
                    _logger.LogDebug("Nova conexão RabbitMQ criada para publicação. IsOpen: {_connection.IsOpen}", _connection.IsOpen);
                }

                using var channel = await _connection.CreateChannelAsync(null, cancellationToken);
                if (channel == null)
                {
                    throw new InvalidOperationException("Falha ao criar canal RabbitMQ.");
                }

                _logger.LogDebug("Canal RabbitMQ criado com sucesso para fila {QueueName}", queueName);

                await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = System.Text.Json.JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(exchange: "Contact", routingKey: string.Empty, body: body);

                _logger.LogInformation("Mensagem publicada na fila {QueueName}: {@Message}", queueName, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao publicar mensagem na fila RabbitMQ: {Message}", ex.Message);
                throw;
            }
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