using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WpfEFCoreStudy.DB;
using WpfEFCoreStudy.DB.Entities;
using WpfEFCoreStudy.Services;
using WpfEFCoreStudy.Services.Interfaces;

namespace WpfEFCoreStudy;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application
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
        ServiceCollection services = new();

        services.AddSingleton<IDialogService, DialogService>();

        return services.BuildServiceProvider();
    }

    /// <summary>
    /// データベースを作成する。
    /// </summary>
    private void CreateDatabase()
    {
        PooledDbContextFactory<BookDBContext>? dbContextFactory = new(new DbContextOptionsBuilder<BookDBContext>().UseSqlite(BookDBContext.ConnectionString).Options);

        // データベースファイルを作成する。
        using (BookDBContext dbContext = dbContextFactory.CreateDbContext())
        {
            // 新規作成だった場合、サンプルデータを登録する。
            if (dbContext.Database.EnsureCreated())
            {
                using (dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Author akutagawa = new() { AuthorName = "芥川龍之介" };
                        Author kawabata = new() { AuthorName = "川端康成" };
                        dbContext.Authors.Add(akutagawa);
                        dbContext.Authors.Add(kawabata);
                        dbContext.SaveChanges();

                        dbContext.Books.Add(new Book() { Title = "蜘蛛の糸", AuthorId = akutagawa.AuthorId });
                        dbContext.Books.Add(new Book() { Title = "雪国", AuthorId = kawabata.AuthorId });
                        dbContext.SaveChanges();

                        dbContext.Database.CommitTransaction();
                    }
                    catch (Exception)
                    {
                        dbContext.Database.RollbackTransaction();

                        throw;
                    }
                }
            }
        }
    }

}
