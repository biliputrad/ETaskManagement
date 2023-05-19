using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldNotificationOnTableTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notification",
                table: "Tasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notification",
                table: "Tasks");
        }
    }
}
