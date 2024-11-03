using MPCalcHub.Domain.Interfaces;
using MPCalcHub.Domain.Interfaces.Infrastructure;
using MPCalcHub.Domain.Services;
using MPCalcHub.Infrastructure.Data.Repositories;
using MPCalcHub.Tests.Shared.Fixtures.Entities;

namespace MPCalcHub.Tests.Domain.Services;

public class ContactServiceTests : BaseServiceTests
{
    private readonly IContactRepository _repository;
    private readonly IContactService _contactService;
    private readonly IStateDDDService _stateDDDService;

    public ContactServiceTests()
    {
        _repository = new ContactRepository(_context);
        _contactService = new ContactService(_repository, _userData, _stateDDDService);
    }

    public class Insert : ContactServiceTests
    {
      
    }
}