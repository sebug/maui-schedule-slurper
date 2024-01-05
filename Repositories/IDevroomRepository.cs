using maui_schedule_slurper.Entities;

namespace maui_schedule_slurper.Repositories;

public interface IDevroomRepository
{
    Task<Devroom?> GetByDevroomID(int devroomID);
    Task<Devroom?> GetByCode(string code);
    Task<long> Save(Devroom devroom);
    Task<List<Devroom>> GetAll();
}