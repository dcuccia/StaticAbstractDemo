using FluentAssertions;
using NSubstitute;
using Xunit;

public class BusinessApiTests
{
    public class FakeCustomerService : ICustomerService
    {
        public virtual string ServiceName => "";
        public virtual List<Customer> GetAllCustomers() => new();

        public static void AddServiceDependencies(IServiceCollection services, IConfiguration configuration) { }
    }

    [Fact]
    public static void GetPrintableCustomerList_ReturnsEmptyList_WhenNoCustomersExist()
    {
        // Arrange
        // compiler error CS8920 "static member does not have a most specific implementation of the interface"
        // ICustomerService subCustomerService = NSubstitute.Substitute.For<ICustomerService>();
        // NSubstitute (sometimes?) generates a runtime System.TypeLoadException : Virtual static method 'AddServiceDependencies' is not implemented on type 'Castle.Proxies.ObjectProxy_1' from assembly 'DynamicProxyGenAssembly2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.
        // ICustomerService subCustomerService = (ICustomerService)NSubstitute.Substitute.For(new[] { typeof(ICustomerService) }, new object[] { });
        // To get this class to work reliably, mock a "fake" class with virtual methods for now, using it's interface:
        ICustomerService subCustomerService = Substitute.For<FakeCustomerService>();
        subCustomerService.GetAllCustomers().Returns(new List<Customer>()); // MOCK EXAMPLE
        BusinessApi api = new (subCustomerService); 
        
        // Act
        List<string> shouldBeEmpty = api.GetPrintableCustomerList();
        
        // Assert
        shouldBeEmpty.Count.Should().Be(0);
    }
    
    [Fact]
    public static void WebApplication_Build_ShouldNotThrowException()
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Services.AddServicesForAssembly(builder.Configuration);
        // compiler error CS8920 "static member does not have a most specific implementation of the interface"
        //builder.Services.AddSingleton<ICustomerService>(services =>
        //    CustomerServiceFactory.GetCustomerService(services, CustomerServiceType.CosmosDb));
        builder.Services.AddSingleton(typeof(ICustomerService), services =>
            CustomerServiceFactory.GetCustomerService(services, CustomerServiceType.CosmosDb));

        // Act
        WebApplication app = builder.Build();
        // compiler error CS8920 "static member does not have a most specific implementation of the interface"
        // ICustomerService customerService = app.Services.GetRequiredService<ICustomerService>();
        ICustomerService customerService = (ICustomerService)app.Services.GetRequiredService(typeof(ICustomerService));

        // Assert
        customerService.Should().NotBeNull();
    }
}
    
