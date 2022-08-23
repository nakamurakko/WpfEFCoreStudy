using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEFCoreStudy.DataTypes;
using WpfEFCoreStudy.Models;

namespace WpfEFCoreStudy.ViewModels;

/// <summary>
/// MainWindow用ViewModel。
/// </summary>
public class MainWindowViewModel
{
    public string Title { get; set; } = "WpfEFCoreStudy";

    public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>(BookModel.GetBooks());
}
