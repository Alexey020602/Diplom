﻿// <auto-generated />
using System;
using DataBase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataBase.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataBase.Models.Agreement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AgreementNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar");

                    b.Property<int>("AgreementStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("AgreementTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("date");

                    b.Property<DateTime>("StarDateTime")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("AgreementNumber")
                        .IsUnique();

                    b.HasIndex("AgreementStatusId");

                    b.HasIndex("AgreementTypeId");

                    b.ToTable("Agreements");
                });

            modelBuilder.Entity("DataBase.Models.AgreementStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AgreementStatus");
                });

            modelBuilder.Entity("DataBase.Models.AgreementType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AgreementType");
                });

            modelBuilder.Entity("DataBase.Models.Direction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("Directions");
                });

            modelBuilder.Entity("DataBase.Models.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Contacts")
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<int>("FacultyId")
                        .HasColumnType("integer");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("Site")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("ShortName")
                        .IsUnique();

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("DataBase.Models.DivisionInAgreement", b =>
                {
                    b.Property<int>("DivisionId")
                        .HasColumnType("integer");

                    b.Property<int>("AgreementId")
                        .HasColumnType("integer");

                    b.Property<string>("ContactPersons")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.HasKey("DivisionId", "AgreementId");

                    b.HasIndex("AgreementId");

                    b.ToTable("DivisionsInAgreement");
                });

            modelBuilder.Entity("DataBase.Models.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("DataBase.Models.Interaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BeginigDateTime")
                        .HasColumnType("date");

                    b.Property<string>("ContactCode")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("varchar");

                    b.Property<int>("DivisionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndingDateTime")
                        .HasColumnType("date");

                    b.Property<int>("InteractionTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("PartnerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SigningDateTime")
                        .HasColumnType("date");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.HasIndex("InteractionTypeId");

                    b.HasIndex("PartnerId");

                    b.ToTable("Interactions");
                });

            modelBuilder.Entity("DataBase.Models.InteractionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("InteractionTypes");
                });

            modelBuilder.Entity("DataBase.Models.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("ContactData")
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.Property<int>("PartnerTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("Site")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("PartnerTypeId");

                    b.HasIndex("ShortName")
                        .IsUnique();

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("DataBase.Models.PartnerInAgreement", b =>
                {
                    b.Property<int>("AgreementId")
                        .HasColumnType("integer");

                    b.Property<int>("PartnerId")
                        .HasColumnType("integer");

                    b.Property<string>("ContactPersons")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.HasKey("AgreementId", "PartnerId");

                    b.HasIndex("PartnerId");

                    b.ToTable("PartnersInAgreement");
                });

            modelBuilder.Entity("DataBase.Models.PartnerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PartnerTypes");
                });

            modelBuilder.Entity("DirectionDivision", b =>
                {
                    b.Property<int>("DirectionsId")
                        .HasColumnType("integer");

                    b.Property<int>("DivisionsId")
                        .HasColumnType("integer");

                    b.HasKey("DirectionsId", "DivisionsId");

                    b.HasIndex("DivisionsId");

                    b.ToTable("DirectionDivision");
                });

            modelBuilder.Entity("DirectionInteraction", b =>
                {
                    b.Property<int>("DirectionsId")
                        .HasColumnType("integer");

                    b.Property<int>("InteractionsId")
                        .HasColumnType("integer");

                    b.HasKey("DirectionsId", "InteractionsId");

                    b.HasIndex("InteractionsId");

                    b.ToTable("DirectionInteraction");
                });

            modelBuilder.Entity("DirectionPartner", b =>
                {
                    b.Property<int>("DirectionsId")
                        .HasColumnType("integer");

                    b.Property<int>("PartnersId")
                        .HasColumnType("integer");

                    b.HasKey("DirectionsId", "PartnersId");

                    b.HasIndex("PartnersId");

                    b.ToTable("DirectionPartner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("varchar");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("varchar");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("varchar");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("varchar");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("varchar");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("varchar");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("varchar");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("varchar");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .HasColumnType("varchar");

                    b.Property<string>("Value")
                        .HasColumnType("varchar");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DataBase.Models.Agreement", b =>
                {
                    b.HasOne("DataBase.Models.AgreementStatus", "AgreementStatus")
                        .WithMany("Agreements")
                        .HasForeignKey("AgreementStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataBase.Models.AgreementType", "AgreementType")
                        .WithMany("Agreements")
                        .HasForeignKey("AgreementTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AgreementStatus");

                    b.Navigation("AgreementType");
                });

            modelBuilder.Entity("DataBase.Models.Division", b =>
                {
                    b.HasOne("DataBase.Models.Faculty", "Faculty")
                        .WithMany("Divisions")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("DataBase.Models.DivisionInAgreement", b =>
                {
                    b.HasOne("DataBase.Models.Agreement", "Agreement")
                        .WithMany("DivisionInAgreements")
                        .HasForeignKey("AgreementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataBase.Models.Division", "Division")
                        .WithMany("DivisionsInAgreement")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Agreement");

                    b.Navigation("Division");
                });

            modelBuilder.Entity("DataBase.Models.Interaction", b =>
                {
                    b.HasOne("DataBase.Models.Division", "Division")
                        .WithMany("Interactions")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataBase.Models.InteractionType", "InteractionType")
                        .WithMany("Interactions")
                        .HasForeignKey("InteractionTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataBase.Models.Partner", "Partner")
                        .WithMany("Interactions")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Division");

                    b.Navigation("InteractionType");

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("DataBase.Models.Partner", b =>
                {
                    b.HasOne("DataBase.Models.PartnerType", "PartnerType")
                        .WithMany("Partners")
                        .HasForeignKey("PartnerTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PartnerType");
                });

            modelBuilder.Entity("DataBase.Models.PartnerInAgreement", b =>
                {
                    b.HasOne("DataBase.Models.Agreement", "Agreement")
                        .WithMany("PartnerInAgreements")
                        .HasForeignKey("AgreementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataBase.Models.Partner", "Partner")
                        .WithMany("PartnersInAgreement")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Agreement");

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("DirectionDivision", b =>
                {
                    b.HasOne("DataBase.Models.Direction", null)
                        .WithMany()
                        .HasForeignKey("DirectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataBase.Models.Division", null)
                        .WithMany()
                        .HasForeignKey("DivisionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DirectionInteraction", b =>
                {
                    b.HasOne("DataBase.Models.Direction", null)
                        .WithMany()
                        .HasForeignKey("DirectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataBase.Models.Interaction", null)
                        .WithMany()
                        .HasForeignKey("InteractionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DirectionPartner", b =>
                {
                    b.HasOne("DataBase.Models.Direction", null)
                        .WithMany()
                        .HasForeignKey("DirectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataBase.Models.Partner", null)
                        .WithMany()
                        .HasForeignKey("PartnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataBase.Models.Agreement", b =>
                {
                    b.Navigation("DivisionInAgreements");

                    b.Navigation("PartnerInAgreements");
                });

            modelBuilder.Entity("DataBase.Models.AgreementStatus", b =>
                {
                    b.Navigation("Agreements");
                });

            modelBuilder.Entity("DataBase.Models.AgreementType", b =>
                {
                    b.Navigation("Agreements");
                });

            modelBuilder.Entity("DataBase.Models.Division", b =>
                {
                    b.Navigation("DivisionsInAgreement");

                    b.Navigation("Interactions");
                });

            modelBuilder.Entity("DataBase.Models.Faculty", b =>
                {
                    b.Navigation("Divisions");
                });

            modelBuilder.Entity("DataBase.Models.InteractionType", b =>
                {
                    b.Navigation("Interactions");
                });

            modelBuilder.Entity("DataBase.Models.Partner", b =>
                {
                    b.Navigation("Interactions");

                    b.Navigation("PartnersInAgreement");
                });

            modelBuilder.Entity("DataBase.Models.PartnerType", b =>
                {
                    b.Navigation("Partners");
                });
#pragma warning restore 612, 618
        }
    }
}
