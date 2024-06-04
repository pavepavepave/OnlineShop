using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class AddTutoringSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TutoringSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSlotId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutoringSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutoringSessions_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "EndTime", "IsAvailable", "StartTime" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 10, 30, 0, 0), true, new TimeSpan(0, 9, 0, 0, 0) },
                    { 2, new TimeSpan(0, 12, 0, 0, 0), true, new TimeSpan(0, 10, 30, 0, 0) },
                    { 3, new TimeSpan(0, 14, 30, 0, 0), true, new TimeSpan(0, 13, 0, 0, 0) },
                    { 4, new TimeSpan(0, 16, 0, 0, 0), true, new TimeSpan(0, 14, 30, 0, 0) },
                    { 5, new TimeSpan(0, 17, 30, 0, 0), true, new TimeSpan(0, 16, 0, 0, 0) },
                    { 6, new TimeSpan(0, 19, 0, 0, 0), true, new TimeSpan(0, 17, 30, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutoringSessions_TimeSlotId",
                table: "TutoringSessions",
                column: "TimeSlotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutoringSessions");

            migrationBuilder.DropTable(
                name: "TimeSlots");
        }
    }
}
