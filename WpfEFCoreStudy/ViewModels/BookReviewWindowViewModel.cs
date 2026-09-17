using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WpfEFCoreStudy.DB.Entities;
using WpfEFCoreStudy.Models;
using WpfEFCoreStudy.Services.Interfaces;
using WpfEFCoreStudy.ViewModels.Common;

namespace WpfEFCoreStudy.ViewModels;

/// <summary>
/// BookReviewWindow 用 ViewModel。
/// </summary>
public sealed partial class BookReviewWindowViewModel : ObservableObject, IAsyncInitialization
{

    [ObservableProperty]
    private string _title = "書評";

    [ObservableProperty]
    private Book? _book;

    [ObservableProperty]
    private BookReview? _bookReview;

    private readonly IDialogService _dialogService = App.Current.Services.GetRequiredService<IDialogService>();

    public Task Initialization { get; private set; }

    /// <summary>
    /// コンストラクター。
    /// </summary>
    /// <remarks>
    /// DialogService.ShowDialog でデフォルトコンストラクターを指定しているため用意しているが、
    /// 使用した場合は引数付きコンストラクターで例外を発生させる。
    /// </remarks>
    public BookReviewWindowViewModel() : this(0)
    {
    }

    public BookReviewWindowViewModel(long bookId)
    {
        if (bookId <= 0)
        {
            throw new ArgumentException("bookId must be greater than 0.", nameof(bookId));
        }

        this.Initialization = this.InitializeAsync(bookId);
    }

    private async Task InitializeAsync(long bookId)
    {
        this.Book = await BookModel.GetBookByIdAsync(bookId);
        this.BookReview = this.Book.BookReview ?? new BookReview();
    }

    /// <summary>
    /// 書評を保存する。
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task SaveBookReviewAsync()
    {
        await BookModel.UpdateBookReviewAsync(this.Book!.BookId, this.BookReview!);
        this._dialogService.CloseWindowByViewModel(this);
    }

}
