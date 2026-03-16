using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KTGKNhom8.Configuration;

namespace KTGKNhom8.Web.Host.Startup
{
    [DependsOn(
       typeof(KTGKNhom8WebCoreModule))]
    public class KTGKNhom8WebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public KTGKNhom8WebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KTGKNhom8WebHostModule).GetAssembly());
        }
    }
}
