using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace EmpAspnetBoilerplate.Departments.Dto
{
    [AutoMapTo(typeof(Department))]
    public class CreateDepartmentInput
    {
        public string DepartmentName { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
