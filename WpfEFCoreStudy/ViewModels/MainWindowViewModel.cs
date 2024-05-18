using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfEFCoreStudy.DataTypes;
using WpfEFCoreStudy.Models;
using WpfEFCoreStudy.ViewModels.Common;

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
        if (string.IsNullOrWhiteSpace(this.SearchTitle))
        {
            return;
        }

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

}
