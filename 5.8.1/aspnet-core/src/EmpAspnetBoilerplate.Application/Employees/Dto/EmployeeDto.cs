using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace EmpAspnetBoilerplate.Employees.Dto
{
    [AutoMapFrom(typeof(Employee))]
    public class EmployeeDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string ContactNumber { get; set; }
        public int? DepartmenttId { get; set; }
    }
}
