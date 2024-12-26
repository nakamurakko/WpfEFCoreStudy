using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfEFCoreStudy.DB.Entities;

/// <summary>
/// 著者クラス。
/// </summary>
[Table(nameof(Author))]
public sealed class Author
{

    public long AuthorId { get; set; }

    [Required]
    [MaxLength(100)]
    public string AuthorName { get; set; } = "";

}
