using Microsoft.EntityFrameworkCore;
using EyeTrainer.Api.Models;

namespace EyeTrainer.Api.Data
{
    public class EyeTrainerApiContext : DbContext
    {
        public EyeTrainerApiContext (DbContextOptions<EyeTrainerApiContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var initialRoles = CreateInitialRoles();
            modelBuilder.Entity<Role>().HasData(initialRoles);
        }

        private static IEnumerable<Role> CreateInitialRoles()
        {
            yield return new Role { Id = 1, Name = "Administrator" };
            yield return new Role { Id = 2, Name = "Doctor" };
            yield return new Role { Id = 3, Name = "Patient" };
        }

        public DbSet<User> User { get; set; } = default!;

        public DbSet<Doctor> Doctor { get; set; }

        public DbSet<EyeTrainingPlan> EyeTrainingPlan { get; set; }

        public DbSet<Patient> Patient { get; set; }

        public DbSet<EyeExercise> EyeExercise { get; set; }
    }
}
