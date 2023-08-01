using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUSerJWTsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserJWTs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JWT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshJWT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JWTExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshJWTExpirtionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshJWTRevokedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRefreshJWTUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJWTs", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserJWTs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserJWTs_UserId",
                table: "UserJWTs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserJWTs");
        }
    }
}
