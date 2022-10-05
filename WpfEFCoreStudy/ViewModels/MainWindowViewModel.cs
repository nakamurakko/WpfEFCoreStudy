using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

    public Task Initialization { get; private set; }

    /// <summary>
    /// コンストラクター。
    /// </summary>
    public MainWindowViewModel()
    {
        Initialization = InitializeAsync();
    }

    /// <summary>
    /// 非同期で初期化する。
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task InitializeAsync()
    {
        var books = await BookModel.GetBooksAsync();
        foreach (var book in books)
        {
            Books.Add(book);
        }
    }

}
