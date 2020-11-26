using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmpAspnetBoilerplate.Authorization;

namespace EmpAspnetBoilerplate
{
    [DependsOn(
        typeof(EmpAspnetBoilerplateCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class EmpAspnetBoilerplateApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<EmpAspnetBoilerplateAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(EmpAspnetBoilerplateApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
               
            );
        }
    }
}
