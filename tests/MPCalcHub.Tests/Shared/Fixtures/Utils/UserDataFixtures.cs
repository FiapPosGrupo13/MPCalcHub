using MPCalcHub.Domain.Entities;

namespace MPCalcHub.Tests.Shared.Fixtures.Utils;

public class UserDataFixtures : BaseFixtures<UserData>
{
    public static UserData GenerateUserData()
    {
        var userData = Faker.
            RuleFor(u => u.Id, f => f.Random.Guid()).
            RuleFor(u => u.Email, f => f.Person.Email).
            RuleFor(u => u.Name, f => f.Person.FullName).
            Generate();

        return userData;
    }

    public static UserData CreateAs_Base()
    {
        var userData = GenerateUserData();

        userData.PrepareToInsert(Guid.NewGuid());

        return userData;
    }
}