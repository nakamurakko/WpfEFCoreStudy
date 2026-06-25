using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfEFCoreStudy.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCommentsAndColumnComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Books",
                comment: "書籍");

            migrationBuilder.AlterTable(
                name: "Authors",
                comment: "著者");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                comment: "書籍名",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Books",
                type: "INTEGER",
                nullable: true,
                comment: "著者 ID",
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BookId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                comment: "書籍 ID",
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorName",
                table: "Authors",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                comment: "著者名",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Authors",
                type: "INTEGER",
                nullable: false,
                comment: "著者 ID",
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Books",
                oldComment: "書籍");

            migrationBuilder.AlterTable(
                name: "Authors",
                oldComment: "著者");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100,
                oldComment: "書籍名");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Books",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true,
                oldComment: "著者 ID");

            migrationBuilder.AlterColumn<long>(
                name: "BookId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldComment: "書籍 ID")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorName",
                table: "Authors",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100,
                oldComment: "著者名");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Authors",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldComment: "著者 ID")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
