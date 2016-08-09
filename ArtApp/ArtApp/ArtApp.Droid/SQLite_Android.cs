using System.IO;
using ArtApp.Droid;
using ArtApp.Services.Interfaces;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace ArtApp.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {
            
        }
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "ArtAppSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), path);
            // Return the database connection
            return conn;
        }
    }
}