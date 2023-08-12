using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Trial.MvcDemoApplication.Pages;

public class Index_Tests : MvcDemoApplicationWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
