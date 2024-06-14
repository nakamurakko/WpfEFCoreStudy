using System.Linq;
using System.Windows;
using WpfEFCoreStudy.DataTypes;
using WpfEFCoreStudy.DB;

namespace WpfEFCoreStudy;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        this.CreateDatabase();
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
