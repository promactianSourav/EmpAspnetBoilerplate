using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using EmpAspnetBoilerplate.Authorization.Users;

namespace EmpAspnetBoilerplate.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] RoleNames { get; set; }

        public string Address { get; set; }
        public string Qualification { get; set; }
        [Required(ErrorMessage = "Please fill the contact number.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Not a valid Phone number")]
        public string ContactNumber { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
