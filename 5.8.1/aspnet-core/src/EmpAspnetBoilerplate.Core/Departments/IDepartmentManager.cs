using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace EmpAspnetBoilerplate.Departments
{
    public interface IDepartmentManager : IDomainService
    {
        IEnumerable<Department> GetAllList();
        Department GetDepartmentByID(int id);
        Task<Department> CreateDepartment(Department entity);
        void UpdateDepartment(Department entity);
        void DeleteDepartment(int id);
    }
}
