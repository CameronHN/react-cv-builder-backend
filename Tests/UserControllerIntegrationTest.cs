using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace Tests;

[TestFixture]
public class UserControllerIntegrationTest : WebApplicationFactory<Program>
{
    private HttpClient _client;
    private DataContext? _context;

    [SetUp]
    public void SetUp()
    {
        _client = CreateClient();
        using var scope = Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<DataContext>();
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task CreateUser_Should_Add_User()
    {
        // Arrange
        var user = new UserEntity
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/user/", content); // Adjust the URL based on your API route
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var createdUser = JsonConvert.DeserializeObject<UserEntity>(responseString);

        // Assert
        Assert.That(user.FirstName, Is.EqualTo(createdUser.FirstName));
        Assert.That(user.LastName, Is.EqualTo(createdUser.LastName));
        Assert.That(user.Email, Is.EqualTo(createdUser.Email));
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
    }

    [Test]
    public async Task GetUserById_UserExists_ReturnsUser()
    {
        // Arrange
        var user = new UserEntity
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com"
        };

        using (var scope = Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        // Act
        var response = await _client.GetAsync($"/api/user/{user.Id}");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var returnedUser = JsonConvert.DeserializeObject<UserEntity>(responseString);

        // Assert
        Assert.That(user.FirstName, Is.EqualTo(returnedUser.FirstName));
        Assert.That(user.LastName, Is.EqualTo(returnedUser.LastName));
        Assert.That(user.Email, Is.EqualTo(returnedUser.Email));
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
    }

    [Test]
    public async Task GetUserById_UserNotExists_ReturnsNotFound()
    {
        // Arrange
        using (var scope = Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        }

        // Act
        var response = await _client.GetAsync($"/api/user/9999");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
    }

    [Test]
    public async Task UpdateUser_Should_Update_User()
    {
        // Arrange
        var user = new UserEntity
        {
            FirstName = "Jake",
            LastName = "Smith",
            Email = "jake.smith@example.com"
        };

        using (var scope = Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            dbContext.Users.Add(user); // Ensure you add the user to the context
            dbContext.SaveChanges();
        }

        var updatedUser = new UserEntity
        {
            Id = user.Id,
            FirstName = "Jacob", // Updating first name
            LastName = "Smith",
            Email = "jacob.smith@example.com" // Updating email
        };
        var content = new StringContent(JsonConvert.SerializeObject(updatedUser), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PatchAsync($"/api/user/{updatedUser.Id}", content); // Adjust the URL based on your API route
        response.EnsureSuccessStatusCode();

        // Assert
        var responseString = await response.Content.ReadAsStringAsync();
        var returnedUser = JsonConvert.DeserializeObject<UserEntity>(responseString);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(updatedUser.Id, Is.EqualTo(user.Id));
            Assert.That(updatedUser.FirstName, Is.Not.EqualTo(user.FirstName));
            Assert.That(updatedUser.LastName, Is.EqualTo(user.LastName));
            Assert.That(updatedUser.Email, Is.Not.EqualTo(user.Email));
        }
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
    }

    [Test]
    public async Task DeleteUser_Should_Remove_User()
    {
        // Arrange
        var user = new UserEntity
        {
            FirstName = "Alice",
            LastName = "Johnson",
            Email = "alice.johnson@example.com"
        };

        using (var scope = Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            dbContext.Users.Add(user); // Ensure you add the user to the context
            dbContext.SaveChanges();
        }

        // Act
        var response = await _client.DeleteAsync($"/api/user/DeleteUser/{user.Id}");
        response.EnsureSuccessStatusCode();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
    }
}
