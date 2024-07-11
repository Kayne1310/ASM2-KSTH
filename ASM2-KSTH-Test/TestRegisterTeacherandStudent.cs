using ASM2_KSTH.Controllers;
using ASM2_KSTH.Data;
using ASM2_KSTH.Helpers;
using ASM2_KSTH.Models;
using ASM2_KSTH.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASM2_KSTH_Test
{
    public class TestRegisterTeacherandStudent
    {
        private readonly AdminsController _controller;
        private readonly ASM2_KSTHContext _context;
        private readonly Mock<IMapper> _mapperMock;

        public TestRegisterTeacherandStudent()
        {
            var options = new DbContextOptionsBuilder<ASM2_KSTHContext>()
                .UseInMemoryDatabase(databaseName: "KSTH")
                .Options;

            _context = new ASM2_KSTHContext(options);

            //SeedDatabase();

            _mapperMock = new Mock<IMapper>();

            _controller = new AdminsController(_context, _mapperMock.Object);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "16"),
                    new Claim(ClaimTypes.Name, "janesmith"),
                    new Claim(ClaimTypes.Role, "Admins"),
                };

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };
            var tempDataProvider = Mock.Of<ITempDataProvider>();
            _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        }

        //private void SeedDatabase()
        //{
        //    _context.Students.RemoveRange(_context.Students); // xoa du lieu 
        //    _context.Roles.RemoveRange(_context.Roles);
        //    _context.Majors.RemoveRange(_context.Majors);
        //    _context.SaveChanges(); // du lieu = null -> test dang nhap (k co account de login)

        //    var role = new Roles
        //    {
        //        RoleId = 1,
        //        RoleName = "Students"
        //    };

        //    var major = new Major
        //    {
        //        MajorId = 1,
        //        MajorName = "Computer Science"
        //    };

        //    _context.Roles.Add(role);
        //    _context.Majors.Add(major);
        //    _context.SaveChanges();
        //}

        [Fact]
        public void SignupST_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.SignupST(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SignupST_Post_ExistingUsername_ReturnsViewWithErrorMessage()
        {
            _context.Students.RemoveRange(_context.Students);
            _context.SaveChanges();
            // Arrange
            var existingStudent = new Student
            {
                Username = "existinguser",
                Password = "password".ToMd5Hash("randomkey"),
                RandomKey = "randomkey",
                Name = "Existing User",
                DateOfBirth = new DateTime(2000, 1, 1),
                Address = "123 Main St",
                PhoneNumber = "123456789",
                Email = "existinguser@example.com",
                RoleId = 1
            };

            _context.Students.Add(existingStudent);
            _context.SaveChanges();

            var model = new StudentRegister
            {
                Username = "existinguser",
                Password = "password",
                MajorName = "Computer Science",
                Name = "New User",
                DateOfBirth = new DateTime(2024, 7, 13),
                Address = "456 Elm St",
                PhoneNumber = "987654321",
                Email = "newuser@example.com",
                RoleId = 1
            };

            // Act
            var result = _controller.SignupST(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Username already exists. Please choose a different username.", _controller.ViewBag.ErrorMessage);
        }


        [Fact]
        public void SignupST_Post_SuccessfulRegistration_ReturnsRedirectToActionResult()
        {
            _context.Majors.RemoveRange(_context.Majors);
            _context.SaveChanges();
            // Arrange
            var model = new StudentRegister
            { 
                Username = "bbbb2",
                Password = "123",
                MajorName = "Computer Science",
                Name = "John Doead",
                DateOfBirth = new DateTime(2024, 07, 13),
                Address = "fgdsgfdgs",
                PhoneNumber = "096727356",
                Email = "johndoe@gmail.com",
                RoleId = 1

            };
            var major = new Major
            {
                MajorId = 1,
                MajorName = "Computer Science",

            };
            _context.Majors.Add(major);
            _context.SaveChanges();       
            _mapperMock.Setup(m => m.Map<Student>(It.IsAny<StudentRegister>())).Returns(new Student());
            // Ensure ModelState is valid
            _controller.ModelState.Clear();
            // Act
            var result = _controller.SignupST(model);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = (RedirectToActionResult)result;
            Assert.Equal("AdminPage", redirectResult.ActionName);
            Assert.Equal("Admins", redirectResult.ControllerName);
        }


        [Fact]
        public void SignupTE_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.SignupTE();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SignupTE_Post_ExistingUsername_ReturnsViewWithErrorMessage()
        {
            _context.Teachers.RemoveRange(_context.Teachers);
            _context.SaveChanges();
            // Arrange
            var teacher = new Teacher
            {
                Username = "existinguser",
                Password = "123",
                RandomKey = "randomKey",
                RoleId = 2,
                Address = "456 Elm St",
                Email = "jane.smith@example.com",
                PhoneNumber = "0987654321",
                Name="gdhfgh"
            };

            _context.Teachers.Add(teacher);
            _context.SaveChanges();

            var model = new Teacher { Username = "existinguser", Password = "password" };

            // Act
            var result = _controller.SignupTE(model, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Username already exists. Please choose a different username.", _controller.ViewBag.ErrorMessage);
        }

        [Fact]
        public void SignupTE_Post_SuccessfulRegistration_ReturnsRedirectToActionResult()
        {
            var model = new Teacher
            {
                Username = "TTT1",
                Password = "123",      
                Name = "vtoan",
                Address = "bn99abc",
                PhoneNumber = "096727356",
                Email = "teacher99@gmail.com",
                RoleId = 2

            };

            
            _context.SaveChanges();        
            // Ensure ModelState is valid
            _controller.ModelState.Clear();
            // Act
            var result = _controller.SignupTE(model, null);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);

            var redirectResult = (RedirectToActionResult)result;
            Assert.Equal("AdminPage", redirectResult.ActionName);
            Assert.Equal("Admins", redirectResult.ControllerName);
        }
    }
}
