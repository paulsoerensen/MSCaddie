using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSCaddie.Migrations
{
    /// <inheritdoc />
    public partial class MemberNoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberNo",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberNo",
                table: "AspNetUsers");
        }
    }
}
