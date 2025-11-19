using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Invoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editAcceptedDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "AcceptedDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentTypeVersion",
                table: "AcceptedDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "AcceptedDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AcceptedDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "AcceptedDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentTypeVersion",
                table: "AcceptedDocuments");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "AcceptedDocuments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AcceptedDocuments");
        }
    }
}
