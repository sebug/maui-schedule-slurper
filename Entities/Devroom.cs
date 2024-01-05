using SQLite;

namespace maui_schedule_slurper.Entities;

public class Devroom
{
    [PrimaryKey]
    [AutoIncrement]
    public int DevroomID { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? DetailsURL { get; set; }
}