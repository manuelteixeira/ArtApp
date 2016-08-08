using System.IO;
using Windows.Storage;
using ArtApp.Services.Interfaces;
using SQLite;

namespace ArtApp.UWP
{
    public class SQLite_UWP : ISQLite
    {
        public SQLite_UWP()
        {
            
        }
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "ArtAppSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}
