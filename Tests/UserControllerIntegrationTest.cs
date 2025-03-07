using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Tests;

[TestFixture]
public class UserControllerIntegrationTest
{
    private HttpClient _client;
    private DataContext? _context;
    private UserEntity _user;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new DataContext(options);

        var _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<DataContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Register the in-memory database for testing
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            });
        });

        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost:44366")
        });
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _context?.Database.EnsureDeleted();  // Delete the in-memory database after tests
        _context?.Dispose();  // Dispose of the DbContext to release resources
        _client.Dispose();  // Dispose of the HttpClient to release resources
    }

    // Set up a user object before each test
    [SetUp]
    public void SetUp()
    {
        _user = new UserEntity
        {
            Id = 1,
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com"
        };
    }

    // Helper method to post a user to the API
    private async Task<HttpResponseMessage> PostUserAsync(UserEntity user)
    {
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        return await _client.PostAsync("/api/user", content);
    }

    [Test]
    public async Task CreateUser_ValidUser_ReturnsCreated()
    {
        // Arrange
        var response = await PostUserAsync(_user);

        // Act
        var responseString = await response.Content.ReadAsStringAsync();
        var createdUser = JsonConvert.DeserializeObject<UserEntity>(responseString);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        createdUser.Should().NotBeNull();
        createdUser.FirstName.Should().Be(_user.FirstName);
    }

    [Test]
    public async Task CreateUser_MissingFirstName_ReturnsBadRequest()
    {
        // Arrange
        var invalidUser = new UserEntity
        {
            LastName = "Doe",
            Email = "jane.doe@example.com"
        };

        var content = new StringContent(JsonConvert.SerializeObject(invalidUser), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/user", content);

        // Assert
        response.Content.ReadAsStringAsync().Result.Should().Contain("The FirstName field is required.");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task CreateUser_InvalidEmail_ReturnsBadRequest()
    {
        // Arrange
        var invalidUser = new UserEntity
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doeexample.com"
        };

        var content = new StringContent(JsonConvert.SerializeObject(invalidUser), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/user", content);
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        responseString.Should().Contain("The Email field is not a valid e-mail address.");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task GetUserById_UserExists_ReturnsOk()
    {
        // Arrange
        await PostUserAsync(_user);

        // Act
        var response = await _client.GetAsync($"/api/user/{_user.Id}");
        var responseString = await response.Content.ReadAsStringAsync();
        var returnedUser = JsonConvert.DeserializeObject<UserEntity>(responseString);

        // Assert
        returnedUser.Should().NotBeNull();
        returnedUser.Id.Should().Be(_user.Id);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public async Task GetUserById_UserNotExists_ReturnsNotFound()
    {
        // Arrange
        int userId = 2;
        //await PostUserAsync(_user);

        // Act
        var response = await _client.GetAsync($"/api/user/{userId}");
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        responseString.Should().Contain($"User with the Id:{userId} not found.");
    }

    [Test]
    public async Task UpdateUser_UserExists_ReturnsOk()
    {
        // Arrange
        await PostUserAsync(_user);

        var updatedUser = new UserEntity
        {
            Id = _user.Id,
            FirstName = "Jacob",
            LastName = "Smith",
            Email = "jacob.smith@example.com"
        };

        var content = new StringContent(JsonConvert.SerializeObject(updatedUser), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PatchAsync($"/api/user/{updatedUser.Id}", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        updatedUser.Id.Should().Be(_user.Id);
        updatedUser.FirstName.Should().NotBe(_user.FirstName);
    }

    [Test]
    public async Task UpdateUser_UserNotExists_ReturnsNotFound()
    {
        // Arrange
        //await PostUserAsync(_user);

        var updatedUser = new UserEntity
        {
            Id = 2,
            FirstName = "Jacob",
            LastName = "Smith",
            Email = "jacob.smith@example.com"
        };

        var content = new StringContent(JsonConvert.SerializeObject(updatedUser), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PatchAsync($"/api/user/{updatedUser.Id}", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task DeleteUser_UserExists_ReturnsSuccess()
    {
        // Arrange
        await PostUserAsync(_user);

        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/user/DeleteUser/{_user.Id}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public async Task DeleteUser_UserNotExists_ReturnsNotFound()
    {
        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/user/DeleteUser/1");
        var responseString = await deleteResponse.Content.ReadAsStringAsync();

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        responseString.Should().Contain("User with Id:1 not found.");
    }
}
