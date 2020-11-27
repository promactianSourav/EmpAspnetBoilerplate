using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EmpAspnetBoilerplate.Departments.Dto;

namespace EmpAspnetBoilerplate.Departments
{
    public interface IDepartmentAppService : IApplicationService
    {
        IEnumerable<GetDepartmentOutput> ListAll();
        Task Create(CreateDepartmentInput input);
        void Update(UpdateDepartmentInput input);
        void Delete(DeleteDepartmentInput input);
        GetDepartmentOutput GetDepartmentById(GetDepartmentInput input);

        Task<ListResultDto<PermissionDepartmentDto>> GetAllPermissions();
    }
}
