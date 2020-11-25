using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using EmpAspnetBoilerplate.Configuration.Dto;

namespace EmpAspnetBoilerplate.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : EmpAspnetBoilerplateAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
