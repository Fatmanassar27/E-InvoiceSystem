using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Invoice.Infrastructure._Identity.Migrations
{
    /// <inheritdoc />
    public partial class updateApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIntermediary",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Onbehalfof",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIntermediary",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Onbehalfof",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
