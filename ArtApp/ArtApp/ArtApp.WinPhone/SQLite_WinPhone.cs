using System.IO;
using Windows.Storage;
using ArtApp.Services.Interfaces;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;

namespace ArtApp.WinPhone
{
    public class SQLite_WinPhone : ISQLite
    {
        public SQLite_WinPhone()
        {
            
        }
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "ArtAppSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(new SQLitePlatformWinRT(), path);
            // Return the database connection
            return conn;
        }
    }
}
