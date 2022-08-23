using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEFCoreStudy.DataTypes;

/// <summary>
/// 著者クラス。
/// </summary>
[Table(nameof(Author))]
public class Author
{
    public Int64 AuthorId { get; set; }

    [Required]
    [MaxLength(100)]
    public string AuthorName { get; set; }
}
