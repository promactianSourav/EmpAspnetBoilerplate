using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using AutoMapper;
using EmpAspnetBoilerplate.Authorization;
using EmpAspnetBoilerplate.Departments.Dto;

namespace EmpAspnetBoilerplate.Departments
{
    [AbpAuthorize(PermissionNames.Pages_Departments)]
    public class DepartmentsAppService : ApplicationService, IDepartmentAppService
    {
        private readonly IDepartmentManager departmentManager;
        private readonly IMapper mapper;

        public DepartmentsAppService(IDepartmentManager departmentManager,IMapper mapper)
        {
            this.departmentManager = departmentManager;
            this.mapper = mapper;
        }
        public async Task Create(CreateDepartmentInput input)
        {
            Department output = mapper.Map<CreateDepartmentInput, Department>(input);
            await departmentManager.CreateDepartment(output);
        }

        public void Delete(DeleteDepartmentInput input)
        {
            departmentManager.DeleteDepartment(input.Id);
        }

        public GetDepartmentOutput GetDepartmentById(GetDepartmentInput input)
        {
            var getEmployee = departmentManager.GetDepartmentByID(input.Id);
            GetDepartmentOutput output = mapper.Map<Department, GetDepartmentOutput>(getEmployee);
            return output;
        }

        public IEnumerable<GetDepartmentOutput> ListAll()
        {
            var getAll = departmentManager.GetAllList().ToList();
            List<GetDepartmentOutput> output = mapper.Map<List<Department>, List<GetDepartmentOutput>>(getAll);
            return output;
        }

        public void Update(UpdateDepartmentInput input)
        {
            Department output = mapper.Map<UpdateDepartmentInput, Department>(input);
            departmentManager.UpdateDepartment(output);
        }
    }
}
