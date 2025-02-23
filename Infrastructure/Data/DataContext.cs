using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<EducationEntitiy> Educations { get; set; }

        public DbSet<PositionEntity> Positions { get; set; }

        public DbSet<PositionResponsibilityEntity> PositionResponsibilities { get; set; }

        public DbSet<ProjectEntity> Projects { get; set; }

        public DbSet<ProjectDescription> ProjectDescriptions { get; set; }

        public DbSet<ProjectTechnologyEntity> ProjectTechnologies { get; set; }

        public DbSet<QualificationTypeEntity> QualificationTypes { get; set; }

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

            // One to many relationship
            // A user can have many positions
            modelBuilder.Entity<PositionEntity>()
                .HasOne(p => p.User)
                .WithMany(u => u.Positions)
                .HasForeignKey(p => p.UserId)
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
                    Email = "joh@example.com"
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
