using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;

namespace EmpAspnetBoilerplate.Employees.Dto
{
    [AutoMapFrom(typeof(Permission))]
    public class PermissionEmployeeDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
