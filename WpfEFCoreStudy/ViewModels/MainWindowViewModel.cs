﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfEFCoreStudy.Constants;
using WpfEFCoreStudy.DB.Entities;
using WpfEFCoreStudy.Models;
using WpfEFCoreStudy.Services.Interfaces;
using WpfEFCoreStudy.ViewModels.Common;
using WpfEFCoreStudy.Views;

namespace WpfEFCoreStudy.ViewModels;

/// <summary>
/// MainWindow用ViewModel。
/// </summary>
public sealed partial class MainWindowViewModel : ObservableObject, IAsyncInitialization
{

    [ObservableProperty]
    private string _title = "WpfEFCoreStudy";

    [ObservableProperty]
    private ObservableCollection<Book> _books = new();

    /// <summary>
    /// 検索対象の本のタイトル。
    /// </summary>
    [ObservableProperty]
    private string _searchTitle = "";

    /// <summary>
    /// 検索対象の著者名。
    /// </summary>
    [ObservableProperty]
    private string _searchAuthorName = "";

    private readonly IDialogService _dialogService = App.Current.Services.GetService<IDialogService>();

    public Task Initialization { get; private set; }

    /// <summary>
    /// コンストラクター。
    /// </summary>
    public MainWindowViewModel()
    {
        this.Initialization = this.InitializeAsync();
    }

    /// <summary>
    /// 非同期で初期化する。
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task InitializeAsync()
    {
        IEnumerable<Book>? books = await BookModel.GetBooksAsync();
        this.Books = new ObservableCollection<Book>(books);
    }

    /// <summary>
    /// 本を検索する。
    /// </summary>
    [RelayCommand]
    private async Task SearchBooksAsync()
    {
        IEnumerable<Book> books = await BookModel.GetBooksAsync(this.SearchTitle, this.SearchAuthorName);
        this.Books = new ObservableCollection<Book>(books);
    }

    /// <summary>
    /// 検索結果をクリアする。
    /// </summary>
    [RelayCommand]
    private async Task ClearSearchResultAsync()
    {
        this.SearchTitle = "";
        this.SearchAuthorName = "";

        IEnumerable<Book> books = await BookModel.GetBooksAsync();
        this.Books = new ObservableCollection<Book>(books);
    }

    /// <summary>
    /// 本の詳細を表示する。
    /// </summary>
    /// <param name="book">本情報。</param>
    [RelayCommand]
    private void ShowBookDetail(Book book)
    {
        BookWindowViewModel viewModel = new(book, DisplayMode.ReadOnly);
        this._dialogService.ShowDialog<BookWindow, BookWindowViewModel>(viewModel);
    }

    /// <summary>
    /// 著者を追加する。
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task AddAuthorAsync()
    {
        this._dialogService.ShowDialog<AuthorWindow, AuthorWindowViewModel>();

        IEnumerable<Book> books = await BookModel.GetBooksAsync();
        this.Books = new ObservableCollection<Book>(books);
    }

    /// <summary>
    /// 本を追加する。
    /// </summary>
    [RelayCommand]
    private async Task AddBookAsync()
    {
        this._dialogService.ShowDialog<BookWindow, BookWindowViewModel>();

        IEnumerable<Book> books = await BookModel.GetBooksAsync();
        this.Books = new ObservableCollection<Book>(books);
    }

}
