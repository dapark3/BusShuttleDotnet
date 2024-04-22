using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Tests.Controller.Tests
{
    public class HomeControllerTests
    {
        private static readonly Mock<ClaimsPrincipal> mockPrincipalService = new();
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            ControllerContext context = InitializeTestContext();
            _controller = new HomeController() {
                ControllerContext = context,
            };
        }

        private static ControllerContext InitializeTestContext()
        {
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.User).Returns(mockPrincipalService.Object);
            return new ControllerContext() { 
                HttpContext = mockHttpContext.Object 
            };
        }

        [Fact]
        public void Index_Get()
        {
            mockPrincipalService.Setup(m => m.IsInRole("Manager")).Returns(true);
            var result = (RedirectToActionResult) _controller.Index();
            Assert.Equal("Manager", result.ActionName);
        }
    }
}