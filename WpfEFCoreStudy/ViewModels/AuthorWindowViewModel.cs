using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WpfEFCoreStudy.DB.Entities;
using WpfEFCoreStudy.Models;
using WpfEFCoreStudy.Services.Interfaces;

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

    private readonly IDialogService _dialogService = App.Current.Services.GetService<IDialogService>();

    /// <summary>
    /// 著者を追加する。
    /// </summary>
    [RelayCommand]
    private async Task AddAuthor()
    {
        await BookModel.AddAuthor(this.Author);

        this._dialogService.CloseWindowByViewModel(this);
    }

}
