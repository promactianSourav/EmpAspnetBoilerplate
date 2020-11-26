using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using EmpAspnetBoilerplate.Employees.Dto;

namespace EmpAspnetBoilerplate.Employees
{
    public interface IEmployeeAppService : IApplicationService
    {
        IEnumerable<GetEmployeeOutput> ListAll();
        Task Create(CreateEmployeeInput input);
        void Update(UpdateEmployeeInput input);
        void Delete(DeleteEmployeeInput input);
        GetEmployeeOutput GetEmployeeById(GetEmployeeInput input);
    }
}
