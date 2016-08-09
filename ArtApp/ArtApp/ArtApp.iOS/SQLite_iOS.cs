using System;
using System.IO;
using ArtApp.iOS;
using ArtApp.Services.Interfaces;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace ArtApp.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS()
        {
     
        }

        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "ArtAppSQLite.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(new SQLitePlatformIOS(), path);
            // Return the database connection
            return conn;
        }
    }
}
