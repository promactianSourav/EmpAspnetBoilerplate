using Abp.AutoMapper;
using EmpAspnetBoilerplate.Authentication.External;

namespace EmpAspnetBoilerplate.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
