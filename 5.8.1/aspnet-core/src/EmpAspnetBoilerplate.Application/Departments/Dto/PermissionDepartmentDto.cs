using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;

namespace EmpAspnetBoilerplate.Departments.Dto
{
    [AutoMapFrom(typeof(Permission))]
    public class PermissionDepartmentDto : EntityDto<long>
    {
        public string DepartmentName { get; set; }
    }
}
