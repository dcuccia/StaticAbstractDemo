public interface IStaticService
{
    // cross-cutting concern: have services boostrap their own dependencies (opinionated approach...)
    public static abstract void AddServiceDependencies(IServiceCollection services, IConfiguration configuration); 
}