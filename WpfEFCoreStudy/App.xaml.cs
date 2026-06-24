using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WpfEFCoreStudy.DB;
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
        // PooledDbContextFactory を使用する場合。
        //services.AddPooledDbContextFactory<BookDBContext>(optionsAction => optionsAction.UseSqlite("Data Source=database.sqlite"));

        return services.BuildServiceProvider();
    }

    /// <summary>
    /// データベースを作成する。
    /// </summary>
    private void CreateDatabase()
    {
        using BookDBContext dbContext = new();

        // DB のマイグレーション
        // https://learn.microsoft.com/ja-jp/dotnet/api/microsoft.entityframeworkcore.migrations.imigrator.migrate?view=efcore-9.0#microsoft-entityframeworkcore-migrations-imigrator-migrate(system-string)
        dbContext.Database.Migrate();
    }

}
