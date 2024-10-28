using Bogus;

namespace MPCalcHub.Tests.Shared.Fixtures.Utils;

public class BaseFixtures<T> : IDisposable where T : class
{
    protected static Faker<T> Faker { get; set; } = new Faker<T>("pt_BR");
    protected static Faker FakerDefault { get; set; } = new Faker("pt_BR");

    public BaseFixtures() { }

    public void Dispose() { }
}

[CollectionDefinition("BaseFixtures Collection")]
public class BaseFixtures2Collection : ICollectionFixture<BaseFixtures<object>> { }