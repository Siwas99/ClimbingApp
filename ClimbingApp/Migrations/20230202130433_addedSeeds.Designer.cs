﻿// <auto-generated />
using System;
using ClimbingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClimbingApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230202130433_addedSeeds")]
    partial class addedSeeds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClimbingApp.Models.Area", b =>
                {
                    b.Property<int>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AreaId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.HasKey("AreaId");

                    b.HasIndex("RegionId");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("ClimbingApp.Models.ClimbStyle", b =>
                {
                    b.Property<int>("ClimbStyleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClimbStyleId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClimbStyleId");

                    b.ToTable("ClimbStyle");

                    b.HasData(
                        new
                        {
                            ClimbStyleId = 1,
                            Description = "Czyste przejście drogi, tzn. bez odpadnięcia i obciążania liny, bez jej znajomości. Oznacza to, że niedozwolone są podpowiedzi lub obserwacja innych wspinaczy.",
                            Name = "On Sight"
                        },
                        new
                        {
                            ClimbStyleId = 2,
                            Description = "Czyste przejście drogii, tzn. bez odpadnięcia i obciążania liny, ze znajomością drogi. Oznacza to, że podpowiedzi oraz oglądanie innych wspinaczy jest dozwolone.",
                            Name = "Flash"
                        },
                        new
                        {
                            ClimbStyleId = 3,
                            Description = "Przejście całej drogi od początku do końca bez odpadnięć i odpoczynków. Dozwolone jest wcześniejsze ćwiczenie drogi i opracowanie sekwencji przechwytów. Styl ten uważa się za normalny w przypadku trudniejszych dróg.",
                            Name = "Red Point"
                        },
                        new
                        {
                            ClimbStyleId = 4,
                            Description = "Lina asekurująca wspinacza biegnie na górę, przechodzi przez stanowisko i wraca do stojącego na dole partnera. Przejście drogi w tym stylu nie jest obecnie uznawane za klasyczne, jednak z uwagi na najmniejsze ryzyko urazów wspinaczka na wędkę ma znaczenie w treningu, patentowaniu drogi oraz we wspinaczkowej rekreacji, szczególnie u osób początkujących.",
                            Name = "Top Rope"
                        });
                });

            modelBuilder.Entity("ClimbingApp.Models.DominantRockFormation", b =>
                {
                    b.Property<int>("DominantRockFormationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DominantRockFormationId"));

                    b.Property<int>("RockFormationId")
                        .HasColumnType("int");

                    b.Property<int>("RockId")
                        .HasColumnType("int");

                    b.HasKey("DominantRockFormationId");

                    b.HasIndex("RockFormationId");

                    b.HasIndex("RockId");

                    b.ToTable("DominantRockFormations");
                });

            modelBuilder.Entity("ClimbingApp.Models.ExpeditionLog", b =>
                {
                    b.Property<int>("ExpeditionLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpeditionLogId"));

                    b.Property<int>("ClimbStyleId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Valuation")
                        .HasColumnType("int");

                    b.HasKey("ExpeditionLogId");

                    b.HasIndex("ClimbStyleId");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.ToTable("ExpeditionLogs");
                });

            modelBuilder.Entity("ClimbingApp.Models.Region", b =>
                {
                    b.Property<int>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegionId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("ClimbingApp.Models.Rock", b =>
                {
                    b.Property<int>("RockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RockId"));

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<bool>("IsLoose")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRecommended")
                        .HasColumnType("bit");

                    b.Property<bool>("IsShadedFromTrees")
                        .HasColumnType("bit");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Popularity")
                        .HasColumnType("int");

                    b.Property<int>("RockFaceExposureId")
                        .HasColumnType("int");

                    b.HasKey("RockId");

                    b.HasIndex("AreaId");

                    b.HasIndex("RockFaceExposureId");

                    b.ToTable("Rocks");
                });

            modelBuilder.Entity("ClimbingApp.Models.RockFaceExposure", b =>
                {
                    b.Property<int>("RockFaceExposureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RockFaceExposureId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RockFaceExposureId");

                    b.ToTable("RockFaceExposures");

                    b.HasData(
                        new
                        {
                            RockFaceExposureId = 1,
                            Name = "North"
                        },
                        new
                        {
                            RockFaceExposureId = 2,
                            Name = "East"
                        },
                        new
                        {
                            RockFaceExposureId = 3,
                            Name = "South"
                        },
                        new
                        {
                            RockFaceExposureId = 4,
                            Name = "West"
                        });
                });

            modelBuilder.Entity("ClimbingApp.Models.RockFormation", b =>
                {
                    b.Property<int>("RockFormationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RockFormationId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RockFormationId");

                    b.ToTable("RockFormations");

                    b.HasData(
                        new
                        {
                            RockFormationId = 1,
                            Name = "Slabs"
                        },
                        new
                        {
                            RockFormationId = 2,
                            Name = "Vertical"
                        },
                        new
                        {
                            RockFormationId = 3,
                            Name = "Overhang"
                        },
                        new
                        {
                            RockFormationId = 4,
                            Name = "Roof"
                        });
                });

            modelBuilder.Entity("ClimbingApp.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("ClimbingApp.Models.Route", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteId"));

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Difficulty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("RockId")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("RouteId");

                    b.HasIndex("RockId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("ClimbingApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClimbingApp.Models.Wishlist", b =>
                {
                    b.Property<int>("WishlistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WishlistId"));

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("WishlistId");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.ToTable("Wishlists");
                });

            modelBuilder.Entity("ClimbingApp.Models.Area", b =>
                {
                    b.HasOne("ClimbingApp.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("ClimbingApp.Models.DominantRockFormation", b =>
                {
                    b.HasOne("ClimbingApp.Models.RockFormation", "RockFormation")
                        .WithMany()
                        .HasForeignKey("RockFormationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClimbingApp.Models.Rock", "Rock")
                        .WithMany()
                        .HasForeignKey("RockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rock");

                    b.Navigation("RockFormation");
                });

            modelBuilder.Entity("ClimbingApp.Models.ExpeditionLog", b =>
                {
                    b.HasOne("ClimbingApp.Models.ClimbStyle", "ClimbStyle")
                        .WithMany()
                        .HasForeignKey("ClimbStyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClimbingApp.Models.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClimbingApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClimbStyle");

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClimbingApp.Models.Rock", b =>
                {
                    b.HasOne("ClimbingApp.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClimbingApp.Models.RockFaceExposure", "RockFaceExposure")
                        .WithMany()
                        .HasForeignKey("RockFaceExposureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("RockFaceExposure");
                });

            modelBuilder.Entity("ClimbingApp.Models.Route", b =>
                {
                    b.HasOne("ClimbingApp.Models.Rock", "Rock")
                        .WithMany()
                        .HasForeignKey("RockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rock");
                });

            modelBuilder.Entity("ClimbingApp.Models.User", b =>
                {
                    b.HasOne("ClimbingApp.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ClimbingApp.Models.Wishlist", b =>
                {
                    b.HasOne("ClimbingApp.Models.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClimbingApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}