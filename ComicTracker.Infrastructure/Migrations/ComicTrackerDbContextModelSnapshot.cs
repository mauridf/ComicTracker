﻿// <auto-generated />
using System;
using ComicTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ComicTracker.Infrastructure.Migrations
{
    [DbContext(typeof(ComicTrackerDbContext))]
    partial class ComicTrackerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ComicTracker.Domain.Entities.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Aliases")
                        .HasColumnType("text");

                    b.Property<string>("Birth")
                        .HasColumnType("text");

                    b.Property<int>("ComicVineId")
                        .HasColumnType("integer");

                    b.Property<int?>("CountOfIssueAppearances")
                        .HasColumnType("integer");

                    b.Property<string>("Deck")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FirstAppearedInIssue")
                        .HasColumnType("text");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Origin")
                        .HasColumnType("text");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<string>("PublisherName")
                        .HasColumnType("text");

                    b.Property<string>("RealName")
                        .HasColumnType("text");

                    b.Property<string>("SiteDetailUrl")
                        .HasColumnType("text");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComicVineId")
                        .IsUnique();

                    b.HasIndex("PublisherId");

                    b.HasIndex("TeamId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Aliases")
                        .HasColumnType("text");

                    b.Property<int>("ComicVineId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CoverDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Deck")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool?>("HasStaffReview")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<int>("IssueNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer");

                    b.Property<bool>("Read")
                        .HasColumnType("boolean");

                    b.Property<string>("SiteDetailUrl")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StoreDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("VolumeDetails")
                        .HasColumnType("text");

                    b.Property<int>("VolumeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComicVineId")
                        .IsUnique();

                    b.HasIndex("VolumeId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Aliases")
                        .HasColumnType("text");

                    b.Property<int>("ComicVineId")
                        .HasColumnType("integer");

                    b.Property<string>("Deck")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("LocationAddress")
                        .HasColumnType("text");

                    b.Property<string>("LocationCity")
                        .HasColumnType("text");

                    b.Property<string>("LocationState")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("SiteDetailUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ComicVineId")
                        .IsUnique();

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Aliases")
                        .HasColumnType("text");

                    b.Property<int>("ComicVineId")
                        .HasColumnType("integer");

                    b.Property<int?>("CountOfIssueAppearances")
                        .HasColumnType("integer");

                    b.Property<int?>("CountOfTeamMembers")
                        .HasColumnType("integer");

                    b.Property<string>("Deck")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FirstAppearedInIssue")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<string>("PublisherName")
                        .HasColumnType("text");

                    b.Property<string>("SiteDetailUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ComicVineId")
                        .IsUnique();

                    b.HasIndex("PublisherId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Volume", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Aliases")
                        .HasColumnType("text");

                    b.Property<int?>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<int>("ComicVineId")
                        .HasColumnType("integer");

                    b.Property<int?>("CountOfIssues")
                        .HasColumnType("integer");

                    b.Property<string>("Deck")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("FirstIssue")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<int?>("LastIssue")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<string>("PublisherName")
                        .HasColumnType("text");

                    b.Property<string>("SiteDetailUrl")
                        .HasColumnType("text");

                    b.Property<int?>("StartYear")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ComicVineId")
                        .IsUnique();

                    b.HasIndex("PublisherId");

                    b.ToTable("Volumes");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Character", b =>
                {
                    b.HasOne("ComicTracker.Domain.Entities.Publisher", "Publisher")
                        .WithMany("Characters")
                        .HasForeignKey("PublisherId");

                    b.HasOne("ComicTracker.Domain.Entities.Team", null)
                        .WithMany("Characters")
                        .HasForeignKey("TeamId");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Issue", b =>
                {
                    b.HasOne("ComicTracker.Domain.Entities.Volume", "Volume")
                        .WithMany("Issues")
                        .HasForeignKey("VolumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Volume");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Team", b =>
                {
                    b.HasOne("ComicTracker.Domain.Entities.Publisher", "Publisher")
                        .WithMany("Teams")
                        .HasForeignKey("PublisherId");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Volume", b =>
                {
                    b.HasOne("ComicTracker.Domain.Entities.Character", null)
                        .WithMany("Volumes")
                        .HasForeignKey("CharacterId");

                    b.HasOne("ComicTracker.Domain.Entities.Publisher", "Publisher")
                        .WithMany("Volumes")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Character", b =>
                {
                    b.Navigation("Volumes");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Publisher", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Teams");

                    b.Navigation("Volumes");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Team", b =>
                {
                    b.Navigation("Characters");
                });

            modelBuilder.Entity("ComicTracker.Domain.Entities.Volume", b =>
                {
                    b.Navigation("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
