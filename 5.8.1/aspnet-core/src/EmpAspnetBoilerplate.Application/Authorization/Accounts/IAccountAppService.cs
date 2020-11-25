using System.Threading.Tasks;
using Abp.Application.Services;
using EmpAspnetBoilerplate.Authorization.Accounts.Dto;

namespace EmpAspnetBoilerplate.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
