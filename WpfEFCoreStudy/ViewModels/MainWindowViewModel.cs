using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfEFCoreStudy.Constants;
using WpfEFCoreStudy.DataTypes;
using WpfEFCoreStudy.Models;
using WpfEFCoreStudy.Services;
using WpfEFCoreStudy.ViewModels.Common;
using WpfEFCoreStudy.Views;

namespace WpfEFCoreStudy.ViewModels;

/// <summary>
/// MainWindow用ViewModel。
/// </summary>
public partial class MainWindowViewModel : ObservableObject, IAsyncInitialization
{

    [ObservableProperty]
    private string _title = "WpfEFCoreStudy";

    [ObservableProperty]
    private ObservableCollection<Book> _books = new ObservableCollection<Book>();

    /// <summary>
    /// 検索対象の本のタイトル。
    /// </summary>
    [ObservableProperty]
    private string _searchTitle = "";

    /// <summary>
    /// 検索対象の著者名。
    /// </summary>
    [ObservableProperty]
    private string _searchAuthorName = "";

    public Task Initialization { get; private set; }

    /// <summary>
    /// コンストラクター。
    /// </summary>
    public MainWindowViewModel()
    {
        this.Initialization = this.InitializeAsync();
    }

    /// <summary>
    /// 非同期で初期化する。
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task InitializeAsync()
    {
        IEnumerable<Book>? books = await BookModel.GetBooksAsync();
        foreach (Book book in books)
        {
            this.Books.Add(book);
        }
    }

    /// <summary>
    /// 本を検索する。
    /// </summary>
    [RelayCommand]
    private async Task SearchBooksAsync()
    {
        this.Books.Clear();
        IEnumerable<Book> books = await BookModel.GetBooksAsync(this.SearchTitle, this.SearchAuthorName);
        foreach (Book book in books)
        {
            this.Books.Add(book);
        }
    }

    /// <summary>
    /// 検索結果をクリアする。
    /// </summary>
    [RelayCommand]
    private async Task ClearSearchResultAsync()
    {
        this.SearchTitle = "";
        this.SearchAuthorName = "";

        this.Books.Clear();
        IEnumerable<Book> books = await BookModel.GetBooksAsync();
        foreach (Book book in books)
        {
            this.Books.Add(book);
        }
    }

    /// <summary>
    /// 本の詳細を表示する。
    /// </summary>
    /// <param name="book">本情報。</param>
    [RelayCommand]
    private void ShowBookDetail(Book book)
    {
        BookWindowViewModel viewModel = new BookWindowViewModel(book, DisplayMode.ReadOnly);
        DialogService.GetInstance.ShowDialog<BookWindow, BookWindowViewModel>(viewModel);
    }

    /// <summary>
    /// 著者を追加する。
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task AddAuthorAsync()
    {
        DialogService.GetInstance.ShowDialog<AuthorWindow, AuthorWindowViewModel>();

        this.Books.Clear();
        IEnumerable<Book> books = await BookModel.GetBooksAsync();
        foreach (Book book in books)
        {
            this.Books.Add(book);
        }
    }

    /// <summary>
    /// 本を追加する。
    /// </summary>
    [RelayCommand]
    private async Task AddBookAsync()
    {
        DialogService.GetInstance.ShowDialog<BookWindow, BookWindowViewModel>();

        this.Books.Clear();
        IEnumerable<Book> books = await BookModel.GetBooksAsync();
        foreach (Book book in books)
        {
            this.Books.Add(book);
        }
    }

}
