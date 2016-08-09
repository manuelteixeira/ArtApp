using System.IO;
using Windows.Storage;
using ArtApp.Services.Interfaces;
using ArtApp.Windows;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_WinRT))]
namespace ArtApp.Windows
{
    public class SQLite_WinRT : ISQLite
    {
        public SQLite_WinRT()
        {
            
        }
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "ArtAppSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path);
            return conn;
        }
    }
}
