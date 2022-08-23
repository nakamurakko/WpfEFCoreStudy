using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEFCoreStudy.DataTypes;

/// <summary>
/// 本クラス。
/// </summary>
[Table(nameof(Book))]
public class Book
{
    public Int64 BookId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    public Int64? AuthorId { get; set; }

    public Author Author { get; set; }
}
