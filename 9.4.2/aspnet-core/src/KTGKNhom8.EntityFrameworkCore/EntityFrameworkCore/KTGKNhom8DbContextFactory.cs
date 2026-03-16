using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using KTGKNhom8.Configuration;
using KTGKNhom8.Web;

namespace KTGKNhom8.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class KTGKNhom8DbContextFactory : IDesignTimeDbContextFactory<KTGKNhom8DbContext>
    {
        public KTGKNhom8DbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<KTGKNhom8DbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            KTGKNhom8DbContextConfigurer.Configure(builder, configuration.GetConnectionString(KTGKNhom8Consts.ConnectionStringName));

            return new KTGKNhom8DbContext(builder.Options);
        }
    }
}
