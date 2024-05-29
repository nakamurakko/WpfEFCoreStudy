using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WpfEFCoreStudy.Constants;
using WpfEFCoreStudy.DataTypes;
using WpfEFCoreStudy.Models;

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

    [ObservableProperty]
    private Author _selectedAuthor;

    public Task Initialization { get; private set; }

    public BookWindowViewModel()
    {
        this.Initialization = this.InitializeAsync();

        this.Book = new Book();
        this.SelectedAuthor = null;
        this.SetDisplayMode(DisplayMode.Add);
    }

    public BookWindowViewModel(Book book, DisplayMode displayMode)
    {
        this.Initialization = this.InitializeAsync();

        this.Book = book;
        this.SelectedAuthor = this.Authors.FirstOrDefault(x => x.AuthorId == this.Book?.AuthorId);
        this.SetDisplayMode(displayMode);
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

}
