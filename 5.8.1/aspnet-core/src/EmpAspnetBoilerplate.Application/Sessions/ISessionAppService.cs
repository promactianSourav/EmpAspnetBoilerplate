using System.Threading.Tasks;
using Abp.Application.Services;
using EmpAspnetBoilerplate.Sessions.Dto;

namespace EmpAspnetBoilerplate.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
