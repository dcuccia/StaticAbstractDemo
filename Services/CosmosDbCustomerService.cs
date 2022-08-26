using Microsoft.Extensions.DependencyInjection.Extensions;

public class CosmosDbCustomerService : ICustomerService, IStaticService
{
     private readonly IRepository<Customer> _repository;
     
     public CosmosDbCustomerService(IRepository<Customer> repository)
     {
         _repository = repository;
     }

     public string ServiceName => nameof(CosmosDbCustomerService);
     public List<Customer> GetAllCustomers() => _repository.Find(c => c.Name != null);
     
    public static void AddServiceDependencies(IServiceCollection services, IConfiguration configuration)
    {
         services.AddOptions();
         services.TryAddSingleton<CosmosDbRepository>(); // boostraps the IRepository<T> container
         services.TryAddSingleton<IRepository<Customer>, CosmosDbRepository>(); // boostraps the IRepository<T> container
    }
}