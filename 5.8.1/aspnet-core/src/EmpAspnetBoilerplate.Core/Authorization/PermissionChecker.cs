using Abp.Authorization;
using EmpAspnetBoilerplate.Authorization.Roles;
using EmpAspnetBoilerplate.Authorization.Users;

namespace EmpAspnetBoilerplate.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
