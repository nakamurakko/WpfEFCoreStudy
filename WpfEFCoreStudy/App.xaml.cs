using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using WpfEFCoreStudy.DB;
using WpfEFCoreStudy.DB.Entities;
using WpfEFCoreStudy.Services;
using WpfEFCoreStudy.Services.Interfaces;

namespace WpfEFCoreStudy;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    public App()
    {
        this.Services = ConfigureServices();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        this.CreateDatabase();
    }

    /// <summary>
    /// Configures the services for the application.
    /// https://learn.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/ioc
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        ServiceCollection services = new ServiceCollection();

        services.AddSingleton<IDialogService, DialogService>();

        return services.BuildServiceProvider();
    }

    /// <summary>
    /// データベースを作成する。
    /// </summary>
    private void CreateDatabase()
    {
        // データベースファイルを作成する。
        using (BookDBContext dbContext = new BookDBContext())
        {
            // 新規作成だった場合、サンプルデータを登録する。
            if (dbContext.Database.EnsureCreated())
            {
                dbContext.Authors.Add(new Author() { AuthorName = "芥川龍之介" });
                dbContext.Authors.Add(new Author() { AuthorName = "川端康成" });
                dbContext.SaveChanges();

                dbContext.Books.Add(new Book() { Title = "蜘蛛の糸", AuthorId = dbContext.Authors.Where(author => author.AuthorName == "芥川龍之介").First().AuthorId });
                dbContext.Books.Add(new Book() { Title = "雪国", AuthorId = dbContext.Authors.Where(author => author.AuthorName == "川端康成").First().AuthorId });
                dbContext.SaveChanges();
            }
        }
    }

}
