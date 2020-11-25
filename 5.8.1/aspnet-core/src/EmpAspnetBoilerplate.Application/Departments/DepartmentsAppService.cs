using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using EmpAspnetBoilerplate.Departments.Dto;

namespace EmpAspnetBoilerplate.Departments
{
    public class DepartmentsAppService : CrudAppService<Department, DepartmentDto>
    {
        public DepartmentsAppService(IRepository<Department, int> repository) : base(repository)
        {
        }
    }
}
