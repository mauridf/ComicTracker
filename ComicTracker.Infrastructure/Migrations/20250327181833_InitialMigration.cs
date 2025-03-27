using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ComicTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComicVineId = table.Column<int>(type: "integer", nullable: false),
                    Aliases = table.Column<string>(type: "text", nullable: true),
                    Deck = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    LocationAddress = table.Column<string>(type: "text", nullable: true),
                    LocationCity = table.Column<string>(type: "text", nullable: true),
                    LocationState = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SiteDetailUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComicVineId = table.Column<int>(type: "integer", nullable: false),
                    Aliases = table.Column<string>(type: "text", nullable: true),
                    CountOfIssueAppearances = table.Column<int>(type: "integer", nullable: true),
                    CountOfTeamMembers = table.Column<int>(type: "integer", nullable: true),
                    Deck = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FirstAppearedInIssue = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PublisherName = table.Column<string>(type: "text", nullable: true),
                    SiteDetailUrl = table.Column<string>(type: "text", nullable: true),
                    PublisherId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComicVineId = table.Column<int>(type: "integer", nullable: false),
                    Aliases = table.Column<string>(type: "text", nullable: true),
                    Birth = table.Column<string>(type: "text", nullable: true),
                    CountOfIssueAppearances = table.Column<int>(type: "integer", nullable: true),
                    Deck = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FirstAppearedInIssue = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Origin = table.Column<string>(type: "text", nullable: true),
                    PublisherName = table.Column<string>(type: "text", nullable: true),
                    RealName = table.Column<string>(type: "text", nullable: true),
                    SiteDetailUrl = table.Column<string>(type: "text", nullable: true),
                    PublisherId = table.Column<int>(type: "integer", nullable: true),
                    TeamId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComicVineId = table.Column<int>(type: "integer", nullable: false),
                    Aliases = table.Column<string>(type: "text", nullable: true),
                    CountOfIssues = table.Column<int>(type: "integer", nullable: true),
                    Deck = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FirstIssue = table.Column<int>(type: "integer", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    LastIssue = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PublisherName = table.Column<string>(type: "text", nullable: true),
                    SiteDetailUrl = table.Column<string>(type: "text", nullable: true),
                    StartYear = table.Column<int>(type: "integer", nullable: true),
                    PublisherId = table.Column<int>(type: "integer", nullable: false),
                    CharacterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volumes_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Volumes_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComicVineId = table.Column<int>(type: "integer", nullable: false),
                    Aliases = table.Column<string>(type: "text", nullable: true),
                    CoverDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Deck = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HasStaffReview = table.Column<bool>(type: "boolean", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    IssueNumber = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SiteDetailUrl = table.Column<string>(type: "text", nullable: true),
                    StoreDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    VolumeDetails = table.Column<string>(type: "text", nullable: true),
                    Read = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: true),
                    VolumeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Volumes_VolumeId",
                        column: x => x.VolumeId,
                        principalTable: "Volumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ComicVineId",
                table: "Characters",
                column: "ComicVineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PublisherId",
                table: "Characters",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_TeamId",
                table: "Characters",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ComicVineId",
                table: "Issues",
                column: "ComicVineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_VolumeId",
                table: "Issues",
                column: "VolumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_ComicVineId",
                table: "Publishers",
                column: "ComicVineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ComicVineId",
                table: "Teams",
                column: "ComicVineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PublisherId",
                table: "Teams",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_CharacterId",
                table: "Volumes",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_ComicVineId",
                table: "Volumes",
                column: "ComicVineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volumes_PublisherId",
                table: "Volumes",
                column: "PublisherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Volumes");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
