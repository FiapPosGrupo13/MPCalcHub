using MPCalcHub.Domain.Entities;
using MPCalcHub.Domain.Interfaces;
using MPCalcHub.Domain.Interfaces.Infrastructure;

namespace MPCalcHub.Domain.Services;

public class ContactService(IContactRepository contactRepository, UserData userData) : BaseService<Contact>(contactRepository, userData), IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Contact> GetById(Guid id, bool include, bool tracking)
    {
        var entity = await _contactRepository.GetById(id, include, tracking);

        if (entity == null)
            throw new Exception("O contato não existe.");

        return entity;
    }

    public override async Task<Contact> Add(Contact entity)
    {
        var contact = await _contactRepository.GetById(entity.Id);

        if (contact != null)
            throw new Exception("O contato já existe.");
        
        return await base.Add(entity);
    }

    public async Task<Contact> GetByEmail(string email)
    {
        var entity = await _contactRepository.GetByEmail(email);
        if (entity == null)
            throw new Exception("O contato não existe.");

        return entity; 
    }

    public async Task Remove(Guid id)
    {
        var entity = await _contactRepository.GetById(id, false, true);
        if (entity == null)
            throw new Exception("O contato não existe.");

        await base.Remove(entity);
    }

    public async Task<IEnumerable<Contact>> FindByDDD(string ddd)
    {
        return await _contactRepository.FindBy(c => c.DDD == ddd);
    }
}