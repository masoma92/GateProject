﻿// <auto-generated />
using System;
using GateProjectBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GateProjectBackend.Migrations
{
    [DbContext(typeof(GPDbContext))]
    [Migration("20201018172330_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GateProjectBackend.Data.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountTypeId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MoidifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountTypeId = 1,
                            City = "Budapest",
                            ContactEmail = "asd@gmail.com",
                            Country = "Hungary",
                            CreatedAt = new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(509),
                            CreatedBy = "SYSTEM",
                            Name = "TestOffice",
                            Street = "Szuglo utca",
                            StreetNo = "53",
                            Zip = "1145"
                        },
                        new
                        {
                            Id = 2,
                            AccountTypeId = 3,
                            City = "Budapest",
                            ContactEmail = "asd@gmail.com",
                            Country = "Hungary",
                            CreatedAt = new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1259),
                            CreatedBy = "SYSTEM",
                            Name = "TestSchool",
                            Street = "Szuglo utca",
                            StreetNo = "53",
                            Zip = "1146"
                        },
                        new
                        {
                            Id = 3,
                            AccountTypeId = 1,
                            City = "Budapest",
                            ContactEmail = "asd@gmail.com",
                            Country = "Hungary",
                            CreatedAt = new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1273),
                            CreatedBy = "SYSTEM",
                            Name = "TestOffice2",
                            Street = "Szuglo utca",
                            StreetNo = "53",
                            Zip = "1147"
                        },
                        new
                        {
                            Id = 4,
                            AccountTypeId = 2,
                            City = "Budapest",
                            ContactEmail = "asd@gmail.com",
                            Country = "Hungary",
                            CreatedAt = new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1275),
                            CreatedBy = "SYSTEM",
                            Name = "TestHome",
                            Street = "Szuglo utca",
                            StreetNo = "53",
                            Zip = "1148"
                        },
                        new
                        {
                            Id = 5,
                            AccountTypeId = 4,
                            City = "Budapest",
                            ContactEmail = "asd@gmail.com",
                            Country = "Hungary",
                            CreatedAt = new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1276),
                            CreatedBy = "SYSTEM",
                            Name = "TestAccomodation",
                            Street = "Szuglo utca",
                            StreetNo = "53",
                            Zip = "1149"
                        },
                        new
                        {
                            Id = 6,
                            AccountTypeId = 1,
                            City = "Budapest",
                            ContactEmail = "asd@gmail.com",
                            Country = "Hungary",
                            CreatedAt = new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(1278),
                            CreatedBy = "SYSTEM",
                            Name = "TestOffice",
                            Street = "Szuglo utca",
                            StreetNo = "53",
                            Zip = "1150"
                        });
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.AccountAdmin", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MoidifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "AccountId");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountAdmins");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            AccountId = 1,
                            CreatedAt = new DateTime(2020, 10, 18, 17, 23, 30, 119, DateTimeKind.Utc).AddTicks(2520),
                            CreatedBy = "SYSTEM"
                        });
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.AccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Office"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Home"
                        },
                        new
                        {
                            Id = 3,
                            Name = "School"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Accomodation"
                        });
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.AccountUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MoidifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "AccountId");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountUsers");
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Error"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Info"
                        });
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.Gate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("CharacteristicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GateTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MoidifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GateTypeId");

                    b.ToTable("Gates");
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.GateType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GateTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Entrance"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Garage"
                        });
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MoidifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MoidifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RfidKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birth = new DateTime(1992, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedAt = new DateTime(2020, 10, 18, 17, 23, 30, 118, DateTimeKind.Utc).AddTicks(6235),
                            CreatedBy = "SYSTEM",
                            Email = "soma.makai@gmail.com",
                            FirstName = "Soma",
                            IsActive = true,
                            LastName = "Makai",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.UserGate", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("GateId")
                        .HasColumnType("int");

                    b.Property<bool>("AccessRight")
                        .HasColumnType("bit");

                    b.Property<bool>("AdminRight")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MoidifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "GateId");

                    b.HasIndex("GateId");

                    b.ToTable("UserGates");
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.Account", b =>
                {
                    b.HasOne("GateProjectBackend.Data.Models.AccountType", "AccountType")
                        .WithMany()
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.AccountAdmin", b =>
                {
                    b.HasOne("GateProjectBackend.Data.Models.Account", "Account")
                        .WithMany("Admins")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GateProjectBackend.Data.Models.User", "User")
                        .WithMany("AdminAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.AccountUser", b =>
                {
                    b.HasOne("GateProjectBackend.Data.Models.Account", "Account")
                        .WithMany("Users")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GateProjectBackend.Data.Models.User", "User")
                        .WithMany("UserAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.Gate", b =>
                {
                    b.HasOne("GateProjectBackend.Data.Models.Account", "Account")
                        .WithMany("Gates")
                        .HasForeignKey("AccountId");

                    b.HasOne("GateProjectBackend.Data.Models.GateType", "GateType")
                        .WithMany()
                        .HasForeignKey("GateTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.Log", b =>
                {
                    b.HasOne("GateProjectBackend.Data.Models.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GateProjectBackend.Data.Models.User", "User")
                        .WithMany("Logs")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.User", b =>
                {
                    b.HasOne("GateProjectBackend.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GateProjectBackend.Data.Models.UserGate", b =>
                {
                    b.HasOne("GateProjectBackend.Data.Models.Gate", "Gate")
                        .WithMany("Users")
                        .HasForeignKey("GateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GateProjectBackend.Data.Models.User", "User")
                        .WithMany("Gates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}