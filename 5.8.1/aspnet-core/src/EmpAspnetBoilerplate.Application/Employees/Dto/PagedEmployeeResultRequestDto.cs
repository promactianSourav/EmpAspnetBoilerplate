using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace EmpAspnetBoilerplate.Employees.Dto
{
    public class PagedEmployeeResultRequestDto : PagedResultRequestDto
    {

        public string Keyword { get; set; }
    }
}
