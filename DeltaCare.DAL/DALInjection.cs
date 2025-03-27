using Microsoft.Extensions.DependencyInjection;

namespace DeltaCare.DAL
{
    public static class DALInjection
    {
        public static IServiceCollection RegisterDALServices(this IServiceCollection services) => services
      .AddTransient<IDataRepository, DataRepository>();

    }
}
