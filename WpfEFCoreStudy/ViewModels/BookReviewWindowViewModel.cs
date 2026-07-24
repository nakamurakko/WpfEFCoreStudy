using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
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

    public BookReviewWindowViewModel() : this(null)
    {
    }

    public BookReviewWindowViewModel(long? bookId)
    {
        this.Initialization = this.InitializeAsync(bookId!.Value);
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
