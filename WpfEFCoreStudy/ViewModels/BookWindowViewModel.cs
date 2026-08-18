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
using WpfEFCoreStudy.ViewModels.Common;

namespace WpfEFCoreStudy.ViewModels;

/// <summary>
/// BookWindow 用 ViewModel。
/// </summary>
public sealed partial class BookWindowViewModel : ObservableObject, IAsyncInitialization
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

    public BookWindowViewModel(long? bookId, DisplayMode displayMode)
    {
        this.Initialization = this.InitializeAsync(bookId, displayMode);
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
    private async Task InitializeAsync(long? bookId, DisplayMode displayMode)
    {
        List<Author> authors = await BookModel.GetAuthorsAsync();
        this.Authors = new ObservableCollection<Author>(authors);

        if (bookId.HasValue)
        {
            this.Book = await BookModel.GetBookByIdAsync(bookId.Value);
            // ComboBox の選択値と一致させるため、一覧のインスタンスを設定する。
            this.Book.Author = this.Authors.FirstOrDefault(x => x.AuthorId == bookId);
        }
        else
        {
            this.Book = new();
        }
        this.SetDisplayMode(bookId == null ? DisplayMode.Add : displayMode);
    }

    /// <summary>
    /// 書籍を追加する。
    /// </summary>
    [RelayCommand]
    private async Task AddBookAsync()
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
    private async Task UpdateBookAsync()
    {
        await BookModel.UpdateBookAsync(this.Book!);

        this._dialogService.CloseWindowByViewModel(this);
    }

}
