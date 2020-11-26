using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace EmpAspnetBoilerplate.Employees
{
    public interface IEmployeeManager : IDomainService
    {
        IEnumerable<Employee> GetAllList();
        Employee GetEmployeeByID(int id);
        Task<Employee> CreateEmployee(Employee entity);
        void UpdateEmployee(Employee entity);
        void DeleteEmployee(int id);
    }
}
