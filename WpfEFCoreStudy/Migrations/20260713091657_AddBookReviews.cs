using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfEFCoreStudy.Migrations
{
    /// <inheritdoc />
    public partial class AddBookReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookReviews",
                columns: table => new
                {
                    BookReviewId = table.Column<long>(type: "INTEGER", nullable: false, comment: "書評 ID")
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<long>(type: "INTEGER", nullable: true, comment: "書籍 ID"),
                    BookReviewContent = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, comment: "書評"),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true, comment: "作成日時"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true, comment: "更新日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReviews", x => x.BookReviewId);
                    table.ForeignKey(
                        name: "FK_BookReviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_BookId",
                table: "BookReviews",
                column: "BookId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookReviews");
        }
    }
}
