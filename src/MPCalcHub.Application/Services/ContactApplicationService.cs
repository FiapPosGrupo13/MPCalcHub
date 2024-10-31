using AutoMapper;
using MPCalcHub.Application.DataTransferObjects;
using MPCalcHub.Application.Interfaces;
using MPCalcHub.Domain.Interfaces;
using EN = MPCalcHub.Domain.Entities;

namespace MPCalcHub.Application.Services;

public class ContactApplicationService(IContactService contactService, IMapper mapper) : IContactApplicationService
{
    private readonly IContactService _contactService = contactService;
    private readonly IMapper _mapper = mapper;

    public async Task<Contact> Add(BasicContact model)
    {
        var contact = _mapper.Map<EN.Contact>(model);

        contact = await _contactService.Add(contact);

        return _mapper.Map<Contact>(contact);
    }

    public async Task<Contact> Update(Contact model)
    {
        var contact = await _contactService.GetById(model.Id.Value, include: false, tracking: true);
        if (contact == null)
            throw new Exception("O contato não existe.");
            
        _mapper.Map(model, contact);

        contact = await _contactService.Update(contact);

        return _mapper.Map<Contact>(contact);
    }

    public async Task<IEnumerable<Contact>> FindByDDD(int ddd)
    {
        var contact = await _contactService.FindByDDD(ddd);
        return _mapper.Map<IEnumerable<Contact>>(contact);
    }

    public async Task<Contact> GetById(Guid id)
    {
        var contact = await _contactService.GetById(id, include: false, tracking: false);
        return _mapper.Map<Contact>(contact);
    }

    public async Task Remove(Guid id)
    {
        await _contactService.Remove(id);
    }
}
