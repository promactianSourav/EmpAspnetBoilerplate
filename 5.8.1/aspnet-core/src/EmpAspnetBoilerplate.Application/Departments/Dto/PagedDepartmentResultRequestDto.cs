using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace EmpAspnetBoilerplate.Departments.Dto
{
    public class PagedDepartmentResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

