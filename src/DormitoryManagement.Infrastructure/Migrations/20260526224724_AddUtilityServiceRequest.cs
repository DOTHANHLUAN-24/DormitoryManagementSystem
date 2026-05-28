using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUtilityServiceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UtilityServiceRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UtilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilityServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilityServiceRequests_AspNetUsers_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilityServiceRequests_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilityServiceRequests_Utilities_UtilityId",
                        column: x => x.UtilityId,
                        principalTable: "Utilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtilityServiceRequests_RequesterId",
                table: "UtilityServiceRequests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilityServiceRequests_RoomId",
                table: "UtilityServiceRequests",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilityServiceRequests_UtilityId",
                table: "UtilityServiceRequests",
                column: "UtilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtilityServiceRequests");
        }
    }
}
