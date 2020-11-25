using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace EmpAspnetBoilerplate.Departments.Dto
{
    [AutoMapFrom(typeof(Department))]
    public class DepartmentDto : EntityDto<int>
    {
        public string DepartmentName { get; set; }

    }
}
