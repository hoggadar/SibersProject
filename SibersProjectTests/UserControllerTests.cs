using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SibersProjectBusiness.DTOs.User;
using SibersProjectBusiness.Interfaces;
using SibersProjectBusiness.Mappers;
using SibersProjectWeb.Controllers;

namespace SibersProjectTests
{
    public class UserControllerTests
    {
        private UserController _controller;
        private Mock<IUserService> _userServiceMock;
        private IMapper _mapper;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _mapper = CreateMapper();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task CreateUser_ValidDto_CallsServiceAndReturnsCreated()
        {
            var createUserDto = new CreateUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Patronymic = "Smith",
                Email = "john.doe@example.com",
                Password = "SecurePassword123",
                Role = "Director"
            };

            var createdUserDto = new UserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Patronymic = "Smith",
                Email = "john.doe@example.com",
                Role = "Director"
            };

            _userServiceMock.Setup(service => service.Create(createUserDto))
                .ReturnsAsync(createdUserDto);

            var result = await _controller.Create(createUserDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserDto>(okResult.Value);

            Assert.Equal(createdUserDto.Email, returnedUser.Email);
            Assert.Equal(createdUserDto.FirstName, returnedUser.FirstName);
            Assert.Equal(createdUserDto.LastName, returnedUser.LastName);
            Assert.Equal(createdUserDto.Role, returnedUser.Role);

            _userServiceMock.Verify(service => service.Create(It.IsAny<CreateUserDto>()), Times.Once);
        }

        private IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });
            return configuration.CreateMapper();
        }
    }
}
