using api.Controllers;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

[TestFixture]
public class UserControllerTest
{
    private Mock<IUserService> _mockUserService = null!;
    private UserController _userController = null!;

    [SetUp]
    public void Setup()
    {
        _mockUserService = new Mock<IUserService>();
        _userController = new UserController(_mockUserService.Object);
    }

    // =================================
    // ======= CREATE USER TESTS =======
    // =================================

    [Test]
    public async Task AddUser_ValidUser_ReturnsOkResult()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com"
        };

        _mockUserService.Setup(service => service.AddUserAsync(user)).Returns(Task.CompletedTask);

        // Act
        var result = await _userController.AddUser(user);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>()
              .Which.StatusCode.Should().Be(201);

        result.As<CreatedAtActionResult>().Value.Should().Be(user);

        _mockUserService.Verify(service => service.AddUserAsync(user), Times.Once);
    }

    [Test]
    public async Task AddUser_NullUser_ReturnsBadRequest()
    {
        // Act
        var result = await _userController.AddUser(null);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>()
              .Which.StatusCode.Should().Be(400);

        result.As<BadRequestObjectResult>().Value.Should().Be("Invalid user data.");
    }

    // =================================
    // ======= GET BY ID TESTS =========
    // =================================

    [Test]
    public async Task GetUser_UserExists_ReturnsOkResult()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com"
        };

        _mockUserService.Setup(service => service.GetUserByIdAsync(1)).ReturnsAsync(user);

        // Act
        var result = await _userController.GetUser(1);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        result.Result.As<OkObjectResult>().Value.Should().Be(user);
        _mockUserService.Verify(service => service.GetUserByIdAsync(user.Id), Times.Once);
    }

    [Test]
    public async Task GetUser_UserNotExists_ReturnsNotFound()
    {
        // Arrange
        _mockUserService.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _userController.GetUser(99);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be(404);

        result.Result.As<NotFoundObjectResult>().Value.Should().BeEquivalentTo(new { message = "User with the Id:99 not found." });
    }

    // =================================
    // ======== GET ALL USERS ==========
    // =================================

    [Test]
    public async Task GetUsers_Users_ReturnOk()
    {
        // Arrange
        var users = new List<UserEntity>
            {
                new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new UserEntity { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
            };

        _mockUserService.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _userController.GetAll();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);

        result.Result.As<OkObjectResult>().Value.Should().Be(users);

        _mockUserService.Verify(service => service.GetAllAsync(), Times.Once);
    }

    // =================================
    // ========= UPDATE USER ===========
    // =================================

    [Test]
    public async Task UpdateUser_UserExists_ReturnsOk()
    {
        // Arrange
        var userDto = new UserUpdateDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

        _mockUserService.Setup(x => x.UpdateUserAsync(It.IsAny<UserUpdateDto>())).Returns(Task.CompletedTask);

        // Act
        var result = await _userController.UpdateUser(1, userDto);

        // Assert
        result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);

        result.As<OkObjectResult>().Value.Should().BeEquivalentTo(new { message = $"Successfully updated user with Id:{userDto.Id}" });

        _mockUserService.Verify(service => service.UpdateUserAsync(userDto), Times.Once);
    }

    [Test]
    public async Task UpdateUser_IdNotMatches_ReturnsBadRequest()
    {
        // Arrange
        var userDto = new UserUpdateDto { Id = 2, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
        var differentId = userDto.Id + 1;

        // Act
        var result = await _userController.UpdateUser(differentId, userDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);

        result.As<BadRequestObjectResult>().Value.Should().BeEquivalentTo(new { message = "The provided Id does not match the User Id" });
    }

    // =================================
    // ========= DELETE USER ===========
    // =================================

    [Test]
    public async Task DeleteUser_UserExists_ReturnsOkResult()
    {
        // Arrange
        var userId = 1;

        _mockUserService.Setup(x => x.DeleteUserAsync(userId)).Returns(Task.CompletedTask);

        // Act
        var result = await _userController.DeleteUser(userId);

        // Assert
        result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);

        result.As<OkObjectResult>().Value.Should().BeEquivalentTo(new { message = $"Successfully deleted user with Id:{userId}" });

        _mockUserService.Verify(service => service.DeleteUserAsync(userId), Times.Once);
    }

    [Test]
    public async Task DeleteUser_UserNotExists_ReturnsNotFound()
    {
        // Arrange
        _mockUserService.Setup(x => x.DeleteUserAsync(It.IsAny<int>()))
                        .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _userController.DeleteUser(99);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be(404);

        result.As<NotFoundObjectResult>().Value.Should().BeEquivalentTo(new { message = "User with Id:99 not found." });
    }
}
