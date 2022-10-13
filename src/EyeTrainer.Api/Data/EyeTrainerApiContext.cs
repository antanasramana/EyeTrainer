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
            modelBuilder.Entity<Appointment>()
                .HasOne(visit => visit.Doctor)
                .WithMany(user => user.DoctorVisits);

            modelBuilder.Entity<Appointment>()
                .HasOne(visit => visit.Patient)
                .WithMany(user => user.PatientVisits);
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<EyeTrainingPlan> EyeTrainingPlan { get; set; }
        public DbSet<EyeExercise> EyeExercise { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
    }
}
