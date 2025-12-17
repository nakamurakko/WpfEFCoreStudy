using Microsoft.EntityFrameworkCore;
using WpfEFCoreStudy.DB.Entities;

namespace WpfEFCoreStudy.DB;

/// <summary>
/// 本DBアクセス用 DbContext。
/// </summary>
public sealed class BookDBContext : DbContext
{

    /// <summary>接続文字列。(本来は環境変数に設定すべき)</summary>
    public readonly static string ConnectionString = "Data Source=database.sqlite";

    public BookDBContext(DbContextOptions<BookDBContext> options) : base(options)
    {
        // PooledDbContextFactory を使用する場合に必要。
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // PooledDbContextFactory を使用しない場合に必要。
        // https://docs.microsoft.com/ja-jp/ef/core/dbcontext-configuration/
        // https://learn.microsoft.com/ja-jp/ef/core/what-is-new/ef-core-7.0/breaking-changes#encrypt-defaults-to-true-for-sql-server-connections
        //optionsBuilder.UseSqlite("Data Source=database.sqlite");
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(author => author.Books)
            .WithOne(book => book.Author)
            .HasForeignKey(book => book.AuthorId);

        modelBuilder.Entity<Book>()
            .HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .HasForeignKey(book => book.AuthorId);
    }

}
