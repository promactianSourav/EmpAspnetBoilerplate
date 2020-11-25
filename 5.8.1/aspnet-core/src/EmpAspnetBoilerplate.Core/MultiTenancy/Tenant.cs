using Abp.MultiTenancy;
using EmpAspnetBoilerplate.Authorization.Users;

namespace EmpAspnetBoilerplate.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
