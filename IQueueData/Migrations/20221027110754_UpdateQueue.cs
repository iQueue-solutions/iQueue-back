using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IQueueData.Migrations
{
    public partial class UpdateQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CloseTime",
                table: "Queues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Queues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpenTime",
                table: "Queues",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "Queues");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Queues");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "Queues");
        }
    }
}
