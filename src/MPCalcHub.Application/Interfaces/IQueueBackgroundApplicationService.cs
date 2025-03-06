using MPCalcHub.Application.DataTransferObjects;


namespace MPCalcHub.Application.Interfaces
{
    public interface IQueueBackgroundApplicationService
    {
        Task<Contact> EnqueueContactUpdateAsync(Contact model, CancellationToken cancellationToken = default);
        Task<Contact> EnqueueContactInsertAsync(BasicContact model, CancellationToken cancellationToken = default);
        void StartConsumingContactUpdates();
        void StartConsumingContactInserts();
    }
}
