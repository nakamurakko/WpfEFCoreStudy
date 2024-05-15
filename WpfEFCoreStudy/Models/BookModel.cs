using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfEFCoreStudy.DataTypes;
using WpfEFCoreStudy.DB;

namespace WpfEFCoreStudy.Models;

/// <summary>
/// 本DBアクセス用 Model。
/// </summary>
public sealed class BookModel
{

    /// <summary>
    /// 本情報を取得する。
    /// </summary>
    /// <returns></returns>
    public static async Task<IEnumerable<Book>> GetBooksAsync()
    {
        List<Book>? books = new List<Book>();

        using (var dbContext = new BookDBContext())
        {
            books = await dbContext.Books
                 .Join(
                     dbContext.Authors,
                     book => book.AuthorId,
                     author => author.AuthorId,
                     (book, author) =>
                         new Book
                         {
                             BookId = book.BookId,
                             Title = book.Title,
                             AuthorId = book.AuthorId,
                             Author = book.Author
                         }
                 )
                 .ToListAsync();
        }

        return books;
    }

    /// <summary>
    /// 本情報を取得する。
    /// </summary>
    /// <param name="title">本のタイトル。部分一致検索する。</param>
    /// <returns>本情報の一覧。</returns>
    public static async Task<IEnumerable<Book>> GetBooksAsync(string title)
    {
        List<Book> books = new List<Book>();

        using (BookDBContext dbContext = new BookDBContext())
        {
            books = await dbContext.Books
                .GroupJoin(
                    dbContext.Authors,
                    book => book.AuthorId,
                    author => author.AuthorId,
                    (book, author) => new { book, author }
                )
                .SelectMany(
                    bookAndAuthor => bookAndAuthor.author.DefaultIfEmpty(),
                    (bookAndAuthor, author) =>
                    new Book()
                    {
                        BookId = bookAndAuthor.book.BookId,
                        Title = bookAndAuthor.book.Title,
                        AuthorId = bookAndAuthor.book.AuthorId,
                        Author = author
                    }
                )
                .Where(book => book.Title.Contains(title))
                .ToListAsync();
        }

        return books;
    }

}
