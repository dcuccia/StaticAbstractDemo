using FluentAssertions;
using NSubstitute;
using Xunit;

public class BusinessApiTests
{
    [Fact]
    public static void GetPrintableCustomerList_ReturnsEmptyList_WhenNoCustomersExist()
    {
        // Arrange
        
        // compiler error CS8920 "static member does not have a most specific implementation of the interface"
        ICustomerService subCustomerService = NSubstitute.Substitute.For<ICustomerService>();
        
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
        builder.Services.AddSingleton<ICustomerService>(services =>
            CustomerServiceFactory.GetCustomerService(services, CustomerServiceType.CosmosDb));

        // Act
        WebApplication app = builder.Build();
        ICustomerService customerService = app.Services.GetRequiredService<ICustomerService>();

        // Assert
        customerService.Should().NotBeNull();
    }
}
    
