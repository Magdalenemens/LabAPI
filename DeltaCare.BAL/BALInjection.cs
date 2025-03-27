using DeltaCare.BAL.Account;
using DeltaCare.BAL.Barcode;
using DeltaCare.BAL.Clinical.AP;
using DeltaCare.BAL.Clinical.AP_Reports;
using DeltaCare.BAL.Common;
using DeltaCare.BAL.Permission;
using DeltaCare.BAL.PR;
using DeltaCare.BAL.Site;
using DeltaCare.BAL.UserAccess;
using DeltaCare.Common;
using Microsoft.Extensions.DependencyInjection;

namespace DeltaCare.BAL
{
    public static class BALInjection
    {
        public static IServiceCollection RegisterBALServices(this IServiceCollection services, AppSettings configuration)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IBarcodeRepository, BarcodeRepository>();
            services.AddTransient<ICentralReceivingRepository, CentralReceivingRepository>();
            services.AddTransient<IClinicalRepository, ClinicalRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IGenLabRepository, GenLabRepository>();
            services.AddSingleton<ITokenRepository, TokenRepository>();
            services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
            services.AddTransient<IDirectoryRepository, DirectoryRepository>();
            services.AddTransient<IClientAccountRepository, ClientAccountRepository>();
            services.AddTransient<IGTRepository, GTRepository>();
            services.AddTransient<IMasterRepository, MasterRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPRRepository, PRRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<ISiteRepository, SiteRepository>();
            services.AddTransient<IPreAnalyticalReceivingRepository, PreAnalyticalReceivingRepository>();
            services.AddTransient<ITDRepository, TDRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserAccessRepository, UserAccessRepository>();
            services.AddTransient<IUtilityRepository, UtilityRepository>();
         
            return services;
        }
    }
}
