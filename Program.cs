
WebApplicationBuilder builder = WebApplication.CreateBuilder();
builder.Services.AddServicesForAssembly(builder.Configuration);
// compiler error CS8920 "static member does not have a most specific implementation of the interface"
//builder.Services.AddSingleton<ICustomerService>(services =>
//    CustomerServiceFactory.GetCustomerService(services, CustomerServiceType.CosmosDb));
builder.Services.AddSingleton(typeof(ICustomerService), services =>
    CustomerServiceFactory.GetCustomerService(services, CustomerServiceType.CosmosDb));

WebApplication app = builder.Build();

// compiler error CS8920 "static member does not have a most specific implementation of the interface"
//ICustomerService customerService = app.Services.GetRequiredService<ICustomerService>();
ICustomerService customerService = (ICustomerService)app.Services.GetRequiredService(typeof(ICustomerService));
