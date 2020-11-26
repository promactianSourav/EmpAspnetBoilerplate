using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace EmpAspnetBoilerplate.Departments.Dto
{
    [AutoMapFrom(typeof(Department))]
    public class GetDepartmentOutput
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
    }
}
