using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.Migrations
{
    /// <inheritdoc />
    public partial class fixpublisheddatename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "BlogPosts",
                newName: "PublishedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishedDate",
                table: "BlogPosts",
                newName: "PublishDate");
        }
    }
}
