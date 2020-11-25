using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace EmpAspnetBoilerplate.Controllers
{
    public abstract class EmpAspnetBoilerplateControllerBase: AbpController
    {
        protected EmpAspnetBoilerplateControllerBase()
        {
            LocalizationSourceName = EmpAspnetBoilerplateConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
