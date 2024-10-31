using MPCalcHub.Application.DataTransferObjects;
using System.Linq.Expressions;
using EN = MPCalcHub.Domain.Entities;

namespace MPCalcHub.Application.Interfaces;

public interface IContactApplicationService
{   
    Task<Contact> Add(BasicContact model);
    Task<Contact> Update(Contact model);
    Task<IEnumerable<Contact>> FindByDDD(string ddd);
    Task Remove(Guid id);
    Task<Contact> GetById(Guid id);
}