using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfEFCoreStudy.DataTypes;

/// <summary>
/// 本クラス。
/// </summary>
[Table(nameof(Book))]
public class Book
{

    public long BookId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    public long? AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId)), NotMapped]
    public Author Author { get; set; }

}
