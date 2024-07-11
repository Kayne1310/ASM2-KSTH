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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Routing;


namespace ASM2_KSTH.Tests
{
    public class StudentsControllerTests
    {
        private readonly StudentsController _controller;
        private readonly ASM2_KSTHContext _context;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<IUrlHelperFactory> _mockUrlHelperFactory;

        public StudentsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ASM2_KSTHContext>()
                .UseInMemoryDatabase(databaseName: "KSTH")
                .Options;

            _context = new ASM2_KSTHContext(options);

            //StudentDB();

            _mockHttpContext = new Mock<HttpContext>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockUrlHelperFactory = new Mock<IUrlHelperFactory>();

            _controller = new StudentsController(_context);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "16"),
                    new Claim(ClaimTypes.Name, "janesmith"),
                    new Claim(ClaimTypes.Role, "Students"),

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
            _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        }

        //private void StudentDB()

        //{
        //    _context.Students.RemoveRange(_context.Students);
        //    _context.Roles.RemoveRange(_context.Roles);
        //    _context.SaveChanges();

        //    var student = new Student
        //    {
        //        StudentId = 20,
        //        Name = "John Doe",
        //        DateOfBirth = new DateTime(1995, 5, 15), 
        //        Address = "123 Main St",
        //        PhoneNumber = "0967327356",
        //        Email = "john.doe@example.com",
        //        MajorId = 1, 
        //        Username = "validuser",
        //        Password = "password".ToMd5Hash("randomKey1"), 
        //        RandomKey = "randomKey1",
        //        RoleId = 1 
        //    };

        //    var student2 = new Student
        //    {
        //        StudentId = 17,
        //        Name = "Jane Smith",
        //        DateOfBirth = new DateTime(1996, 5, 15),
        //        Username = "janesmithjk",
        //        Password = "janespassword".ToMd5Hash("randomKey2"),
        //        Address = "456 Elm Stdhgf",
        //        Email = "jane.smith@example.comfgh",
        //        PhoneNumber = "098-765-4321543",
        //        RandomKey = "randomKey2",
        //        MajorId = 1,
                
        //    };
        //    var role = new Roles
        //    {
        //        RoleId = 2,
        //        RoleName = "Teacher"

        //    };
        //    _context.Students.Add(student);
        //    _context.Students.Add(student2);
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
            var model = new Student { Username = "invaliduser", Password = "password" };

            // Act
            var result = await _controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Invalid username or password.", _controller.ViewBag.ErrorMessage);
            

        }

        [Fact]
        public async Task Index_Post_ValidUser_ReturnsRedirectToActionResult()
        {
            //_context.Students.RemoveRange(_context.Students);
            //_context.Roles.RemoveRange(_context.Roles);
            //_context.SaveChanges();

            var student = new Student
            {
                StudentId = 20,
                Name = "John Doe",
                DateOfBirth = new DateTime(1995, 5, 15),
                Address = "123 Main St",
                PhoneNumber = "0967327356",
                Email = "john.doe@example.com",
                MajorId = 1,
                Username = "validuser333",
                Password = "password".ToMd5Hash("randomKey1"),
                RandomKey = "randomKey1",
                RoleId = 1
            };

            var role = new Roles
            {
                RoleId = 1,
                RoleName = "Student"

            };
            _context.Students.Add(student);
            _context.Roles.Add(role);
            _context.SaveChanges();
            // Arrange
            var model = new Student { Username = "validuser333", Password = "password" };

            // Act
            var result = await _controller.Index(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("DashBoard", redirectToActionResult.ActionName);
        }
    }
}

