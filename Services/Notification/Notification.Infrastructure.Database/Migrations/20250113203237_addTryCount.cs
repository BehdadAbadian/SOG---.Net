using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notification.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class addTryCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TryCount",
                table: "Emails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TryCount",
                table: "Emails");
        }
    }
}
