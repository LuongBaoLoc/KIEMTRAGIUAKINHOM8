using System.Threading.Tasks;
using KTGKNhom8.Models.TokenAuth;
using KTGKNhom8.Web.Controllers;
using Shouldly;
using Xunit;

namespace KTGKNhom8.Web.Tests.Controllers
{
    public class HomeController_Tests: KTGKNhom8WebTestBase
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