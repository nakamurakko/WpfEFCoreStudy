using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WpfEFCoreStudy.DB.Entities;

/// <summary>
/// 著者クラス。
/// </summary>
[Comment("著者")]
public sealed class Author : IDbTimestamps
{

    /// <summary>著者 ID</summary>
    [Comment("著者 ID")]
    public long AuthorId { get; set; }

    /// <summary>著者名</summary>
    [Required]
    [MaxLength(100)]
    [Comment("著者名")]
    public string AuthorName { get; set; } = "";

    [Comment("作成日時")]
    public DateTime? CreatedAt { get; set; }

    [Comment("更新日時")]
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
