using maui_schedule_slurper.Entities;
using SQLite;

namespace maui_schedule_slurper.Repositories;

public class ScheduleDatabase
{
    SQLiteAsyncConnection? Database;

    public ScheduleDatabase()
    {

    }

    async Task Init()
    {
        if (Database is not null)
        {
            return;
        }
        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var devroomsResult = await Database.CreateTableAsync<Devroom>();
    }

    public async Task<Devroom?> GetDevroomByID(int id)
    {
        await Init();
        return await Database!.Table<Devroom>().Where(dr => dr.DevroomID == id).FirstOrDefaultAsync();
    }

    public async Task<Devroom?> GetByCode(string code)
    {
        await Init();
        return await Database!.Table<Devroom>().Where(dr => dr.Code == code).FirstOrDefaultAsync();
    }

    public async Task<long> Save(Devroom devroom)
    {
        await Init();
        var existing = await Database!.Table<Devroom>().FirstOrDefaultAsync(dr => dr.DevroomID == devroom.DevroomID);
        if (existing != null)
        {
            return await Database.UpdateAsync(devroom);
        }
        else
        {
            return await Database.InsertAsync(devroom);
        }
    }

    public async Task<List<Devroom>> GetAllDevrooms()
    {
        await Init();
        return await Database!.Table<Devroom>().ToListAsync();
    }
}