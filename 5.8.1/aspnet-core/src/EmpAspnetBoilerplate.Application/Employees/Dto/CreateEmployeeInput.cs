using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace EmpAspnetBoilerplate.Employees.Dto
{
    [AutoMapTo(typeof(Employee))]
    public class CreateEmployeeInput
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string ContactNumber { get; set; }
        public int DepartmentId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
