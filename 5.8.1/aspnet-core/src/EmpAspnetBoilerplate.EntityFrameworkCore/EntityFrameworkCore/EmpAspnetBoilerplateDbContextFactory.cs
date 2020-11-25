using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using EmpAspnetBoilerplate.Configuration;
using EmpAspnetBoilerplate.Web;

namespace EmpAspnetBoilerplate.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class EmpAspnetBoilerplateDbContextFactory : IDesignTimeDbContextFactory<EmpAspnetBoilerplateDbContext>
    {
        public EmpAspnetBoilerplateDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EmpAspnetBoilerplateDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            EmpAspnetBoilerplateDbContextConfigurer.Configure(builder, configuration.GetConnectionString(EmpAspnetBoilerplateConsts.ConnectionStringName));

            return new EmpAspnetBoilerplateDbContext(builder.Options);
        }
    }
}
