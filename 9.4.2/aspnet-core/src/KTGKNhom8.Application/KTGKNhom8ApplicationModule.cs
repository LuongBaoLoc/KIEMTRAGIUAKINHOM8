using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KTGKNhom8.Authorization;

namespace KTGKNhom8
{
    [DependsOn(
        typeof(KTGKNhom8CoreModule), 
        typeof(AbpAutoMapperModule))]
    public class KTGKNhom8ApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<KTGKNhom8AuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(KTGKNhom8ApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
