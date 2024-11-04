using System.ComponentModel.DataAnnotations;
using MPCalcHub.Domain.Entities;
using MPCalcHub.Domain.Interfaces;
using MPCalcHub.Domain.Interfaces.Infrastructure;
using MPCalcHub.Domain.Services;
using MPCalcHub.Infrastructure.Data.Repositories;
using MPCalcHub.Tests.Shared.Fixtures;
using MPCalcHub.Tests.Shared.Fixtures.Entities;
using MPCalcHub.Tests.Shared.Fixtures.Utils;

namespace MPCalcHub.Tests.Domain.Services;

public class ContactServiceTests : BaseServiceTests
{
    private readonly IContactRepository _repository;
    private readonly IContactService _contactService;
    private readonly IStateDDDService _stateDDDService;
    private readonly IStateDDDRepository _stateDDDRepository;
    private readonly UserData _userData;

    public ContactServiceTests()
    {
        _userData = UserDataFixtures.CreateAs_Base();
        _repository = new ContactRepository(_context);
        _stateDDDRepository = new StateDDDRepository(_context);
        _stateDDDService = new StateDDDService(_stateDDDRepository, _userData);
        _contactService = new ContactService(_repository, _userData, _stateDDDService);
    }

    public class Insert : ContactServiceTests
    {
        [Fact]
        public async Task ShouldInsertContact()
        {
            // Arrange
            var contact = ContactFixtures.CreateAs_Base();
            var states = StateDDDFixtures.GenerateAllStateDDD();
            
            await _context.AddRangeAsync(states);
            await SaveChanges();

            // Act
            var result = await _contactService.Add(contact);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldInsertContact_InvalidDDD_ExpectedThrow()
        {
            // Arrange
            var contact = ContactFixtures.CreateAs_Base();
            contact.DDD = 99;

            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _contactService.Add(contact));

            // Assert
            Assert.Equal("DDD inválido e/ou não existe.", exception.Message);
        }
    }

    public class Update : ContactServiceTests
    {
        [Fact]
        public async Task ShouldUpdateContact()
        {
            // Arrange
            var contact = ContactFixtures.CreateAs_Base();
            var states = StateDDDFixtures.GenerateAllStateDDD();
            contact.DDD = 11;

            await _context.AddRangeAsync(states);
            await _context.AddAsync(contact);

            await SaveChanges();

            // Act
            contact.Name = "Updated Name";
            var result = await _contactService.Update(contact);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
        }
    }

    public class Remove : ContactServiceTests
    {
        [Fact]
        public async Task ShouldRemoveContact()
        {
            // Arrange
            var contact = ContactFixtures.CreateAs_Base();
            await _context.AddAsync(contact);
            await SaveChanges();

            // Act
            await _contactService.Remove(contact.Id);
            await SaveChanges();

            // Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _contactService.GetById(contact.Id, false, false));

            Assert.Equal("O contato não existe.", exception.Message);
            Assert.True(contact.Removed);
        }
    }

    public class FindByDDD : ContactServiceTests
    {
        [Fact]
        public async Task ShouldFindByDDD()
        {
            // Arrange
            var dddFilter = 12;
            var contact1 = ContactFixtures.CreateAs_Base();
            var contact2 = ContactFixtures.CreateAs_Base();
            contact2.DDD = dddFilter;

            await _context.AddRangeAsync(contact1, contact2);

            await SaveChanges();

            // Act
            var result = await _contactService.FindByDDD(dddFilter);

            // Assert
            var expectedReturn = 1;

            Assert.NotEmpty(result);
            Assert.Equal(expectedReturn, result.Count());
        }
    }

    public override void Dispose()
    {
        _context?.Dispose();
        _contactService?.Dispose();
        _repository?.Dispose();
        
        GC.SuppressFinalize(this);
    }
}