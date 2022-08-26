public static class CustomerServiceFactory
{
    // simplify factories that don't themselves need instance members by using static contracts
    public static ICustomerService GetCustomerService(IServiceProvider provider, CustomerServiceType type) => type switch
    {
        CustomerServiceType.CosmosDb => provider.GetRequiredService<CosmosDbCustomerService>(),
        CustomerServiceType.RavenDb => provider.GetRequiredService<RavenDbCustomerService>(),
        _ => throw new ArgumentOutOfRangeException()
    };
}