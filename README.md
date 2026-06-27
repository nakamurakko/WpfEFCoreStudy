# WpfEFCoreStudy

WPF と Entity Framework Core 勉強用プロジェクト。

## 追加 NuGet パッケージ

* [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)
* [LinqKit.Microsoft.EntityFrameworkCore](https://github.com/scottksmith95/LINQKit)
* [Microsoft.EntityFrameworkCore](https://learn.microsoft.com/ja-jp/ef/core/)
* [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/)
* [Microsoft.EntityFrameworkCore.Relational](https://learn.microsoft.com/ja-jp/ef/core/what-is-new/nuget-packages)
* [Microsoft.EntityFrameworkCore.SQLite](https://learn.microsoft.com/ja-jp/ef/core/providers/?tabs=dotnet-core-cli)
* [Microsoft.EntityFrameworkCore.Tools](https://learn.microsoft.com/ja-jp/ef/core/cli/powershell)

## DB 定義更新

DB の新規作成、更新に Entity Framework Core を使用する。

* [ツールのインストール](https://learn.microsoft.com/ja-jp/ef/core/cli/dotnet#installing-the-tools)
* [dotnet ef migrations add InitialCreate](https://learn.microsoft.com/ja-jp/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli#create-your-first-migration) --startup-project .\WpfEFCoreStudy (最初の移行を作成する)
* [dotnet ef migrations add \<AddTableNameColumnName\>](https://learn.microsoft.com/ja-jp/ef/core/cli/dotnet#dotnet-ef-migrations-add) --startup-project .\WpfEFCoreStudy (新しい移行を追加する)
* [dotnet ef database update](https://learn.microsoft.com/ja-jp/ef/core/cli/dotnet#dotnet-ef-database-update) --startup-project .\WpfEFCoreStudy (データベースを最後の移行または指定された移行に更新)
    * 実行時に [IMigrator.Migrate](https://learn.microsoft.com/ja-jp/dotnet/api/microsoft.entityframeworkcore.migrations.imigrator.migrate) を呼び出しているため、 `dotnet ef database update` は不要。