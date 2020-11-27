using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using AutoMapper;
using EmpAspnetBoilerplate.Authorization;
using EmpAspnetBoilerplate.Departments.Dto;
using Microsoft.AspNetCore.Identity;

namespace EmpAspnetBoilerplate.Departments
{
    [AbpAuthorize(PermissionNames.Pages_Departments)]
    public class DepartmentsAppService : AsyncCrudAppService<Department, DepartmentDto, int, PagedDepartmentResultRequestDto, CreateDepartmentInput, DepartmentDto>
    {
        private readonly IDepartmentManager departmentManager;
        private readonly IMapper mapper;

        public DepartmentsAppService(IRepository<Department> repository ,IDepartmentManager departmentManager,IMapper mapper) :base (repository)
        {
            this.departmentManager = departmentManager;
            this.mapper = mapper;
        }


        //public override async Task<DepartmentDto> UpdateAsync(DepartmentDto input)
        //{
        //    Department output = mapper.Map<DepartmentDto, Department>(input);
        //    //    departmentManager.UpdateDepartment(output);
        //    departmentManager.UpdateDepartment(output);
        //    var department = await departmentManager.GetDepartmentByID(input.Id);
        //    var outputDto = mapper.Map<Department, DepartmentDto>(department);
        //    return outputDto;

        //}

        //public async Task Create(CreateDepartmentInput input)
        //{
        //    Department output = mapper.Map<CreateDepartmentInput, Department>(input);
        //    await departmentManager.CreateDepartment(output);
        //}

        //public void Delete(DeleteDepartmentInput input)
        //{
        //    departmentManager.DeleteDepartment(input.Id);
        //}

        //public GetDepartmentOutput GetDepartmentById(GetDepartmentInput input)
        //{
        //    var getEmployee = departmentManager.GetDepartmentByID(input.Id);
        //    GetDepartmentOutput output = mapper.Map<Department, GetDepartmentOutput>(getEmployee);
        //    return output;
        //}

        //public IEnumerable<GetDepartmentOutput> ListAll()
        //{
        //    var getAll = departmentManager.GetAllList().ToList();
        //    List<GetDepartmentOutput> output = mapper.Map<List<Department>, List<GetDepartmentOutput>>(getAll);
        //    return output;
        //}

        //public void Update(UpdateDepartmentInput input)
        //{
        //    Department output = mapper.Map<UpdateDepartmentInput, Department>(input);
        //    departmentManager.UpdateDepartment(output);
        //}

        public Task<ListResultDto<PermissionDepartmentDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDepartmentDto>(
                ObjectMapper.Map<List<PermissionDepartmentDto>>(permissions).OrderBy(p => p.DepartmentName).ToList()
            ));
        }

        //protected override IQueryable<Department> CreateFilteredQuery(PagedDepartmentResultRequestDto input)
        //{
        //    return Repository.GetAllIncluding(x => x.Permissions)
        //        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
        //        || x.DisplayName.Contains(input.Keyword)
        //        || x.Description.Contains(input.Keyword));
        //}

        protected override IQueryable<Department> ApplySorting(IQueryable<Department> query, PagedDepartmentResultRequestDto input)
        {
            return query.OrderBy(r => r.DepartmentName);
        }



        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
