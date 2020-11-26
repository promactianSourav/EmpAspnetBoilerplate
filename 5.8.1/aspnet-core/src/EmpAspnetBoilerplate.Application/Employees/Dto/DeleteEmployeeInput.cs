using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace EmpAspnetBoilerplate.Employees.Dto
{
    [AutoMapTo(typeof(Employee))]
    public class DeleteEmployeeInput
    {
        public int Id { get; set; }
       
    }
}
