using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MPCalcHub.Domain.Entities;
using MPCalcHub.Domain.Interfaces;
using MPCalcHub.Domain.Interfaces.Infrastructure;
using MPCalcHub.Infrastructure.Messaging;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EN = MPCalcHub.Domain.Entities;


namespace MPCalcHub.Infrastructure.Messaging
{
    public class QueueBackgroundService : BackgroundService, IQueueBackgroundService
    {
        private readonly ILogger<QueueBackgroundService> _logger;
        private readonly IRabbitMQProducer _rabbitMQProducer;
        private readonly IRabbitMQConsumer _rabbitMQConsumer;
        private readonly IContactService _contactService;

        public QueueBackgroundService(
            IContactService contactService,
            ILogger<QueueBackgroundService> logger,
            IRabbitMQProducer rabbitMQProducer,
            IRabbitMQConsumer rabbitMQConsumer)
        {
            _logger = logger;
            _rabbitMQProducer = rabbitMQProducer;
            _rabbitMQConsumer = rabbitMQConsumer;
            _contactService = contactService;
        }

        public async Task EnqueueContactUpdateAsync(Contact message, CancellationToken cancellationToken = default)
        {
            try
            {
                var contactMessage = new
                {
                    ContactId = message.Id,
                    Name = message.Name,
                    Email = message.Email,
                    Phone = message.PhoneNumber,
                    DDD = message.DDD
                };
                await _rabbitMQProducer.PublishAsync("contact-updates", contactMessage, cancellationToken);
                _logger.LogInformation("Mensagem de atualização de contato enviada para a fila: {@Message}", contactMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar mensagem de contato para a fila RabbitMQ: {Message}", ex.Message);
                throw;
            }
        }

        public async Task EnqueueContactInsertAsync(Contact message, CancellationToken cancellationToken = default)
        {
            try
            {
                var contactMessage = new
                {
                    ContactId = message.Id,
                    Name = message.Name,
                    Email = message.Email,
                    Phone = message.PhoneNumber,
                    DDD = message.DDD
                };
                await _rabbitMQProducer.PublishAsync("contact-inserts", contactMessage, cancellationToken);
                _logger.LogInformation("Mensagem de criação de contato enviada para a fila: {@Message}", contactMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar mensagem de contato para a fila RabbitMQ: {Message}", ex.Message);
                throw;
            }
        }

        public void StartConsumingContactUpdates()
        {
            _rabbitMQConsumer.StartConsuming("contact-updates", ProcessContactUpdateAsync, CancellationToken.None);
        }

        public void StartConsumingContactInserts()
        {
            _rabbitMQConsumer.StartConsuming("contact-inserts", ProcessContactInsertAsync, CancellationToken.None);
        }

        private async Task ProcessContactUpdateAsync(string message)
        {
            try
            {
                var data = JsonSerializer.Deserialize<dynamic>(message);
                var contactId = data.ContactId.ToString();

                if (Guid.TryParse(contactId, out Guid parsedContactId))
                {
                    var contact = await _contactService.GetById(parsedContactId, include: false, tracking: true);
                    if (contact != null)
                    {
                        await _contactService.Update(contact);
                        _logger.LogInformation("Contato atualizado com sucesso: {@Contact}", contact);
                    }
                    else
                    {
                        _logger.LogWarning("Contato não encontrado para o ID {ContactId}", new { ContactId = contactId });
                    }
                }
                else
                {
                    _logger.LogWarning("ID de contato inválido na mensagem: {ContactId}", new { ContactId = contactId });
                }
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro ao deserializar mensagem de atualização de contato: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem de atualização de contato: {Message}", ex.Message);
                throw;
            }
        }

        private async Task ProcessContactInsertAsync(string message)
        {
            try
            {
                var data = JsonSerializer.Deserialize<dynamic>(message);
                var contactId = data.Id?.ToString();
                var name = data.Name?.ToString();
                var email = data.Email?.ToString();

                if (Guid.TryParse(contactId, out Guid parsedContactId))
                {
                    var existingContact = await _contactService.GetById(parsedContactId, include: false, tracking: false);
                    if (existingContact != null)
                    {
                        _logger.LogWarning("Contato já existe para o ID {ContactId}, não será adicionado novamente.", new { ContactId = contactId });
                        return;
                    }

                    var newContact = new EN.Contact
                    {
                        Id = parsedContactId,
                        Name = name,
                        Email = email
                    };

                    await _contactService.Add(newContact);
                    _logger.LogInformation("Contato adicionado com sucesso: {@Contact}", newContact);
                }
                else
                {
                    _logger.LogWarning("ID de contato inválido na mensagem: {ContactId}", new { ContactId = contactId });
                }
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro ao deserializar mensagem de adição de contato: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem de adição de contato: {Message}", ex.Message);
                throw;
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Serviço de fila de contatos RabbitMQ iniciado.");

            // Inicia o consumo de mensagens
            StartConsumingContactUpdates();

            // Mantém o serviço ativo até o cancellation token ser solicitado
            return Task.CompletedTask;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando serviço de fila de contatos RabbitMQ...");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Parando serviço de fila de contatos RabbitMQ...");
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
            (_rabbitMQConsumer as IDisposable)?.Dispose();
            (_rabbitMQProducer as IDisposable)?.Dispose();
        }
    }
}