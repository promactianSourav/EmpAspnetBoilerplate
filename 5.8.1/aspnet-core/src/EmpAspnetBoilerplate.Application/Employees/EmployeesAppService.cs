using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using EmpAspnetBoilerplate.Employees.Dto;
using AutoMapper;
using System.Linq;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Identity;
using Abp.IdentityFramework;
using Abp.Authorization;
using EmpAspnetBoilerplate.Authorization;

namespace EmpAspnetBoilerplate.Employees
{
    [AbpAuthorize(PermissionNames.Pages_Employees)]
    public class EmployeesAppService : AsyncCrudAppService<Employee, EmployeeDto, int, PagedEmployeeResultRequestDto, CreateEmployeeInput, EmployeeDto>
    {
        private readonly IEmployeeManager employeeManager;
        private readonly IMapper mapper;

        public EmployeesAppService(IRepository<Employee> repository,IEmployeeManager employeeManager,IMapper mapper):base(repository)
        {
            this.employeeManager = employeeManager;
            this.mapper = mapper;
        }

        public Task<ListResultDto<PermissionEmployeeDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionEmployeeDto>(
                ObjectMapper.Map<List<PermissionEmployeeDto>>(permissions).OrderBy(p => p.Name).ToList()
            ));
        }

        
        protected override IQueryable<Employee> ApplySorting(IQueryable<Employee> query, PagedEmployeeResultRequestDto input)
        {
            return query.OrderBy(r => r.Name);
        }



        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        //public async Task Create(CreateEmployeeInput input)
        //{
        //    Employee output = mapper.Map<CreateEmployeeInput, Employee>(input);
        //    await employeeManager.CreateEmployee(output);
        //}

        //public void Delete(DeleteEmployeeInput input)
        //{
        //    employeeManager.DeleteEmployee(input.Id);
        //}

        //public GetEmployeeOutput GetEmployeeById(GetEmployeeInput input)
        //{
        //    var getEmployee = employeeManager.GetEmployeeByID(input.Id);
        //    GetEmployeeOutput output = mapper.Map<Employee, GetEmployeeOutput>(getEmployee);
        //    return output;
        //}

        //public IEnumerable<GetEmployeeOutput> ListAll()
        //{
        //    var getAll = employeeManager.GetAllList().ToList();
        //    List<GetEmployeeOutput> output = mapper.Map<List<Employee>, List<GetEmployeeOutput>>(getAll);
        //    return output;
        //}

        //public void Update(UpdateEmployeeInput input)
        //{
        //    Employee output = mapper.Map<UpdateEmployeeInput, Employee>(input);
        //    employeeManager.UpdateEmployee(output);
        //}
    }
}
