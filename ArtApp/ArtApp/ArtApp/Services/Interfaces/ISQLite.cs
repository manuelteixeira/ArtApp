using SQLite;

namespace ArtApp.Services.Interfaces
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
