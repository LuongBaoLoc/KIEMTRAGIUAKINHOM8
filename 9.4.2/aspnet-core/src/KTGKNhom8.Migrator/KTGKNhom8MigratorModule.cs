using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KTGKNhom8.Configuration;
using KTGKNhom8.EntityFrameworkCore;
using KTGKNhom8.Migrator.DependencyInjection;

namespace KTGKNhom8.Migrator
{
    [DependsOn(typeof(KTGKNhom8EntityFrameworkModule))]
    public class KTGKNhom8MigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public KTGKNhom8MigratorModule(KTGKNhom8EntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(KTGKNhom8MigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                KTGKNhom8Consts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KTGKNhom8MigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
