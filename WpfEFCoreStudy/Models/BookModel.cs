﻿using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfEFCoreStudy.DB;
using WpfEFCoreStudy.DB.Entities;

namespace WpfEFCoreStudy.Models;

/// <summary>
/// 本DBアクセス用 Model。
/// </summary>
public sealed class BookModel
{

    /// <summary>
    /// 著者の一覧を取得する。
    /// </summary>
    /// <returns>著者の一覧。</returns>
    public static async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        using (BookDBContext dbContext = new BookDBContext())
        {
            return await dbContext.Authors.ToListAsync();
        }
    }

    /// <summary>
    /// 本情報を取得する。
    /// </summary>
    /// <param name="title">本のタイトル。部分一致検索する。</param>
    /// <param name="authorName">著者名。部分一致検索する。</param>
    /// <returns>本情報の一覧。</returns>
    public static async Task<IEnumerable<Book>> GetBooksAsync(string title = "", string authorName = "")
    {
        using (BookDBContext dbContext = new BookDBContext())
        {
            ExpressionStarter<Book> predicateBuilder = PredicateBuilder.New<Book>(true);
            if (!string.IsNullOrWhiteSpace(title))
            {
                predicateBuilder.Or(x => x.Title.Contains(title));
            }
            if (!string.IsNullOrWhiteSpace(authorName))
            {
                predicateBuilder.Or(x => x.Author.AuthorName.Contains(authorName));
            }

            // Left Join で取得。 <https://learn.microsoft.com/ja-jp/dotnet/csharp/linq/standard-query-operators/join-operations#perform-left-outer-joins>
            return await dbContext.Books
                .GroupJoin(
                    dbContext.Authors,
                    book => book.AuthorId,
                    author => author.AuthorId,
                    (book, authors) => new { book, authors }
                )
                .SelectMany(
                    bookAndAuthor => bookAndAuthor.authors.DefaultIfEmpty(),
                    (bookAndAuthor, author) =>
                    new Book()
                    {
                        BookId = bookAndAuthor.book.BookId,
                        Title = bookAndAuthor.book.Title,
                        AuthorId = bookAndAuthor.book.AuthorId,
                        Author = author
                    }
                )
                .Where(predicateBuilder)
                .ToListAsync();
        }
    }

    /// <summary>
    /// 著者を追加する。
    /// </summary>
    /// <param name="author">著者。</param>
    public static void AddAuthor(Author author)
    {
        using (BookDBContext dbContext = new BookDBContext())
        {
            dbContext.Authors.Add(author);

            dbContext.SaveChanges();
        }
    }

    /// <summary>
    /// 本を追加する。
    /// </summary>
    /// <param name="book">本情報。</param>
    public static void AddBook(Book book)
    {
        using (BookDBContext dbContext = new BookDBContext())
        {
            dbContext.Books.Add(book);

            dbContext.SaveChanges();
        }
    }

}
