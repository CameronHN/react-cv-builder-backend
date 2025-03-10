using api.Controllers;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.ControllerTests
{
    [TestFixture]
    public class UserSkillControllerTest
    {
        private readonly UserSkillController _controller;
        private readonly Mock<IUserSkillService> _mockService;

        public UserSkillControllerTest()
        {
            _mockService = new Mock<IUserSkillService>();
            _controller = new UserSkillController(_mockService.Object);
        }

        [Test]
        public async Task GetSkillsByUserId_UserHasSkills_ReturnsOkResultWithSkills()
        {
            // Arrange
            var userId = 1;
            var userSkills = new List<UserSkillEntity>
                {
                    new UserSkillEntity { Skill = new SkillEntity { SkillName = "C#" } },
                    new UserSkillEntity { Skill = new SkillEntity { SkillName = "JavaScript" } }
                };
            _mockService.Setup(s => s.GetUserSkillsByUserIdAsync(userId)).ReturnsAsync(userSkills);

            // Act
            var result = await _controller.GetSkillsByUserId(userId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task GetSkillsByUserId_UserHasNoSkills_ReturnsNotFound()
        {
            // Arrange
            var userId = 1;
            _mockService.Setup(s => s.GetUserSkillsByUserIdAsync(userId)).ReturnsAsync(new List<UserSkillEntity>());

            // Act
            var result = await _controller.GetSkillsByUserId(userId);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Test]
        public async Task GetSkillNameBySkillId_SkillExists_ReturnsOkResultWithSkillName()
        {
            // Arrange
            var skillId = 1;
            var skillName = "C#";
            _mockService.Setup(s => s.GetSkillNameBySkillIdAsync(skillId)).ReturnsAsync(skillName);

            // Act
            var result = await _controller.GetSkillNameBySkillId(skillId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task GetSkillNameBySkillId_SkillDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var skillId = 1;
            _mockService.Setup(s => s.GetSkillNameBySkillIdAsync(skillId)).ReturnsAsync((string)null);

            // Act
            var result = await _controller.GetSkillNameBySkillId(skillId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetUserSkillsWithSkillTypeByUserId_UserHasSkills_ReturnsOkResultWithSkills()
        {
            // Arrange
            var userId = 1;
            var userSkills = new List<UserSkillEntity>
                {
                    new UserSkillEntity
                    {
                        Skill = new SkillEntity
                        {
                            Id = 1,
                            SkillName = "C#",
                            SkillType = new SkillTypeEntity { Id = 1, SkillTypeName = "Programming Language" }
                        }
                    },
                    new UserSkillEntity
                    {
                        Skill = new SkillEntity
                        {
                            Id = 2,
                            SkillName = "JavaScript",
                            SkillType = new SkillTypeEntity { Id = 1, SkillTypeName = "Programming Language" }
                        }
                    }
                };
            _mockService.Setup(s => s.GetUserSkillsWithSkillTypeByUserIdAsync(userId)).ReturnsAsync(userSkills);

            // Act
            var result = await _controller.GetUserSkillsWithSkillTypeByUserId(userId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task GetUserSkillsWithSkillTypeByUserId_UserHasNoSkills_ReturnsNotFound()
        {
            // Arrange
            var userId = 1;
            _mockService.Setup(s => s.GetUserSkillsWithSkillTypeByUserIdAsync(userId)).ReturnsAsync(new List<UserSkillEntity>());

            // Act
            var result = await _controller.GetUserSkillsWithSkillTypeByUserId(userId);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
