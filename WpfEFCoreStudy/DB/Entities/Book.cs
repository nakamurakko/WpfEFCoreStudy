using System.ComponentModel.DataAnnotations;

namespace WpfEFCoreStudy.DB.Entities;

/// <summary>
/// 本クラス。
/// </summary>
public sealed class Book
{

    public long BookId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = "";

    public long? AuthorId { get; set; }

    public Author? Author { get; set; }

}
