using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using EmpAspnetBoilerplate.Authorization.Roles;
using EmpAspnetBoilerplate.Authorization.Users;
using EmpAspnetBoilerplate.MultiTenancy;
using EmpAspnetBoilerplate.Employees;
using EmpAspnetBoilerplate.Departments;

namespace EmpAspnetBoilerplate.EntityFrameworkCore
{
    public class EmpAspnetBoilerplateDbContext : AbpZeroDbContext<Tenant, Role, User, EmpAspnetBoilerplateDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        
        public EmpAspnetBoilerplateDbContext(DbContextOptions<EmpAspnetBoilerplateDbContext> options)
            : base(options)
        {
        }
    }
}
