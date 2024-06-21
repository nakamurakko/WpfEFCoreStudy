using Microsoft.EntityFrameworkCore;
using WpfEFCoreStudy.DB.Entities;

namespace WpfEFCoreStudy.DB;

/// <summary>
/// 本DBアクセス用 DbContext。
/// </summary>
public sealed class BookDBContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // https://docs.microsoft.com/ja-jp/ef/core/dbcontext-configuration/
        // https://learn.microsoft.com/ja-jp/ef/core/what-is-new/ef-core-7.0/breaking-changes#encrypt-defaults-to-true-for-sql-server-connections
        optionsBuilder.UseSqlite("Data Source=database.sqlite");
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

}
