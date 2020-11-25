using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;

namespace EmpAspnetBoilerplate.Departments
{
    public class Department : Entity<int>
    {
        public string DepartmentName { get; set; }
    }
}
