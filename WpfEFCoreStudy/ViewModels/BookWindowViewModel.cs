using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfEFCoreStudy.Constants;
using WpfEFCoreStudy.DB.Entities;
using WpfEFCoreStudy.Models;
using WpfEFCoreStudy.Services.Interfaces;

namespace WpfEFCoreStudy.ViewModels;

public sealed partial class BookWindowViewModel : ObservableObject
{

    [ObservableProperty]
    private string _title = "書籍情報";

    [ObservableProperty]
    private Book? _book;

    [ObservableProperty]
    private DisplayMode _displayMode;

    [ObservableProperty]
    private bool _isReadonly = false;

    [ObservableProperty]
    private ObservableCollection<Author> _authors = new();

    private readonly IDialogService _dialogService = App.Current.Services.GetRequiredService<IDialogService>();

    public Task Initialization { get; private set; }

    public BookWindowViewModel() : this(null, DisplayMode.Add)
    {
    }

    public BookWindowViewModel(Book? book, DisplayMode displayMode)
    {
        this.Initialization = this.InitializeAsync(book, displayMode);
    }

    /// <summary>
    /// DisplayMode 変更時処理。
    /// </summary>
    /// <param name="value"><see cref="DisplayMode"/></param>
    public void SetDisplayMode(DisplayMode value)
    {
        this.DisplayMode = value;
        switch (this.DisplayMode)
        {
            case DisplayMode.Add:
                this.Title = "本を追加";
                this.IsReadonly = false;

                break;
            case DisplayMode.Edit:
                this.Title = "本を編集";
                this.IsReadonly = false;

                break;
            case DisplayMode.ReadOnly:
                this.Title = "本情報";
                this.IsReadonly = true;

                break;
        }
    }

    /// <summary>
    /// 非同期で初期化する。
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task InitializeAsync(Book? book, DisplayMode displayMode)
    {
        IEnumerable<Author> authors = await BookModel.GetAuthorsAsync();
        this.Authors = new ObservableCollection<Author>(authors);

        if (book == null)
        {
            this.Book = new();
        }
        else
        {
            this.Book = new Book()
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                Author = this.Authors.FirstOrDefault(x => x.AuthorId == book.AuthorId),
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            };
        }
        this.SetDisplayMode(book == null ? DisplayMode.Add : displayMode);
    }

    /// <summary>
    /// 書籍を追加する。
    /// </summary>
    [RelayCommand]
    private async Task AddBook()
    {
        this.Book!.AuthorId = this.Book.Author?.AuthorId;

        await BookModel.AddBookAsync(this.Book);

        this._dialogService.CloseWindowByViewModel(this);
    }

    /// <summary>
    /// 書籍を更新する。
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task UpdateBook()
    {
        await BookModel.UpdateBookAsync(this.Book!);

        this._dialogService.CloseWindowByViewModel(this);
    }

}
