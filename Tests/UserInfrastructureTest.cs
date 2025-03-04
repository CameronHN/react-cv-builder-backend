using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests;

[TestFixture]
public class UserInfrastructureTest
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
    public async Task AddUserAsync_UserProvided_ShouldCallRepository_AddRecordAsync()
    {
        var user = new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

        await _userService.AddUserAsync(user);

        _mockUserRepository.Verify(repo => repo.AddRecordAsync(user), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_UsersExist_ReturnsAllUsers()
    {
        var users = new List<UserEntity>
            {
                new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new UserEntity { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            };

        _mockUserRepository.Setup(repo => repo.GetAllRecordsAsync()).ReturnsAsync(users);

        var result = await _userService.GetAllAsync();

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetUserByIdAsync_ValidIdProvided_ReturnsCorrectUser()
    {
        var user = new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
        _mockUserRepository.Setup(repo => repo.GetRecordByIdAsync(1)).ReturnsAsync(user);

        var result = await _userService.GetUserByIdAsync(1);

        Assert.That(result, Is.EqualTo(user));
    }

    [Test]
    public async Task UpdateUserAsync_UserExists_UpdatesUser()
    {
        var user = new UserEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
        var updatedUserDto = new UserUpdateDto { Id = 1, FirstName = "Johnny" };
        _mockUserRepository.Setup(repo => repo.GetRecordByIdAsync(1)).ReturnsAsync(user);

        await _userService.UpdateUserAsync(updatedUserDto);

        _mockUserRepository.Verify(repo => repo.UpdateRecordAsync(It.IsAny<UserEntity>()), Times.Once);
    }

    [Test]
    public async Task DeleteUserAsync_UserExists_DeletesUser()
    {
        _mockUserRepository.Setup(repo => repo.DeleteRecordAsync(1)).ReturnsAsync(true);

        await _userService.DeleteUserAsync(1);

        _mockUserRepository.Verify(repo => repo.DeleteRecordAsync(1), Times.Once);
    }
}
