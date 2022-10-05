using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEFCoreStudy.DataTypes;
using WpfEFCoreStudy.DB;

namespace WpfEFCoreStudy.Models;

/// <summary>
/// 本DBアクセス用 Model。
/// </summary>
public class BookModel
{
    /// <summary>
    /// 本情報を取得する。
    /// </summary>
    /// <returns></returns>
    public static async Task<IEnumerable<Book>> GetBooksAsync()
    {
        var books = new List<Book>();

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
}
