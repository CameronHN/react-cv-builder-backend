using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests
{
    [TestFixture]
    public class UserRepositoryTest
    {
        // Mock DbSet for UserEntity, used to simulate interaction with the table
        private Mock<DbSet<UserEntity>> _mockDbSet;

        // Mock DataContext to simulate interaction with the actual database context
        private Mock<DataContext> _mockContext;

        private Repository<UserEntity> _repository;

        // Setup method executed before each test
        [SetUp]
        public void SetUp()
        {
            // Creating a mock instance of DbSet<UserEntity> which simulates the DbSet<UserEntity> for the Users table
            _mockDbSet = new Mock<DbSet<UserEntity>>();

            // Creating a mock instance of the DataContext which will simulate the actual database context
            _mockContext = new Mock<DataContext>();

            // Set up the mock context
            _mockContext.Setup(c => c.Set<UserEntity>()).Returns(_mockDbSet.Object);

            // Setting up the SaveChangesAsync method of the context to return a successful result (1)
            // This simulates saving changes to the database and indicates that one record was saved
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Initializing the repository instance with the mocked DataContext
            _repository = new Repository<UserEntity>(_mockContext.Object);
        }

        [Test]
        public async Task AddRecordAsync_ValidUser_VerifyUserAdded()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890"
            };

            // Setup the AddAsync to add the entity to the list
            var mockUserList = new List<UserEntity>();
            _mockDbSet.Setup(dbSet => dbSet.AddAsync(It.IsAny<UserEntity>(), default))
                      .Callback<UserEntity, CancellationToken>((u, ct) => mockUserList.Add(u));

            // Act
            await _repository.AddRecordAsync(user);

            // Assert
            Assert.That(mockUserList.Contains(user), Is.True);
            _mockDbSet.Verify(dbSet => dbSet.AddAsync(It.IsAny<UserEntity>(), default), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task GetUserById_UserExists_VerifyUserExist()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Simulate fetching the user by ID
            _mockDbSet.Setup(dbSet => dbSet.FindAsync(It.IsAny<int>())).ReturnsAsync(user);

            // Act
            var result = await _repository.GetRecordByIdAsync(user.Id);

            // Assert
            Assert.That(user, Is.EqualTo(result));
            Assert.That(result, Is.Not.Null);
            _mockDbSet.Verify(dbSet => dbSet.FindAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task UpdateUserAsync_UserExists_VerifyUpdate()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Setup the Update method of the mock DbSet
            _mockDbSet.Setup(dbSet => dbSet.Update(It.IsAny<UserEntity>())).Callback<UserEntity>(u => user = u);

            // Act
            await _repository.UpdateRecordAsync(user);

            // Assert
            _mockDbSet.Verify(dbSet => dbSet.Update(It.IsAny<UserEntity>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteRecordAsync_UserExists_VerifyDelete()
        {
            // Arrange
            var user = new UserEntity
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Simulate fetching the user by ID
            _mockDbSet.Setup(dbSet => dbSet.FindAsync(It.Is<int>(id => id == 1))).ReturnsAsync(user);

            // Ensure the context returns the mocked DbSet
            _mockContext.Setup(c => c.Set<UserEntity>()).Returns(_mockDbSet.Object);

            // Act
            var result = await _repository.DeleteRecordAsync(user.Id);

            // Assert
            Assert.That(result, Is.True);
            _mockDbSet.Verify(dbSet => dbSet.FindAsync(user.Id), Times.Once);
            _mockContext.Verify(c => c.Remove(It.Is<UserEntity>(u => u.Id == user.Id)), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
    }
}
