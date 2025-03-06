using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPCalcHub.Domain.Interfaces.Infrastructure
{
    public interface IRabbitMQConsumer
    {
        void StartConsuming(string queueName, Func<string, Task> messageHandler, CancellationToken cancellationToken);

    }
}
