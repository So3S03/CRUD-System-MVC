using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.CRUD.DAL.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageColumnMigartion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Employees");
        }
    }
}
