using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;

namespace EmpAspnetBoilerplate.Departments
{
    public class DepartmentManager : DomainService, IDepartmentManager
    {
        private readonly IRepository<Department> repositoryDepartment;

        public DepartmentManager(IRepository<Department> repositoryDepartment)
        {
            this.repositoryDepartment = repositoryDepartment;
        }
        public async Task<Department> CreateDepartment(Department entity)
        {
            var department = repositoryDepartment.FirstOrDefault(x => x.Id == entity.Id);
            if (department != null)
            {
                throw new UserFriendlyException("Already Exist");
            }
            return await repositoryDepartment.InsertAsync(entity);
        }

        public void DeleteDepartment(int id)
        {
            var department = repositoryDepartment.FirstOrDefault(x => x.Id == id);
            if (department == null)
            {
                throw new UserFriendlyException("No Data Found");
            }
            else
            {
                repositoryDepartment.Delete(department);
            }
        }

        public IEnumerable<Department> GetAllList()
        {
            return repositoryDepartment.GetAllList();
        }

        public Department GetDepartmentByID(int id)
        {
            return repositoryDepartment.Get(id);
        }

        public void UpdateDepartment(Department entity)
        {
            repositoryDepartment.Update(entity);
        }
    }
}
