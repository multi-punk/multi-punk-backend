﻿// <auto-generated />
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MultiApi.Database;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MultiApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231203132930_editStats")]
    partial class editStats
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MultiApi.Database.Tables.ApiKey", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("ownerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("type")
                        .HasColumnType("integer");

                    b.HasKey("Key");

                    b.ToTable("ApiKeys", "public");
                });

            modelBuilder.Entity("MultiApi.Database.Tables.Game", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("MaxPlayersCount")
                        .HasColumnType("integer");

                    b.Property<int>("MinPlayersCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Games", "public");
                });

            modelBuilder.Entity("MultiApi.Database.Tables.Party", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<List<string>>("Participants")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("Party", "public");
                });

            modelBuilder.Entity("MultiApi.Database.Tables.Permission", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Permissions", "public");
                });

            modelBuilder.Entity("MultiApi.Database.Tables.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles", "public");
                });

            modelBuilder.Entity("MultiApi.Database.Tables.Statistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Game")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Statistic", "public");
                });

            modelBuilder.Entity("MultiApi.Database.Tables.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("PartyId")
                        .HasColumnType("text");

                    b.Property<string[]>("Permissions")
                        .HasColumnType("text[]");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.Property<string>("XUId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", "public");
                });
#pragma warning restore 612, 618
        }
    }
}
