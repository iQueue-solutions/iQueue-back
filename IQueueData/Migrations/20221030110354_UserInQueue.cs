using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IQueueData.Migrations
{
    public partial class UserInQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueueRecords");

            migrationBuilder.CreateTable(
                name: "UserQueueCollection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QueueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQueueCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserQueueCollection_Queues_QueueId",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserQueueCollection_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserQueueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_UserQueueCollection_UserQueueId",
                        column: x => x.UserQueueId,
                        principalTable: "UserQueueCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_UserQueueId",
                table: "Records",
                column: "UserQueueId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQueueCollection_QueueId",
                table: "UserQueueCollection",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQueueCollection_UserId",
                table: "UserQueueCollection",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "UserQueueCollection");

            migrationBuilder.CreateTable(
                name: "QueueRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QueueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    LabNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueueRecords_Queues_QueueId",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueueRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueueRecords_QueueId",
                table: "QueueRecords",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueRecords_UserId",
                table: "QueueRecords",
                column: "UserId");
        }
    }
}
