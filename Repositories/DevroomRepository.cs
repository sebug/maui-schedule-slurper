using maui_schedule_slurper.Entities;

namespace maui_schedule_slurper.Repositories;

public class DevroomRepository : IDevroomRepository
{
    private readonly ScheduleDatabase Database;
    public DevroomRepository(ScheduleDatabase database)
    {
        Database = database;
    }

    public Task<Devroom?> GetByCode(string code)
    {
        return Database.GetByCode(code);
    }

    public Task<Devroom?> GetByDevroomID(int devroomID)
    {
        return Database.GetDevroomByID(devroomID);
    }

    public Task<long> Save(Devroom devroom)
    {
        return Database.Save(devroom);
    }
}