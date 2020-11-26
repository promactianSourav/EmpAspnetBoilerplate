using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace EmpAspnetBoilerplate.Departments.Dto
{
    [AutoMapTo(typeof(Department))]
    public class DeleteDepartmentInput
    {
        public int Id { get; set; }
    }
}
