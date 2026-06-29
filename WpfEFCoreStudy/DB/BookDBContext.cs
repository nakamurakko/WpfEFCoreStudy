using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using WpfEFCoreStudy.DB.Entities;

namespace WpfEFCoreStudy.DB;

/// <summary>
/// 本DBアクセス用 DbContext。
/// </summary>
public sealed class BookDBContext : DbContext
{

    /// <summary>
    /// コンストラクター。
    /// </summary>
    public BookDBContext()
    {
        this.ChangeTracker.StateChanged += this.TimestampsChanged;
        this.ChangeTracker.Tracked += this.TimestampsChanged;
    }

    //public BookDBContext(DbContextOptions<BookDBContext> options) : base(options)
    //{
    //    // PooledDbContextFactory を使用する場合に必要。
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // PooledDbContextFactory を使用しない場合に必要。
        // https://docs.microsoft.com/ja-jp/ef/core/dbcontext-configuration/
        // https://learn.microsoft.com/ja-jp/ef/core/what-is-new/ef-core-7.0/breaking-changes#encrypt-defaults-to-true-for-sql-server-connections
        optionsBuilder.UseSqlite("Data Source=database.sqlite");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(author => author.Books)
            .WithOne(book => book.Author)
            .HasPrincipalKey(author => author.AuthorId)
            .HasForeignKey(book => book.AuthorId);
    }

    /// <summary>
    /// 日時更新のイベント。
    /// </summary>
    /// <param name="sender">通知元のオブジェクト。</param>
    /// <param name="e">イベントデータ。</param>
    /// <remarks>
    /// https://learn.microsoft.com/ja-jp/ef/core/logging-events-diagnostics/events
    /// </remarks>
    private void TimestampsChanged(object? sender, EntityEntryEventArgs e)
    {
        if (e.Entry.Entity is IDbTimestamps entityWithTimestamps)
        {
            switch (e.Entry.State)
            {
                case EntityState.Added:
                    DateTime now = DateTime.Now;
                    entityWithTimestamps.CreatedAt = now;
                    entityWithTimestamps.UpdatedAt = now;
                    break;
                case EntityState.Modified:
                    entityWithTimestamps.UpdatedAt = DateTime.Now;
                    break;
            }
        }
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

}
