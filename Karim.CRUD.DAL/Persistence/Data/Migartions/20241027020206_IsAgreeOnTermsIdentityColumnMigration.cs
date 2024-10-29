using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karim.CRUD.DAL.Persistence.Data.Migartions
{
    /// <inheritdoc />
    public partial class IsAgreeOnTermsIdentityColumnMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAgreeOnTerms",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAgreeOnTerms",
                table: "AspNetUsers");
        }
    }
}
