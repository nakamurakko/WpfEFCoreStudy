using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WpfEFCoreStudy.DB.Entities;

/// <summary>
/// 著者クラス。
/// </summary>
public sealed class Author
{

    public long AuthorId { get; set; }

    [Required]
    [MaxLength(100)]
    public string AuthorName { get; set; } = "";

    public ICollection<Book> Books { get; set; } = new List<Book>();

}
