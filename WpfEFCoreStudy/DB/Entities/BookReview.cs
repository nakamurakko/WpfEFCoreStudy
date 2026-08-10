using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace WpfEFCoreStudy.DB.Entities;

/// <summary>
/// 書評クラス。
/// </summary>
[Comment("書評")]
public sealed class BookReview : IHasDbTimestamps
{

    /// <summary>書評 ID</summary>
    [Comment("書評 ID")]
    public long BookReviewId { get; set; }

    /// <summary>書籍 ID</summary>
    [Comment("書籍 ID")]
    public long? BookId { get; set; }

    /// <summary>内容</summary>
    [Comment("書評")]
    [MaxLength(1000)]
    public string? BookReviewContent { get; set; }

    [Comment("作成日時")]
    public DateTime? CreatedAt { get; set; }

    [Comment("更新日時")]
    public DateTime? UpdatedAt { get; set; }

    public Book? Book { get; set; }

}
