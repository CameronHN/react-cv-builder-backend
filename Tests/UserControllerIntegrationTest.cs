using Domain.Entities;
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

    private async Task<HttpResponseMessage> PostUserAsync(UserEntity user)
    {
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        return await _client.PostAsync("/api/user", content);
    }

    [Test]
    public async Task CreateUser_Should_Add_User()
    {
        // Arrange
        var response = await PostUserAsync(_user);

        // Act
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var createdUser = JsonConvert.DeserializeObject<UserEntity>(responseString);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(createdUser, Is.Not.Null);
        Assert.That(createdUser.FirstName, Is.EqualTo(_user.FirstName));
    }

    [Test]
    public async Task GetUserById_UserExists_ReturnsUser()
    {
        // Arrange
        await PostUserAsync(_user);

        // Act
        var response = await _client.GetAsync($"/api/user/{_user.Id}");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var returnedUser = JsonConvert.DeserializeObject<UserEntity>(responseString);

        // Assert
        Assert.That(returnedUser, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetUserById_UserNotExists_ReturnsNotFound()
    {
        // Arrange
        await PostUserAsync(_user);

        // Act
        var response = await _client.GetAsync($"/api/user/2");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task UpdateUser_Should_Update_User()
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
        response.EnsureSuccessStatusCode();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(updatedUser, Is.Not.EqualTo(_user));
        Assert.That(updatedUser.FirstName, Is.Not.EqualTo(_user.FirstName));
    }

    [Test]
    public async Task DeleteUser_Should_Remove_User()
    {
        // Arrange
        await PostUserAsync(_user);

        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/user/DeleteUser/{_user.Id}");
        var getUserResponse = await _client.GetAsync($"/api/user/1");
        var responseString = await getUserResponse.Content.ReadAsStringAsync();

        // Assert
        Assert.That(getUserResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(responseString, Does.Contain("User with the Id:1 not found."));
    }
}
