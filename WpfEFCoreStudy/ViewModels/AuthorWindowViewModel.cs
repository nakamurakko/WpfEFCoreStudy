using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfEFCoreStudy.DataTypes;
using WpfEFCoreStudy.Models;
using WpfEFCoreStudy.Services;

namespace WpfEFCoreStudy.ViewModels;

/// <summary>
/// AuthorWindow 用 ViewModel。
/// </summary>
public sealed partial class AuthorWindowViewModel : ObservableObject
{

    [ObservableProperty]
    private string _title = "著者を追加";

    [ObservableProperty]
    private Author _author = new Author();

    /// <summary>
    /// 著者を追加する。
    /// </summary>
    [RelayCommand]
    private void AddAuthor()
    {
        BookModel.AddAuthor(this.Author);

        DialogService.GetInstance.TerminateWindow(this);
    }

}
