using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<EducationEntity> Educations { get; set; }

        public DbSet<PositionEntity> Positions { get; set; }

        public DbSet<PositionResponsibilityEntity> PositionResponsibilities { get; set; }

        public DbSet<ProjectEntity> Projects { get; set; }

        public DbSet<ProjectTechnologyEntity> ProjectTechnologies { get; set; }

        public DbSet<SkillEntity> Skills { get; set; }

        public DbSet<SkillTypeEntity> SkillTypes { get; set; }

        public DbSet<SocialMediaEntity> SocialMedias { get; set; }

        public DbSet<TechnologyEntity> Technologies { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserSkillEntity> UserSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // ==========================
            // ===== Relationships ======
            // ==========================

            modelBuilder.Entity<PositionEntity>()
                .HasOne(p => p.User)
                .WithMany(u => u.Positions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PositionResponsibilityEntity>()
                .HasOne(pr => pr.Position)
                .WithMany(p => p.Responsibilities)
                .HasForeignKey(pr => pr.PositionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ==========================
            // ======= Seed Data ========
            // ==========================

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = 1,
                    FirstName = "John",
                    MiddleName = "Henry",
                    LastName = "Doe",
                    Email = "john@example.com"
                },
                new UserEntity
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane@email.com",
                    PhoneNumber = "1234567890"
                },
                new UserEntity
                {
                    Id = 3,
                    FirstName = "Kevin",
                    MiddleName = "James",
                    LastName = "Kelvin",
                    Email = "kev@email.com",
                    PhoneNumber = "0987654321"
                },
                new UserEntity
                {
                    Id = 4,
                    FirstName = "Charlie",
                    LastName = "Thompson",
                    Email = "charlie@email.com"
                }
            );

            modelBuilder.Entity<PositionEntity>().HasData(
                new PositionEntity
                {
                    Id = 1,
                    Role = "Software Developer Intern",
                    StartDate = "2020-01-01",
                    EndDate = "2021-01-01",
                    UserId = 1
                },
                new PositionEntity
                {
                    Id = 2,
                    Role = "Software Developer",
                    StartDate = "2021-01-02",
                    EndDate = "2022-01-01",
                    UserId = 1
                },
                new PositionEntity
                {
                    Id = 3,
                    Role = "Junior Software Engineer",
                    StartDate = "2022-01-02",
                    UserId = 1
                },
                new PositionEntity
                {
                    Id = 4,
                    Role = "Software Developer",
                    StartDate = "2020-01-01",
                    EndDate = "2021-01-01",
                    UserId = 2
                },
                new PositionEntity
                {
                    Id = 5,
                    Role = "Mechanical Mathematician",
                    StartDate = "2021-01-02",
                    UserId = 2
                },
                new PositionEntity
                {
                    Id = 6,
                    Role = "Astrophysicist",
                    StartDate = "2020-01-01",
                    UserId = 3
                }
            );

            modelBuilder.Entity<PositionResponsibilityEntity>().HasData(
                new PositionResponsibilityEntity { Id = 1, PositionId = 1, Responsibility = "Assist in software development tasks" },
                new PositionResponsibilityEntity { Id = 2, PositionId = 1, Responsibility = "Write and maintain code" },
                new PositionResponsibilityEntity { Id = 3, PositionId = 1, Responsibility = "Collaborate with team members" },
                new PositionResponsibilityEntity { Id = 4, PositionId = 1, Responsibility = "Participate in code reviews" },
                new PositionResponsibilityEntity { Id = 5, PositionId = 1, Responsibility = "Assist in testing and debugging" },

                new PositionResponsibilityEntity { Id = 6, PositionId = 2, Responsibility = "Develop new software features" },
                new PositionResponsibilityEntity { Id = 7, PositionId = 2, Responsibility = "Maintain existing codebase" },
                new PositionResponsibilityEntity { Id = 8, PositionId = 2, Responsibility = "Collaborate with cross-functional teams" },
                new PositionResponsibilityEntity { Id = 9, PositionId = 2, Responsibility = "Review and refactor code" },
                new PositionResponsibilityEntity { Id = 10, PositionId = 2, Responsibility = "Document development processes" },

                new PositionResponsibilityEntity { Id = 11, PositionId = 3, Responsibility = "Assist in software design" },
                new PositionResponsibilityEntity { Id = 12, PositionId = 3, Responsibility = "Implement software solutions" },
                new PositionResponsibilityEntity { Id = 13, PositionId = 3, Responsibility = "Collaborate with developers" },
                new PositionResponsibilityEntity { Id = 14, PositionId = 3, Responsibility = "Participate in Agile processes" },
                new PositionResponsibilityEntity { Id = 15, PositionId = 3, Responsibility = "Assist in system testing" },

                new PositionResponsibilityEntity { Id = 16, PositionId = 4, Responsibility = "Develop software components" },
                new PositionResponsibilityEntity { Id = 17, PositionId = 4, Responsibility = "Maintain technical documentation" },
                new PositionResponsibilityEntity { Id = 18, PositionId = 4, Responsibility = "Work with QA team" },
                new PositionResponsibilityEntity { Id = 19, PositionId = 4, Responsibility = "Review pull requests" },
                new PositionResponsibilityEntity { Id = 20, PositionId = 4, Responsibility = "Fix software bugs" },

                new PositionResponsibilityEntity { Id = 21, PositionId = 5, Responsibility = "Conduct mathematical analysis" },
                new PositionResponsibilityEntity { Id = 22, PositionId = 5, Responsibility = "Develop mathematical models" },
                new PositionResponsibilityEntity { Id = 23, PositionId = 5, Responsibility = "Collaborate with engineering teams" },
                new PositionResponsibilityEntity { Id = 24, PositionId = 5, Responsibility = "Present findings and recommendations" },
                new PositionResponsibilityEntity { Id = 25, PositionId = 5, Responsibility = "Document mathematical solutions" },

                new PositionResponsibilityEntity { Id = 26, PositionId = 6, Responsibility = "Conduct astrophysical research" },
                new PositionResponsibilityEntity { Id = 27, PositionId = 6, Responsibility = "Analyze astronomical data" },
                new PositionResponsibilityEntity { Id = 28, PositionId = 6, Responsibility = "Collaborate with research teams" },
                new PositionResponsibilityEntity { Id = 29, PositionId = 6, Responsibility = "Present research findings" },
                new PositionResponsibilityEntity { Id = 30, PositionId = 6, Responsibility = "Publish research papers" }
            );

        }


        // Override to configure the database provider
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data source=(localdb)\\localdb;Initial catalog=CvDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}
