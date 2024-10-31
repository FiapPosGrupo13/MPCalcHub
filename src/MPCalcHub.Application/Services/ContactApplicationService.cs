using AutoMapper;
using MPCalcHub.Application.DataTransferObjects;
using MPCalcHub.Application.Interfaces;
using MPCalcHub.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using EN = MPCalcHub.Domain.Entities;

namespace MPCalcHub.Application.Services;

public class ContactApplicationService(
    IContactService ContactService,
    IMapper mapper) : IContactApplicationService
{

    public async Task<Contact> Add(BasicContact model)
    {
        var Contact = mapper.Map<EN.Contact>(model);

        Contact = await ContactService.Add(Contact);

        return mapper.Map<Contact>(Contact);
    }

    public async Task<Contact> Update(Contact model)
    {
        var Contact = mapper.Map<EN.Contact>(model);

        Contact = await ContactService.Update(Contact);

        return mapper.Map<Contact>(Contact);
    }

    public async Task<IEnumerable<Contact>> FindBy(Expression<Func<EN.Contact, bool>> expression)
    {
        return mapper.Map<IEnumerable<Contact>>(await ContactService.FindBy(expression));
    }

    public async Task<Contact> GetById(Guid id)
    {
        return mapper.Map<Contact>(await ContactService.GetById(id));
    }

    public async Task Remove(Guid id)
    {
        await ContactService.Remove(id);
    }
}
