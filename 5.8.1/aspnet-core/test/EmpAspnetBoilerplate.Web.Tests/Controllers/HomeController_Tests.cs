using System.Threading.Tasks;
using EmpAspnetBoilerplate.Models.TokenAuth;
using EmpAspnetBoilerplate.Web.Controllers;
using Shouldly;
using Xunit;

namespace EmpAspnetBoilerplate.Web.Tests.Controllers
{
    public class HomeController_Tests: EmpAspnetBoilerplateWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}