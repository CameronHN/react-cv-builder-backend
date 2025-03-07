using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

[TestFixture]
public class PositionIntegrationTest
{
    private DataContext _context;
    private PositionRepository? _positionRepository;
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void OneTimeSetUp()
    {

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new DataContext(options);

        _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDatabase")
                                   .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                        });

                        _context.Database.EnsureCreated();
                    });
                });

        _positionRepository = new PositionRepository(_context);
    }

    [TearDown]
    public void OneTimeTearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        _factory.Dispose();
        // Reset repository
        _positionRepository = null;
    }

    [Test]
    public async Task GetPositionsByUserIdAsync_UserExists_ReturnsList()
    {
        // Arrange
        var userId = 2;

        var positions = new List<PositionEntity>
            {
                new PositionEntity { Id = 1, UserId = 1, Role = "Developer", StartDate="2022-01-01", EndDate="2022-01-31" },
                new PositionEntity { Id = 2, UserId = 1, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"},
                new PositionEntity { Id = 3, UserId = 1, Role = "Developer", StartDate="2022-01-01", EndDate="2022-01-31" },
                new PositionEntity { Id = 4, UserId = userId, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"},
                new PositionEntity { Id = 5, UserId = userId, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"}
            };

        _context.Positions.AddRange(positions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _positionRepository.GetPositionsByUserIdAsync(userId);

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(2);
    }

    [Test]
    public async Task GetPositionsByUserIdAsync_UserNotExists_ReturnsEmptyList()
    {
        // Arrange
        var userId = 2;

        var positions = new List<PositionEntity>
            {
                new PositionEntity { Id = 1, UserId = 1, Role = "Developer", StartDate="2022-01-01", EndDate="2022-01-31" },
                new PositionEntity { Id = 2, UserId = 1, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"},
                new PositionEntity { Id = 3, UserId = 1, Role = "Developer", StartDate="2022-01-01", EndDate="2022-01-31" }
            };

        _context.Positions.AddRange(positions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _positionRepository.GetPositionsByUserIdAsync(userId);

        // Assert
        result.Should().BeEmpty();
        result.Count.Should().Be(0);
    }

    [Test]
    public async Task SearchPositionsByRoleAsync_RoleExists_ReturnsList()
    {
        // Arrange
        var searchString = "Developer";

        var positions = new List<PositionEntity>
            {
                new PositionEntity { Id = 1, UserId = 1, Role = "Software Developer", StartDate="2022-01-01", EndDate="2022-01-31" },
                new PositionEntity { Id = 2, UserId = 1, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"},
                new PositionEntity { Id = 3, UserId = 1, Role = "Mobile app developer", StartDate="2022-01-01", EndDate="2022-01-31" },
                new PositionEntity { Id = 4, UserId = 2, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"},
                new PositionEntity { Id = 5, UserId = 2, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"}
            };

        _context.Positions.AddRange(positions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _positionRepository.SearchPositionsByRoleAsync(searchString);

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(2);
        foreach (var item in result)
            Console.WriteLine($"Id: {item.Id}, Role: {item.Role}");
    }

    [Test]
    public async Task SearchPositionsByRoleAsync_RoleNotExists_ReturnsEmptyList()
    {
        // Arrange
        var searchString = "Engineer";

        var positions = new List<PositionEntity>
            {
                new PositionEntity { Id = 1, UserId = 1, Role = "Software Developer", StartDate="2022-01-01", EndDate="2022-01-31" },
                new PositionEntity { Id = 2, UserId = 1, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"},
                new PositionEntity { Id = 3, UserId = 1, Role = "Mobile app developer", StartDate="2022-01-01", EndDate="2022-01-31" },
                new PositionEntity { Id = 4, UserId = 2, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"},
                new PositionEntity { Id = 5, UserId = 2, Role = "Tester", StartDate="2023-01-01", EndDate="2024-01-01"}
            };

        _context.Positions.AddRange(positions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _positionRepository.SearchPositionsByRoleAsync(searchString);

        // Assert
        result.Should().BeEmpty();
        result.Should().NotBeNull();
        result.Count.Should().Be(0);
    }
}
