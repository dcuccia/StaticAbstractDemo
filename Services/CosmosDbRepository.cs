public class CosmosDbRepository : IRepository<Customer>
{
    public List<Customer> Find(Predicate<Customer> query) => throw new NotImplementedException();
}