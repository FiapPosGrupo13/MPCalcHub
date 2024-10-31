using MPCalcHub.Domain.Entities;

namespace MPCalcHub.Domain.Interfaces;

public interface IContactService : IBaseService<Contact>
{
    Task<Contact> GetById(Guid id);
    Task<Contact> GetByEmail(string email);
    Task Remove(Guid id);
}