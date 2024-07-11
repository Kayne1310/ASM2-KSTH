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

namespace ASM2_KSTH.Tests
{
    public class TeachersControllerTests
    {
        private readonly TeachersController _controller;
        private readonly ASM2_KSTHContext _context;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<IUrlHelperFactory> _mockUrlHelperFactory;
        public TeachersControllerTests()
        {
            var options = new DbContextOptionsBuilder<ASM2_KSTHContext>()
             .UseInMemoryDatabase(databaseName: "KSTH")
             .Options;

            _context = new ASM2_KSTHContext(options);

            //TeacherDB();

            _mockHttpContext = new Mock<HttpContext>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockUrlHelperFactory = new Mock<IUrlHelperFactory>();

            _controller = new TeachersController(_context);

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, "16"),
            new Claim(ClaimTypes.Name, "janesmith"),
            new Claim(ClaimTypes.Role, "Teachers"),
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

        //private void TeacherDB()

        //{
        //    _context.Teachers.RemoveRange(_context.Teachers);
        //    //_context.Roles.RemoveRange(_context.Roles);
        //    _context.SaveChanges();

        //    var teacher = new Teacher
        //    {
        //        TeacherId = 16,
        //        Name = "John Doe",
        //        Username = "validuser",
        //        Password = "password".ToMd5Hash("randomKey1"),
        //        Address = "123 Main St",
        //        Email = "john.doe@example.com",
        //        PhoneNumber = "123-456-7890",
        //        RandomKey = "randomKey1",
        //        RoleId = 2
        //    };

        //    var teacher2 = new Teacher
        //    {
        //        TeacherId = 17,
        //        Name = "Jane Smith",
        //        Username = "janesmith",
        //        Password = "janespassword".ToMd5Hash("randomKey2"),
        //        Address = "456 Elm St dfg",
        //        Email = "jane.smith@exampledfg.com",
        //        PhoneNumber = "098-765-4321dfg",
        //        RandomKey = "randomKey2",
        //        RoleId = 2
        //    };

        //    var role = new Roles
        //    {
        //        RoleId = 2,
        //        RoleName = "Teacher"

        //    };


        //    _context.Teachers.Add(teacher);
        //    _context.Teachers.Add(teacher2);
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
             var model = new Teacher { Username = "invaliduseafsrz", Password = "passwordasf" };

            // Act
            var result = await _controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Invalid username or password.", _controller.ViewBag.ErrorMessage);
            Assert.Equal("Invalid username or password.", _controller.TempData["no"]);

        }

        [Fact]
        public async Task Index_Post_ValidUser_ReturnsRedirectToActionResult()
        {
            //_context.Teachers.RemoveRange(_context.Teachers);
            //_context.Roles.RemoveRange(_context.Roles);
            //_context.SaveChanges();

            var teacher = new Teacher
            {
                TeacherId = 16,
                Name = "John Doe",
                Username = "validuser",
                Password = "password".ToMd5Hash("randomKey1"),
                Address = "123 Main St",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                RandomKey = "randomKey1",
                RoleId = 2
            };
            var role = new Roles
            {
                RoleId = 2,
                RoleName = "Teacher"

            };
            _context.Teachers.Add(teacher);
            _context.Roles.Add(role);
            _context.SaveChanges();
            // Arrange
            var model = new Teacher { Username = "validuser", Password = "password" };

            // Act
            var result = await _controller.Index(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("DashBoard", redirectToActionResult.ActionName);
        }
    }
}

