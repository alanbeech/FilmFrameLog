namespace FilmFrameLog;

public static class Constants
{
    public static string FirestoreProjectID = "filmframelog";
    public const string DatabaseFilename = "FilmFrameLogSQLite.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public static string FirestoreKeyFilename { get; set; }
    
}