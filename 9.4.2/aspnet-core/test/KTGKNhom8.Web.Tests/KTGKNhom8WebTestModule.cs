using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KTGKNhom8.EntityFrameworkCore;
using KTGKNhom8.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace KTGKNhom8.Web.Tests
{
    [DependsOn(
        typeof(KTGKNhom8WebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class KTGKNhom8WebTestModule : AbpModule
    {
        public KTGKNhom8WebTestModule(KTGKNhom8EntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KTGKNhom8WebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(KTGKNhom8WebMvcModule).Assembly);
        }
    }
}