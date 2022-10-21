using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IQueueData.Migrations
{
    public partial class NullableAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Queues_Users_AdminId",
                table: "Queues");

            migrationBuilder.DropIndex(
                name: "IX_Queues_AdminId",
                table: "Queues");

            migrationBuilder.AlterColumn<int>(
                name: "MaxRecordNumber",
                table: "Queues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxRecordNumber",
                table: "Queues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Queues_AdminId",
                table: "Queues",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Queues_Users_AdminId",
                table: "Queues",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
