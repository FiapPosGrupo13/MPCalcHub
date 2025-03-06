using MPCalcHub.Domain.Entities;

namespace MPCalcHub.Domain.Interfaces
{
    public interface IQueueBackgroundService
    {
        Task EnqueueContactUpdateAsync(Contact message, CancellationToken cancellationToken = default);
        Task EnqueueContactInsertAsync(Contact message, CancellationToken cancellationToken = default);
        void StartConsumingContactUpdates();
        void StartConsumingContactInserts();
    }
}
