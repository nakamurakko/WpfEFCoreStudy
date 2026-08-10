using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfEFCoreStudy.Migrations
{
    /// <inheritdoc />
    public partial class AddBookReviewComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "BookReviews",
                comment: "書評");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "BookReviews",
                oldComment: "書評");
        }
    }
}
