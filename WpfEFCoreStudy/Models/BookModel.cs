using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfEFCoreStudy.DB;
using WpfEFCoreStudy.DB.Entities;

namespace WpfEFCoreStudy.Models;

/// <summary>
/// 書籍 DB アクセス用 Model。
/// </summary>
public static class BookModel
{

    // PooledDbContextFactory を使用する場合。
    //private static readonly IDbContextFactory<BookDBContext> _dbContextFactory = App.Current.Services.GetRequiredService<IDbContextFactory<BookDBContext>>();

    /// <summary>
    /// 著者の一覧を取得する。
    /// </summary>
    /// <returns>著者の一覧。</returns>
    public static async Task<List<Author>> GetAuthorsAsync()
    {
        await using ApplicationDbContext dbContext = new();

        return await dbContext.Authors
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// 書籍情報を取得する。
    /// </summary>
    /// <param name="bookId">書籍 ID。</param>
    /// <returns>書籍情報</returns>
    public static async Task<Book> GetBookByIdAsync(long bookId)
    {
        await using ApplicationDbContext dbContext = new();

        return await dbContext.Books
            .Include(x => x.Author)
            .Include(x => x.BookReview)
            .Where(x => x.BookId == bookId)
            .AsNoTracking()
            .FirstAsync();
    }

    /// <summary>
    /// 書籍情報を取得する。
    /// </summary>
    /// <param name="title">書籍のタイトル。部分一致検索する。</param>
    /// <param name="authorName">著者名。部分一致検索する。</param>
    /// <returns>書籍情報の一覧。</returns>
    public static async Task<List<Book>> GetBooksAsync(string title = "", string authorName = "")
    {
        await using ApplicationDbContext dbContext = new();

        LinqKit.ExpressionStarter<Book> predicateBuilder = LinqKit.PredicateBuilder.New<Book>(true);
        if (!string.IsNullOrWhiteSpace(title))
        {
            predicateBuilder.Or(x => x.Title.Contains(title));
        }
        if (!string.IsNullOrWhiteSpace(authorName))
        {
            predicateBuilder.Or(x => x.Author.AuthorName.Contains(authorName));
        }

        // Left Join で取得。 <https://learn.microsoft.com/ja-jp/dotnet/csharp/linq/standard-query-operators/join-operations#perform-left-outer-joins>
        //return await dbContext.Books
        //    .GroupJoin(
        //        dbContext.Authors,
        //        book => book.AuthorId,
        //        author => author.AuthorId,
        //        (book, authors) => new { book, authors }
        //    )
        //    .SelectMany(
        //        bookAndAuthor => bookAndAuthor.authors.DefaultIfEmpty(),
        //        (bookAndAuthor, author) =>
        //        new Book()
        //        {
        //            BookId = bookAndAuthor.book.BookId,
        //            Title = bookAndAuthor.book.Title,
        //            AuthorId = bookAndAuthor.book.AuthorId,
        //            Author = author
        //        }
        //    )
        //    .Where(predicateBuilder)
        //    .ToListAsync();

        //// .NET 10 以降では LeftJoin を使う。(LinqKit に LeftJoin が存在するため注意する。)
        //return await dbContext.Books
        //    .LeftJoin(
        //        dbContext.Authors,
        //        book => book.AuthorId,
        //        author => author.AuthorId,
        //        (book, author) =>
        //        new Book()
        //        {
        //            BookId = book.BookId,
        //            Title = book.Title,
        //            AuthorId = book.AuthorId,
        //            Author = author
        //        }
        //    )
        //    .Where(predicateBuilder)
        //    .ToListAsync();

        // QueryableExtensions.Include を使う。
        return await dbContext.Books
            .Include(x => x.Author)
            .Include(x => x.BookReview)
            .Where(predicateBuilder)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// 著者を追加する。
    /// </summary>
    /// <param name="author">著者。</param>
    /// <returns></returns>
    public static async Task AddAuthorAsync(Author author)
    {
        await using ApplicationDbContext dbContext = new();

        await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await dbContext.Authors.AddAsync(
                new Author()
                {
                    AuthorName = author.AuthorName
                }
            );
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            throw;
        }
    }

    /// <summary>
    /// 書籍を追加する。
    /// </summary>
    /// <param name="book">書籍情報。</param>
    /// <returns></returns>
    public static async Task AddBookAsync(Book book)
    {
        await using ApplicationDbContext dbContext = new();

        await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await dbContext.Books.AddAsync(
                new Book()
                {
                    Title = book.Title,
                    AuthorId = book?.Author?.AuthorId,
                }
            );
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            throw;
        }
    }

    /// <summary>
    /// 書籍を更新する。
    /// </summary>
    /// <param name="book">書籍情報。</param>
    /// <returns></returns>
    public static async Task UpdateBookAsync(Book book)
    {
        await using ApplicationDbContext dbContext = new();

        await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            Book targetBook = await dbContext.Books
                .Where(x => x.BookId == book.BookId)
                .FirstAsync();

            targetBook.Title = book.Title;
            targetBook.AuthorId = book.Author?.AuthorId;
            dbContext.Books.Update(targetBook);

            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            throw;
        }
    }

    /// <summary>
    /// 書評を更新する。
    /// </summary>
    /// <param name="bookId">書籍 ID。</param>
    /// <param name="bookReview">書評。</param>
    /// <returns></returns>
    public static async Task UpdateBookReviewAsync(long bookId, BookReview bookReview)
    {
        await using ApplicationDbContext dbContext = new();

        await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            BookReview? targetBookReview = await dbContext.BookReviews
                .Where(x => x.BookId == bookId)
                .FirstOrDefaultAsync();

            if (targetBookReview == null)
            {
                targetBookReview = new()
                {
                    BookId = bookId,
                    BookReviewContent = bookReview.BookReviewContent,
                };
                await dbContext.AddAsync(targetBookReview);
            }
            else
            {
                targetBookReview.BookReviewContent = bookReview.BookReviewContent;
                dbContext.BookReviews.Update(targetBookReview);
            }

            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            throw;
        }
    }

}
