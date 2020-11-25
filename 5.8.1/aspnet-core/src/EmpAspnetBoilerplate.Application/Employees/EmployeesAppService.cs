using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using EmpAspnetBoilerplate.Employees.Dto;

namespace EmpAspnetBoilerplate.Employees
{
    public class EmployeesAppService : CrudAppService<Employee, EmployeeDto>
    {
        public EmployeesAppService(IRepository<Employee, int> repository) : base(repository)
        {
        }
    }
}
