using EyeTrainer.Api.Constants;
using EyeTrainer.Api.Models;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EyeTrainer.Api.Data
{
    public static class DataSeeder
    {
        public static void CreateInitialData(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<EyeTrainerApiContext>();
            var wasCreated = dbContext.Database.EnsureCreated();
            
            //if (!wasCreated) return;

            dbContext.RemoveAllRecords();
            dbContext.SaveChanges();
            
            dbContext.ResetTableIdentities();

            dbContext.AddUsers()
                .AddAppointments()
                .AddEyeTrainingPlans()
                .AddEyeExercises();

            dbContext.SaveChanges();
        }

        private static void RemoveAllRecords(this EyeTrainerApiContext context)
        {
            context.User.RemoveRange(context.User);
            context.Appointment.RemoveRange(context.Appointment);
            context.EyeTrainingPlan.RemoveRange(context.EyeTrainingPlan);
            context.EyeExercise.RemoveRange(context.EyeExercise);
        }

        private static void ResetTableIdentities(this EyeTrainerApiContext context)
        {
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('User', RESEED, 0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Appointment', RESEED, 0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('EyeTrainingPlan', RESEED, 0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('EyeExercise', RESEED, 0);");
        }

        private static EyeTrainerApiContext AddUsers(this EyeTrainerApiContext context)
        {
            var users = new User[]
            {
                new User
                {
                    Email = "antanas.ramanauskas@ktu.edu",
                    Name = "Antanas",
                    Surname = "Ramanauskas",
                    HashedPassword = BCryptNet.HashPassword("password"),
                    DateOfBirth = new DateTime(2000, 3, 26),
                    DateOfRegistration = DateTime.Now,
                    Role = Roles.Administrator
                },

                new User
                {
                    Email = "deimante.dailidaviciute@gmail.com",
                    Name = "Deimante",
                    Surname = "Dailidaviciute",
                    HashedPassword = BCryptNet.HashPassword("password"),
                    DateOfBirth = new DateTime(1998, 3, 2),
                    DateOfRegistration = DateTime.Now,
                    Role = Roles.Doctor
                },

                new User
                {
                    Email = "elvinas.pilypas@gmail.com",
                    Name = "Elvinas",
                    Surname = "Pilypas",
                    HashedPassword = BCryptNet.HashPassword("password"),
                    DateOfBirth = new DateTime(1996, 4, 1),
                    DateOfRegistration = DateTime.Now,
                    Role = Roles.Patient
                }
            };

            context.User.AddRange(users);
            context.SaveChanges();

            return context;
        }

        private static EyeTrainerApiContext AddAppointments(this EyeTrainerApiContext context)
        {
            var doctor = context.User.First(user => user.Name == "Deimante");
            var firstPatient = context.User.First(user => user.Name == "Antanas");
            var secondPatient = context.User.First(user => user.Name == "Elvinas");

            var appointments = new Appointment[]
            {
                //Appointment between Antanas (Patient) and Deimante (Doctor)
                new Appointment
                {
                    Date = new DateTime(2022, 10, 12, 12, 0, 0),
                    Address = "Savanorių pr. 284, 49476 Kaunas",
                    Description = "Skauda akis, jaučiasi nuovargis. Norima pasikonsultuoti dėl akių pratimų, kurie padėtų jas pailsinti.",
                    IsConfirmed = true,
                    Doctor = doctor,
                    Patient = firstPatient
                },
                //Appointment between Elvinas (Patient) and Deimante (Doctor)
                new Appointment
                {
                    Date = new DateTime(2022, 10, 13, 13, 0, 0),
                    Address = "A. Baranausko g. 15, 50249 Kaunas",
                    Description = "Akys silpsta kiekvienais metais. Ar būtų galima sudaryti pratimus, kurie jas stiprintų?",
                    IsConfirmed = true,
                    Doctor = doctor,
                    Patient = secondPatient
                },
            };

            context.Appointment.AddRange(appointments);
            context.SaveChanges();

            return context;
        }

        private static EyeTrainerApiContext AddEyeTrainingPlans(this EyeTrainerApiContext context)
        {
            var appointment = context.Appointment
                .Include(app => app.Doctor)
                .Include(app => app.Patient)
                .First(app => app.Doctor.Name == "Deimante" && app.Patient.Name == "Antanas");

            var eyeTrainingPlans = new EyeTrainingPlan[]
            {
                new EyeTrainingPlan
                {
                    StartDate = new DateTime(2022, 10, 12, 12, 0, 0),
                    EndDate = new DateTime(2022, 10, 19, 12, 0, 0),
                    ImageLink = "https://medpharm.co.za/wp-content/uploads/2018/10/shutterstock_617982098.jpg",
                    TimesPerDay = 1,
                    Description = "Vakarinis akių treniravimo planas skirtas stiprinti ir atpalaiduoti akis",
                    Appointment = appointment
                },

                new EyeTrainingPlan
                {
                    StartDate = new DateTime(2022, 10, 12, 12, 0, 0),
                    EndDate = new DateTime(2022, 10, 19, 12, 0, 0),
                    ImageLink = "https://d31g6oeq0bzej7.cloudfront.net/Assets/image/jpeg/6f9cd14f-78ac-4032-a84e-0748180922ec.jpg",
                    TimesPerDay = 2,
                    Description = "Akių treniravimo planas skirtas akių spazmams malšinti",
                    Appointment = appointment
                },

                new EyeTrainingPlan
                {
                    StartDate = new DateTime(2022, 10, 12, 12, 0, 0),
                    EndDate = new DateTime(2022, 10, 19, 12, 0, 0),
                    ImageLink = "https://img.webmd.com/dtmcms/live/webmd/consumer_assets/site_images/article_thumbnails/other/dry_eyes_alt1_other/650x350_dry_eyes_alt1_other.jpg",
                    TimesPerDay = 5,
                    Description = "Akių treniravimo planas skirtas sausoms akims, ypač tinkantis atlikti pertraukų metu po darbo su kompiuteriu",
                    Appointment = appointment
                },
            };

            context.EyeTrainingPlan.AddRange(eyeTrainingPlans);
            context.SaveChanges();

            return context;
        }

        private static EyeTrainerApiContext AddEyeExercises(this EyeTrainerApiContext context)
        {
            var eyeTrainingPlan = context.EyeTrainingPlan.Find(3);

            var eyeExercises = new EyeExercise[]
            {
                new EyeExercise
                {
                    Name = "Akių dengimas",
                    Description = "Užsimerkti, uždengti akis delnais, atsimerkti. Laikyti uždengtas 5 sekundes",
                    RestTimeSeconds = 5,
                    TimesPerSet = 6,
                    SetCount = 3,
                    EyeTrainingPlan = eyeTrainingPlan
                },

                new EyeExercise
                {
                    Name = "Akių mirksėjimas (greitai)",
                    Description = "Mirksėti greitai",
                    RestTimeSeconds = 5,
                    TimesPerSet = 50,
                    SetCount = 3,
                    EyeTrainingPlan = eyeTrainingPlan
                },

                new EyeExercise
                {
                    Name = "Akių sukimas",
                    Description = "Sukti akis ratu atliekant kaip įmanoma didesnį ratą",
                    RestTimeSeconds = 5,
                    TimesPerSet = 20,
                    SetCount = 3,
                    EyeTrainingPlan = eyeTrainingPlan
                },

                new EyeExercise
                {
                    Name = "Akių mirksėjimas (stipriai)",
                    Description = "Mirksėti neskubant siekiant užsimerkti kaip įmanoma stipriau",
                    RestTimeSeconds = 5,
                    TimesPerSet = 15,
                    SetCount = 3,
                    EyeTrainingPlan = eyeTrainingPlan
                }
            };

            context.EyeExercise.AddRange(eyeExercises);
            context.SaveChanges();

            return context;
        }
    }
}
