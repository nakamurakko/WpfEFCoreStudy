using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace WpfEFCoreStudy.Services.Interfaces;

/// <summary>
/// ダイアログサービス。
/// </summary>
public interface IDialogService
{

    /// <summary>
    /// Window を生成してモードレス表示する。
    /// </summary>
    /// <typeparam name="TWindow">Window を継承したクラス。</typeparam>
    /// <typeparam name="TViewModel">ObservableObject を継承した ViewModel クラス。</typeparam>
    /// <param name="viewModel">ViewModel インスタンス。 null の場合はメソッド内で生成する。</param>
    void Show<TWindow, TViewModel>(TViewModel? viewModel = null)
        where TWindow : Window, new()
        where TViewModel : ObservableObject, new();

    /// <summary>
    /// Window を生成してモーダル表示する。
    /// </summary>
    /// <typeparam name="TWindow">Window を継承したクラス。</typeparam>
    /// <typeparam name="TViewModel">ObservableObject を継承した ViewModel クラス。</typeparam>
    /// <param name="viewModel">ViewModel インスタンス。 null の場合はメソッド内で生成する。</param>
    void ShowDialog<TWindow, TViewModel>(TViewModel? viewModel = null)
        where TWindow : Window, new()
        where TViewModel : ObservableObject, new();

    /// <summary>
    /// ViewModel を指定して画面を終了する。
    /// </summary>
    /// <param name="viewModel">対象の ViewModel。</param>
    public void CloseWindowByViewModel(ObservableObject viewModel);

}
