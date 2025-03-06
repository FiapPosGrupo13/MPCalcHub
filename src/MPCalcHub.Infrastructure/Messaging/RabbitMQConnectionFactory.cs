using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MPCalcHub.Domain.Interfaces.Infrastructure;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MPCalcHub.Infrastructure.Messaging
{
    public class RabbitMQConnectionFactory : IRabbitMQConnectionFactory, IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMQConnectionFactory> _logger;
        private bool _disposed;

        public RabbitMQConnectionFactory(IConfiguration configuration, ILogger<RabbitMQConnectionFactory> logger)
        {
            _logger = logger;
            _connectionFactory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:Host"] ?? "localhost",
                UserName = configuration["RabbitMQ:Username"] ?? "guest",
                Password = configuration["RabbitMQ:Password"] ?? "guest",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };
        }

        public async Task<IConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                IConnection connection = await Task.Run(() => _connectionFactory.CreateConnectionAsync(), cancellationToken);
                if (connection == null || !connection.IsOpen)
                {
                    throw new InvalidOperationException("Falha ao criar conexão RabbitMQ: conexão não está aberta.");
                }
                _logger.LogDebug("Conexão RabbitMQ criada com sucesso. IsOpen: {IsOpen}", connection.IsOpen);
                return connection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar conexão RabbitMQ: {Message}", ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}