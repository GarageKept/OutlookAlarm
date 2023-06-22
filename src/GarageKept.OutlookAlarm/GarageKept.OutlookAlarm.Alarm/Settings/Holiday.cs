namespace GarageKept.OutlookAlarm.Alarm.Settings;

public class Holiday
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } // The name of the holiday
    public DateTime Date { get; set; } // The date of the holiday
    public string Description { get; set; } // Additional description or details about the holiday

    // ReSharper disable once MemberCanBePrivate.Global
    public Holiday()
    {
        Name = string.Empty;
        Date = DateTime.MinValue;
        Description = string.Empty;
    }

    // Constructor to initialize the holiday with its name and date
    public Holiday(string name, DateTime date) : this()
    {
        Name = name;
        Date = date;
    }

    public Holiday(string name, DateTime date, string description) : this(name, date)
    {
        Description = description;
    }
}