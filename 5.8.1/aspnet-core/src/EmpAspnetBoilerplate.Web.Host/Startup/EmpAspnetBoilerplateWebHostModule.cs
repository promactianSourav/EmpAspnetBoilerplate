using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmpAspnetBoilerplate.Configuration;

namespace EmpAspnetBoilerplate.Web.Host.Startup
{
    [DependsOn(
       typeof(EmpAspnetBoilerplateWebCoreModule))]
    public class EmpAspnetBoilerplateWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EmpAspnetBoilerplateWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EmpAspnetBoilerplateWebHostModule).GetAssembly());
        }
    }
}
