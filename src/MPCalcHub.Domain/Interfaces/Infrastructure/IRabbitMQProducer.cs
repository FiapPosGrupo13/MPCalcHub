using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPCalcHub.Domain.Interfaces.Infrastructure
{
    public interface IRabbitMQProducer
    {
        Task PublishAsync(string queueName, object message, CancellationToken cancellationToken = default);

    }
}
