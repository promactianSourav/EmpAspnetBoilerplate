using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace EmpAspnetBoilerplate.Departments
{
    public class Department : FullAuditedEntity
    {
        public string DepartmentName { get; set; }
    }
}
