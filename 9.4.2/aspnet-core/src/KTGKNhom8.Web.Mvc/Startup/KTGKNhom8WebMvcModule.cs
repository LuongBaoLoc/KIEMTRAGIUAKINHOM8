using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KTGKNhom8.Configuration;

namespace KTGKNhom8.Web.Startup
{
    [DependsOn(typeof(KTGKNhom8WebCoreModule))]
    public class KTGKNhom8WebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public KTGKNhom8WebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<KTGKNhom8NavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KTGKNhom8WebMvcModule).GetAssembly());
        }
    }
}
