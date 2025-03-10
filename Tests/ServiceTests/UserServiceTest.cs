using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Tests.ServiceTests;

[TestFixture]
public class UserServiceTest
{
    private Mock<IRepository<UserEntity>> _mockUserRepository = null!;
    private UserService _userService = null!;

    [SetUp]
    public void SetUp()
    {
        _mockUserRepository = new Mock<IRepository<UserEntity>>();
        _userService = new UserService(_mockUserRepository.Object);
    }

    [Test]
    public async Task AddUserAsync_ValidUser_VerifyUserAdded()
    {
        // Arrange
        // Creating a user entity with valid data
        var user = new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

        // Act
        // Calling the service method to add the user
        await _userService.AddUserAsync(user);

        // Assert
        // Verify that the repository method was called exactly once
        _mockUserRepository.Verify(repo => repo.AddRecordAsync(user), Times.Once);
    }

    [Test]
    public void AddUserAsync_InvalidEmail_CatchEmailValidation()
    {
        // Arrange
        // Creating a user entity with an invalid email address
        var user = new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doeexample.com" };

        var validationContext = new ValidationContext(user);
        var validationResults = new List<ValidationResult>();

        // Act
        // Validating the user entity
        var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

        // Assert
        // Checking that the validation failed and the correct validation error message is present
        isValid.Should().BeFalse();
        validationResults.Should().ContainSingle(v => v.ErrorMessage == "The Email field is not a valid e-mail address.");
    }

    [Test]
    public async Task GetAllAsync_UsersExist_ReturnsAllUsers()
    {
        // Arrange
        // Setting up a list of users
        var users = new List<UserEntity>
        {
            new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
            new UserEntity { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
        };

        // Mocking the repository to return the list of users
        _mockUserRepository.Setup(repo => repo.GetAllRecordsAsync()).ReturnsAsync(users);

        // Act
        // Calling the service method to get all users
        var result = await _userService.GetAllAsync();

        // Assert
        // Verifying the result contains the expected number of users
        result.Count().Should().Be(2);
    }

    [Test]
    public async Task GetUserByIdAsync_UserExists_ReturnsUser()
    {
        // Arrange
        // Creating a user entity
        var user = new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

        // Mocking the repository to return the user when requested by ID
        _mockUserRepository.Setup(repo => repo.GetRecordByIdAsync(1)).ReturnsAsync(user);

        // Act
        // Calling the service method to get the user by ID
        var result = await _userService.GetUserByIdAsync(1);

        // Assert
        // Verifying the result matches the expected user
        result.Should().Be(user);
    }

    [Test]
    public async Task UpdateUserAsync_UserExists_VerifyUpdate()
    {
        // Arrange
        // Creating a user entity
        var user = new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
        var updatedUserDto = new UserUpdateDto { Id = 1, FirstName = "Johnny" };

        // Mocking the repository to return the user when requested by ID
        _mockUserRepository.Setup(repo => repo.GetRecordByIdAsync(1)).ReturnsAsync(user);

        // Act
        // Calling the service method to update the user
        await _userService.UpdateUserAsync(updatedUserDto);

        // Assert
        // Verifying the update method was called exactly once
        _mockUserRepository.Verify(repo => repo.UpdateRecordAsync(It.IsAny<UserEntity>()), Times.Once);
    }

    [Test]
    public async Task DeleteUserAsync_UserExists_VerifyDelete()
    {
        // Arrange
        // Mocking the repository to return true when delete is called with ID 1
        _mockUserRepository.Setup(repo => repo.DeleteRecordAsync(1)).ReturnsAsync(true);

        // Act
        // Calling the service method to delete the user
        await _userService.DeleteUserAsync(1);

        // Assert
        // Verifying the delete method was called exactly once
        _mockUserRepository.Verify(repo => repo.DeleteRecordAsync(1), Times.Once);
    }
}
