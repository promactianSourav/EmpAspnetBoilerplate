using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using EmpAspnetBoilerplate.Departments;

namespace EmpAspnetBoilerplate.Employees
{
    public class Employee : FullAuditedEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        [Required(ErrorMessage = "Please fill the contact number.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Not a valid Phone number")]
        public string ContactNumber { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        
        public virtual Department Department { get; set; }
    }
}
