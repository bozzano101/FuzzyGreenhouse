﻿// <auto-generated />
using System;
using AdminBoard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdminBoard.Data.Migrations
{
    [DbContext(typeof(FuzzyGreenhouseDbContext))]
    partial class FuzzyGreenhouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("AdminBoard.Models.FuzzyGreenHouse.Rule", b =>
                {
                    b.Property<int>("RuleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("InputValue1ID")
                        .HasColumnType("int");

                    b.Property<int>("InputValue2ID")
                        .HasColumnType("int");

                    b.Property<int>("Operator")
                        .HasColumnType("int");

                    b.Property<int>("OutputValueID")
                        .HasColumnType("int");

                    b.Property<int>("SubsystemID")
                        .HasColumnType("int");

                    b.HasKey("RuleID");

                    b.HasIndex("InputValue1ID");

                    b.HasIndex("InputValue2ID");

                    b.HasIndex("OutputValueID");

                    b.ToTable("Rule");
                });

            modelBuilder.Entity("AdminBoard.Models.FuzzyGreenHouse.Set", b =>
                {
                    b.Property<int>("SetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("SetID");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("AdminBoard.Models.FuzzyGreenHouse.Subsystem", b =>
                {
                    b.Property<int>("SubsystemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("SubsystemID");

                    b.ToTable("Subsystem");
                });

            modelBuilder.Entity("AdminBoard.Models.FuzzyGreenHouse.Value", b =>
                {
                    b.Property<int>("ValueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("SetID")
                        .HasColumnType("int");

                    b.Property<string>("XCoords")
                        .HasColumnType("longtext");

                    b.Property<string>("YCoords")
                        .HasColumnType("longtext");

                    b.HasKey("ValueID");

                    b.HasIndex("SetID");

                    b.ToTable("Value");
                });

            modelBuilder.Entity("AdminBoard.Models.FuzzyGreenHouse.Version", b =>
                {
                    b.Property<int>("VersionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.HasKey("VersionID");

                    b.ToTable("Version");
                });

            modelBuilder.Entity("AdminBoard.Models.FuzzyGreenHouse.Rule", b =>
                {
                    b.HasOne("AdminBoard.Models.FuzzyGreenHouse.Value", "InputValue1")
                        .WithMany()
                        .HasForeignKey("InputValue1ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminBoard.Models.FuzzyGreenHouse.Value", "InputValue2")
                        .WithMany()
                        .HasForeignKey("InputValue2ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminBoard.Models.FuzzyGreenHouse.Value", "OutputValue")
                        .WithMany()
                        .HasForeignKey("OutputValueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminBoard.Models.FuzzyGreenHouse.Subsystem", "Subsystem")
                        .WithMany()
                        .HasForeignKey("SubsystemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InputValue1");

                    b.Navigation("InputValue2");

                    b.Navigation("OutputValue");

                    b.Navigation("Subsystem");
                });

            modelBuilder.Entity("AdminBoard.Models.FuzzyGreenHouse.Value", b =>
                {
                    b.HasOne("AdminBoard.Models.FuzzyGreenHouse.Set", "Set")
                        .WithMany("Values")
                        .HasForeignKey("SetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Set");
                });

            modelBuilder.Entity("AdminBoard.Models.FuzzyGreenHouse.Set", b =>
                {
                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
