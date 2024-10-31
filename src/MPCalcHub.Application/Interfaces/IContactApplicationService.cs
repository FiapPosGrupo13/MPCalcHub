using MPCalcHub.Application.DataTransferObjects;

namespace MPCalcHub.Application.Interfaces;

public interface IContactApplicationService
{   
    Task<Contact> Add(BasicContact model);
    Task<Contact> Update(Contact model);
    Task<IEnumerable<Contact>> FindByDDD(int ddd);
    Task Remove(Guid id);
    Task<Contact> GetById(Guid id);
}