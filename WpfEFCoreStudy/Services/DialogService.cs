using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using WpfEFCoreStudy.Services.Interfaces;

namespace WpfEFCoreStudy.Services;

public class DialogService : IDialogService
{

    public static IDialogService GetInstance { get; } = new DialogService();

    private static Dictionary<ObservableObject, Window> viewModelWindows = new Dictionary<ObservableObject, Window>();

    public void Show<TWindow, TViewModel>(TViewModel viewModel = null)
        where TWindow : Window, new()
        where TViewModel : ObservableObject, new()
    {
        TWindow w = new TWindow();
        TViewModel v = viewModel ?? new TViewModel();
        w.DataContext = v;

        viewModelWindows.Add(v, w);

        w.Show();
    }

    public void ShowDialog<TWindow, TViewModel>(TViewModel viewModel = null)
        where TWindow : Window, new()
        where TViewModel : ObservableObject, new()
    {
        TWindow w = new TWindow();
        TViewModel v = viewModel ?? new TViewModel();
        w.DataContext = v;

        viewModelWindows.Add(v, w);

        w.ShowDialog();
    }

    public void TerminateWindow(ObservableObject viewModel)
    {
        if (viewModelWindows.TryGetValue(viewModel, out Window w))
        {
            viewModelWindows.Remove(viewModel);
            w?.Close();
        }
    }

}
