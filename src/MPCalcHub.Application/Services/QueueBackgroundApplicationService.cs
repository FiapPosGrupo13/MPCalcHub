using AutoMapper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MPCalcHub.Application.DataTransferObjects;
using MPCalcHub.Application.Interfaces;
using MPCalcHub.Domain.Interfaces;
using MPCalcHub.Infrastructure.Messaging;
using System.Text.Json;
using EN = MPCalcHub.Domain.Entities;

namespace MPCalcHub.Application.Services
{
    public class QueueBackgroundApplicationService(IContactService contactService, IQueueBackgroundService queueBackgroundService, IMapper mapper) : BackgroundService, IQueueBackgroundApplicationService
    {
        private readonly ILogger<QueueBackgroundApplicationService> _logger;
        private readonly IQueueBackgroundService _queueBackgroundService = queueBackgroundService;
        private readonly IContactService _contactService = contactService;
        private readonly IMapper _mapper = mapper;

        public async Task<Contact> EnqueueContactUpdateAsync(Contact model, CancellationToken cancellationToken = default)
        {
            try
            {
                var contact = await _contactService.GetById(model.Id.Value, include: false, tracking: true);
                if (contact == null)
                    throw new Exception("O contato não existe.");

                _mapper.Map(model, contact);

                await _queueBackgroundService.EnqueueContactUpdateAsync(contact, cancellationToken);
                _logger.LogInformation("Mensagem de atualização de contato orquestrada para a fila: {@Contact}", model);

                return _mapper.Map<Contact>(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao orquestrar mensagem de contato para a fila RabbitMQ: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<Contact> EnqueueContactInsertAsync(BasicContact model, CancellationToken cancellationToken = default)
        {
            try
            {
                var contact = _mapper.Map<EN.Contact>(model);

                await _queueBackgroundService.EnqueueContactInsertAsync(contact, cancellationToken);
                _logger.LogInformation("Mensagem de atualização de contato orquestrada para a fila: {@Contact}", contact);

                return _mapper.Map<Contact>(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao orquestrar mensagem de contato para a fila RabbitMQ: {Message}", ex.Message);
                throw;
            }
        }

        public void StartConsumingContactUpdates()
        {
            _queueBackgroundService.StartConsumingContactUpdates();
            _logger.LogInformation("Consumo de atualizações de contato iniciado.");
        }

        public void StartConsumingContactInserts()
        {
            _queueBackgroundService.StartConsumingContactInserts();
            _logger.LogInformation("Consumo de criações de contato iniciado.");
        }        

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Serviço de orquestração de fila de contatos RabbitMQ iniciado.");

            StartConsumingContactUpdates();

            return Task.CompletedTask;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando serviço de orquestração de fila de contatos RabbitMQ...");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Parando serviço de orquestração de fila de contatos RabbitMQ...");
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
            (_queueBackgroundService as IDisposable)?.Dispose();
        }
    }
}