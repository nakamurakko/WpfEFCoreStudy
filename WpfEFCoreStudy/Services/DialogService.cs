using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using WpfEFCoreStudy.Services.Interfaces;

namespace WpfEFCoreStudy.Services;

public sealed class DialogService : IDialogService
{

    private static readonly Dictionary<ObservableObject, Window> viewModelWindows = new();

    public void Show<TWindow, TViewModel>(TViewModel? viewModel = null)
        where TWindow : Window, new()
        where TViewModel : ObservableObject, new()
    {
        TWindow w = new();
        TViewModel v = viewModel ?? new TViewModel();
        w.DataContext = v;

        viewModelWindows.Add(v, w);

        w.Show();
    }

    public void ShowDialog<TWindow, TViewModel>(TViewModel? viewModel = null)
        where TWindow : Window, new()
        where TViewModel : ObservableObject, new()
    {
        TWindow w = new();
        TViewModel v = viewModel ?? new TViewModel();
        w.DataContext = v;

        viewModelWindows.Add(v, w);

        w.ShowDialog();
    }

    public void CloseWindowByViewModel(ObservableObject viewModel)
    {
        if (viewModelWindows.TryGetValue(viewModel, out Window? w))
        {
            viewModelWindows.Remove(viewModel);
            w?.Close();
        }
    }

}
