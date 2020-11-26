using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using EmpAspnetBoilerplate.Employees.Dto;
using AutoMapper;
using System.Linq;

namespace EmpAspnetBoilerplate.Employees
{
    public class EmployeesAppService : ApplicationService, IEmployeeAppService
    {
        private readonly IEmployeeManager employeeManager;
        private readonly IMapper mapper;

        public EmployeesAppService(IEmployeeManager employeeManager,IMapper mapper)
        {
            this.employeeManager = employeeManager;
            this.mapper = mapper;
        }
        public async Task Create(CreateEmployeeInput input)
        {
            Employee output = mapper.Map<CreateEmployeeInput, Employee>(input);
            await employeeManager.CreateEmployee(output);
        }

        public void Delete(DeleteEmployeeInput input)
        {
            employeeManager.DeleteEmployee(input.Id);
        }

        public GetEmployeeOutput GetEmployeeById(GetEmployeeInput input)
        {
            var getEmployee = employeeManager.GetEmployeeByID(input.Id);
            GetEmployeeOutput output = mapper.Map<Employee, GetEmployeeOutput>(getEmployee);
            return output;
        }

        public IEnumerable<GetEmployeeOutput> ListAll()
        {
            var getAll = employeeManager.GetAllList().ToList();
            List<GetEmployeeOutput> output = mapper.Map<List<Employee>, List<GetEmployeeOutput>>(getAll);
            return output;
        }

        public void Update(UpdateEmployeeInput input)
        {
            Employee output = mapper.Map<UpdateEmployeeInput, Employee>(input);
            employeeManager.UpdateEmployee(output);
        }
    }
}
