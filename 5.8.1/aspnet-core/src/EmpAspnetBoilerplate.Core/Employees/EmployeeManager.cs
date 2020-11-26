using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;

namespace EmpAspnetBoilerplate.Employees
{
    public class EmployeeManager : DomainService, IEmployeeManager
    {
        private readonly IRepository<Employee> repositoryEmployee;

        public EmployeeManager(IRepository<Employee> repositoryEmployee)
        {
            this.repositoryEmployee = repositoryEmployee;
        }
        public async Task<Employee> CreateEmployee(Employee entity)
        {
            var employee = repositoryEmployee.FirstOrDefault(x => x.Id == entity.Id);
            if (employee != null)
            {
                throw new UserFriendlyException("Already Exist");
            }
            return await repositoryEmployee.InsertAsync(entity);
        }

        public void DeleteEmployee(int id)
        {
            var employee = repositoryEmployee.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                throw new UserFriendlyException("No Data Found");
            }
            else
            {
                repositoryEmployee.Delete(employee);
            }
        }

        public IEnumerable<Employee> GetAllList()
        {
            return repositoryEmployee.GetAllList();
        }

        public Employee GetEmployeeByID(int id)
        {
            return repositoryEmployee.Get(id);
        }

        public void UpdateEmployee(Employee entity)
        {
            repositoryEmployee.Update(entity);
        }
    }
}
