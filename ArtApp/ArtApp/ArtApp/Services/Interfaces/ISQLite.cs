using SQLite.Net;

namespace ArtApp.Services.Interfaces
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
