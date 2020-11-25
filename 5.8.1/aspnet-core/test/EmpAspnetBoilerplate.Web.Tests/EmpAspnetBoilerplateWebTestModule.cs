using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmpAspnetBoilerplate.EntityFrameworkCore;
using EmpAspnetBoilerplate.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace EmpAspnetBoilerplate.Web.Tests
{
    [DependsOn(
        typeof(EmpAspnetBoilerplateWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class EmpAspnetBoilerplateWebTestModule : AbpModule
    {
        public EmpAspnetBoilerplateWebTestModule(EmpAspnetBoilerplateEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EmpAspnetBoilerplateWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(EmpAspnetBoilerplateWebMvcModule).Assembly);
        }
    }
}