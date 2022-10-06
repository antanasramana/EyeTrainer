﻿// <auto-generated />
using System;
using EyeTrainer.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EyeTrainer.Api.Migrations
{
    [DbContext(typeof(EyeTrainerApiContext))]
    [Migration("20221006220028_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EyeExerciseEyeTrainingPlan", b =>
                {
                    b.Property<int>("EyeExercisesId")
                        .HasColumnType("int");

                    b.Property<int>("EyeTrainingPlansId")
                        .HasColumnType("int");

                    b.HasKey("EyeExercisesId", "EyeTrainingPlansId");

                    b.HasIndex("EyeTrainingPlansId");

                    b.ToTable("EyeExerciseEyeTrainingPlan");
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speciality")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.EyeExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("RestTimeSeconds")
                        .HasColumnType("float");

                    b.Property<int>("SetCount")
                        .HasColumnType("int");

                    b.Property<int>("TimesPerSet")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EyeExercise");
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.EyeTrainingPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("EyeTrainingPlan");
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClinicalConditionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Doctor"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Patient"
                        });
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfRegistration")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId")
                        .IsUnique()
                        .HasFilter("[DoctorId] IS NOT NULL");

                    b.HasIndex("PatientId")
                        .IsUnique()
                        .HasFilter("[PatientId] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("EyeExerciseEyeTrainingPlan", b =>
                {
                    b.HasOne("EyeTrainer.Api.Models.EyeExercise", null)
                        .WithMany()
                        .HasForeignKey("EyeExercisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EyeTrainer.Api.Models.EyeTrainingPlan", null)
                        .WithMany()
                        .HasForeignKey("EyeTrainingPlansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.EyeTrainingPlan", b =>
                {
                    b.HasOne("EyeTrainer.Api.Models.Patient", "Patient")
                        .WithMany("EyeTrainingPlans")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.Patient", b =>
                {
                    b.HasOne("EyeTrainer.Api.Models.Doctor", "Doctor")
                        .WithMany("Patients")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.User", b =>
                {
                    b.HasOne("EyeTrainer.Api.Models.Doctor", "Doctor")
                        .WithOne("User")
                        .HasForeignKey("EyeTrainer.Api.Models.User", "DoctorId");

                    b.HasOne("EyeTrainer.Api.Models.Patient", "Patient")
                        .WithOne("User")
                        .HasForeignKey("EyeTrainer.Api.Models.User", "PatientId");

                    b.HasOne("EyeTrainer.Api.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.Doctor", b =>
                {
                    b.Navigation("Patients");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EyeTrainer.Api.Models.Patient", b =>
                {
                    b.Navigation("EyeTrainingPlans");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
