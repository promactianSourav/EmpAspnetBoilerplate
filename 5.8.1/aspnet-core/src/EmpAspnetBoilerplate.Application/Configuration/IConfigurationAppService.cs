using System.Threading.Tasks;
using EmpAspnetBoilerplate.Configuration.Dto;

namespace EmpAspnetBoilerplate.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
