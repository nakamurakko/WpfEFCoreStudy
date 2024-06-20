using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfEFCoreStudy.Constants;
using WpfEFCoreStudy.DataTypes;
using WpfEFCoreStudy.Models;
using WpfEFCoreStudy.Services.Interfaces;

namespace WpfEFCoreStudy.ViewModels;

public partial class BookWindowViewModel : ObservableObject
{

    [ObservableProperty]
    private string _title = "本情報";

    [ObservableProperty]
    private Book _book;

    [ObservableProperty]
    private DisplayMode _displayMode;

    [ObservableProperty]
    private bool _isReadonly = false;

    [ObservableProperty]
    private ObservableCollection<Author> _authors = new ObservableCollection<Author>();

    private readonly IDialogService _dialogService = App.Current.Services.GetService<IDialogService>();

    public Task Initialization { get; private set; }

    public BookWindowViewModel() : this(null, DisplayMode.Add)
    {
    }

    public BookWindowViewModel(Book book, DisplayMode displayMode)
    {
        this.Initialization = this.InitializeAsync();

        this.Book = book ?? new Book();
        this.SetDisplayMode(book == null ? DisplayMode.Add : displayMode);
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
    private async Task InitializeAsync()
    {
        IEnumerable<Author> authors = await BookModel.GetAuthorsAsync();
        foreach (Author author in authors)
        {
            this.Authors.Add(author);
        }
    }

    /// <summary>
    /// 本を追加する。
    /// </summary>
    [RelayCommand]
    private void AddBook()
    {
        this.Book.AuthorId = this.Book.Author?.AuthorId;

        BookModel.AddBook(this.Book);

        this._dialogService.CloseWindowByViewModel(this);
    }

}
