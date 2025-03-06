using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPCalcHub.Domain.Interfaces.Infrastructure
{
    public interface IRabbitMQConnectionFactory
    {
        Task<IConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);

    }
}
