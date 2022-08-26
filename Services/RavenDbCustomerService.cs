public class RavenDbCustomerService : ICustomerService, IStaticService // IStaticService added here to avoid compilation error
{
    public string ServiceName => nameof(RavenDbCustomerService);
    public static void AddServiceDependencies(IServiceCollection services, IConfiguration configuration) { }
    public List<Customer> GetAllCustomers() => new();
}