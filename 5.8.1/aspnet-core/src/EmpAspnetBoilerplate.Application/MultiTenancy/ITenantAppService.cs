using Abp.Application.Services;
using EmpAspnetBoilerplate.MultiTenancy.Dto;

namespace EmpAspnetBoilerplate.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

