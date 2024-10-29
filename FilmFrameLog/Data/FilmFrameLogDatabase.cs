using FilmFrameLog.Models;
using SQLite;

namespace FilmFrameLog.Data;

public class FilmFrameLogDatabase
{
    SQLiteAsyncConnection Database;

    public FilmFrameLogDatabase()
    {
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Camera>();
        var result2 = await Database.CreateTableAsync<Film>();
    }
    
    public async Task<List<Camera>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<Camera>().ToListAsync();
    }

    public async Task<List<Camera>> GetItemsNotDoneAsync()
    {
        await Init();
        return await Database.Table<Camera>().ToListAsync();

        // SQL queries are also possible
        //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
    }

    public async Task<Camera> GetItemAsync(int id)
    {
        await Init();
        return await Database.Table<Camera>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(Camera item)
    {
        await Init();
        if (item.Id != 0)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }
    
    public async Task<int> SaveFilmAsync(Film item)
    {
        await Init();
        if (item.Id != 0)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(Camera item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }

    public async Task<IEnumerable<Film>> GetFilmsAsync()
    {
        await Init();
        return await Database.Table<Film>().ToListAsync();
    }
}