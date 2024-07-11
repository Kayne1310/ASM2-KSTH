using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASM2_KSTH.Controllers;
using ASM2_KSTH.Data;
using ASM2_KSTH.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using ASM2_KSTH.Helpers;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Net.Http;
using NuGet.DependencyResolver;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;


namespace ASM2_KSTH.Tests
{
    public class AdminsControllerTests
    {
        private readonly AdminsController _controller;
        private readonly ASM2_KSTHContext _context;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<IUrlHelperFactory> _mockUrlHelperFactory;

        public AdminsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ASM2_KSTHContext>()
                .UseInMemoryDatabase(databaseName: "KSTH")
                .Options;

            _context = new ASM2_KSTHContext(options);

            //SeedDatabase();

            _mockHttpContext = new Mock<HttpContext>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockUrlHelperFactory = new Mock<IUrlHelperFactory>();

            var mockMapper = new Mock<IMapper>(); // Mock IMapper
            _controller = new AdminsController(_context, mockMapper.Object);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "16"),
            new Claim(ClaimTypes.Role, "Admins"),
        };

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            _mockHttpContext
                .Setup(context => context.User)
                .Returns(userPrincipal);

            _mockHttpContext
                .Setup(context => context.RequestServices.GetService(typeof(IAuthenticationService)))
                .Returns(_mockAuthenticationService.Object);

             _mockHttpContext
                 .Setup(context => context.RequestServices.GetService(typeof(IUrlHelperFactory)))
                 .Returns(_mockUrlHelperFactory.Object);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _mockHttpContext.Object
            };

            var tempDataProvider = Mock.Of<ITempDataProvider>();
            _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), tempDataProvider);
        }

        //private void SeedDatabase()
        //{
        //    _context.Admins.RemoveRange(_context.Admins);
        //    _context.Roles.RemoveRange(_context.Roles);
        //    _context.SaveChanges();

        //    var admin = new Admin
        //    {
        //       Username="test",
        //       Password= "123",
        //       Id=20,
        //       RoleId=3

        //    };

        //    var admin2 = new Admin
        //    {
        //        Username = "admin",
        //        Password = "123",
        //        Id = 5,
        //        RoleId = 3
        //    };

        //    var role = new Roles
        //    {
        //        RoleId = 3,
        //        RoleName = "Admins"

        //    };

        //    _context.Admins.Add(admin);
        //    _context.Admins.Add(admin2);
        //    _context.Roles.Add(role);
        //    _context.SaveChanges();
        //}

        [Fact]
        public void Index_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Index_Post_InvalidUser_ReturnsViewWithErrorMessage()
        {
            // Arrange
            var model = new Admin { Username = "invaliduser", Password = "password" };

            // Act
            var result = await _controller.Index(model );

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Invalid username or password.", _controller.ViewBag.ErrorMessage);


        }
        [Fact]
        public async Task Index_Post_ValidUser_ReturnsRedirectToActionResult()
        {
            _context.Admins.RemoveRange(_context.Admins);
            _context.Roles.RemoveRange(_context.Roles);
            _context.SaveChanges();
            // Arrange
            var admin = new Admin
            {
                Username = "admin",
                Password = "123",
                RoleId = 3
            };
            var role = new Roles
            {
                RoleId = 3,
                RoleName = "Admins"

            };
            _context.Admins.Add(admin);
            _context.Roles.Add(role);
            _context.SaveChanges();
            // Act

            var model = new Admin { Username = "admin", Password = "123" };

            var result = await _controller.Index(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("DashBoard", redirectToActionResult.ActionName);
            Assert.Equal("Admins", redirectToActionResult.ControllerName);
        }

    }
}

