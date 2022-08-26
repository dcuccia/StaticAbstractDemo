public class BusinessApi
{
    private readonly ICustomerService _customerService;

    public BusinessApi(ICustomerService customerService) =>
        _customerService = customerService;

    public List<string> GetPrintableCustomerList() =>
        _customerService
            .GetAllCustomers()
            .Select(c => $"{c.Id}: {c.Name}")
            .ToList();
}