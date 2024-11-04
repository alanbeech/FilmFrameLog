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
        var result = await Database.CreateTableAsync<MyCamera>();
        var result2 = await Database.CreateTableAsync<MyFilm>();
    }
    
    public async Task<List<MyCamera>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<MyCamera>().ToListAsync();
    }

    public async Task<List<MyCamera>> GetItemsNotDoneAsync()
    {
        await Init();
        return await Database.Table<MyCamera>().ToListAsync();

        // SQL queries are also possible
        //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
    }

    public async Task<MyCamera> GetItemAsync(int id)
    {
        await Init();
        return await Database.Table<MyCamera>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(MyCamera item)
    {
        await Init();
        if (item.Id != 0)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }
    
    public async Task<int> SaveFilmAsync(MyFilm item)
    {
        await Init();
        if (item.Id != 0)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(MyCamera item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }

    public async Task<IEnumerable<MyFilm>> GetFilmsAsync()
    {
        await Init();
        return await Database.Table<MyFilm>().ToListAsync();
    }
}