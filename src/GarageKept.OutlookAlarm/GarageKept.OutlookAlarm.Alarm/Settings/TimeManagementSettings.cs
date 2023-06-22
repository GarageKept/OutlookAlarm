using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace GarageKept.OutlookAlarm.Alarm.Settings;

/// <summary>
///     Represents the time management settings for alarms.
/// </summary>
public class TimeManagementSettings : SettingsBase
{
    private bool _enableOnlyWorkingPeriods;
    private ObservableCollection<string> _exceptionCategories = new();

    private ObservableCollection<Holiday> _holidays = new()
    {
        new Holiday("New Year's Day", new DateTime(DateTime.Now.Year, 1, 1)),
        new Holiday("Martin Luther King Jr. Day", GetNthDayOfWeek(DateTime.Now.Year, 1, DayOfWeek.Monday, 3)),
        new Holiday("Presidents Day", GetNthDayOfWeek(DateTime.Now.Year, 2, DayOfWeek.Monday, 3)),
        new Holiday("Memorial Day", GetLastDayOfWeek(DateTime.Now.Year, 5, DayOfWeek.Monday)),
        new Holiday("Independence Day", new DateTime(DateTime.Now.Year, 7, 4)),
        new Holiday("Labor Day", GetNthDayOfWeek(DateTime.Now.Year, 9, DayOfWeek.Monday, 1)),
        new Holiday("Columbus Day", GetNthDayOfWeek(DateTime.Now.Year, 10, DayOfWeek.Monday, 2)),
        new Holiday("Veterans Day", new DateTime(DateTime.Now.Year, 11, 11)),
        new Holiday("Thanksgiving Day", GetNthDayOfWeek(DateTime.Now.Year, 11, DayOfWeek.Thursday, 4)),
        new Holiday("Christmas Day", new DateTime(DateTime.Now.Year, 12, 25))
    };

    private ObservableCollection<DayOfWeek> _workDays = new()
    {
        DayOfWeek.Monday,
        DayOfWeek.Tuesday,
        DayOfWeek.Wednesday,
        DayOfWeek.Thursday,
        DayOfWeek.Friday
    };

    private DateTime _workingEndTime = DateTime.Today.AddHours(17);
    private DateTime _workingStartTime = DateTime.Today.AddHours(8);

    /// <summary>
    ///     Initializes a new instance of the <see cref="TimeManagementSettings" /> class.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public TimeManagementSettings() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TimeManagementSettings" /> class with a save action.
    /// </summary>
    /// <param name="save">The action to save the settings.</param>
    public TimeManagementSettings(Action save) : base(save) { }

    /// <summary>
    ///     Gets or sets the start time of the working period.
    /// </summary>
    public DateTime WorkingStartTime
    {
        get => _workingStartTime;
        set
        {
            if (_workingStartTime.Equals(value))
                return;

            _workingStartTime = value;
            Save?.Invoke();
        }
    }

    /// <summary>
    ///     Gets or sets the end time of the working period.
    /// </summary>
    public DateTime WorkingEndTime
    {
        get => _workingEndTime;
        set
        {
            if (_workingEndTime.Equals(value))
                return;

            _workingEndTime = value;
            Save?.Invoke();
        }
    }

    /// <summary>
    ///     Gets or sets the days of the week considered as workdays.
    /// </summary>
    public ObservableCollection<DayOfWeek> WorkDays
    {
        get => _workDays;
        set
        {
            if (_workDays.SequenceEqual(value))
                return;

            if (_workDays is INotifyCollectionChanged oldCollection)
                oldCollection.CollectionChanged -= WorkDays_CollectionChanged;

            _workDays = value;

            if (_workDays is INotifyCollectionChanged newCollection)
                newCollection.CollectionChanged += WorkDays_CollectionChanged;

            Save?.Invoke();
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether non-working periods are enabled.
    /// </summary>
    public bool EnableOnlyWorkingPeriods
    {
        get => _enableOnlyWorkingPeriods;
        set
        {
            if (_enableOnlyWorkingPeriods == value)
                return;

            _enableOnlyWorkingPeriods = value;
            Save?.Invoke();
        }
    }

    /// <summary>
    ///     Gets or sets the list of categories that will still sound an alarm regardless of working time.
    /// </summary>
    public ObservableCollection<string> ExceptionCategories
    {
        get => _exceptionCategories;
        set
        {
            if (_exceptionCategories.SequenceEqual(value))
                return;

            if (_exceptionCategories is INotifyCollectionChanged oldCollection)
                oldCollection.CollectionChanged -= ExceptionCategories_Changed;

            _exceptionCategories = value;

            if (_exceptionCategories is INotifyCollectionChanged newCollection)
                newCollection.CollectionChanged += ExceptionCategories_Changed;

            Save?.Invoke();
        }
    }

    /// <summary>
    ///     Gets or sets the list of holidays.
    /// </summary>
    public ObservableCollection<Holiday> Holidays
    {
        get => _holidays;
        set
        {
            if (_holidays.SequenceEqual(value))
                return;

            if (_holidays is INotifyCollectionChanged oldCollection)
                oldCollection.CollectionChanged -= Holidays_Changed;

            _holidays = value;

            if (_holidays is INotifyCollectionChanged newCollection)
                newCollection.CollectionChanged += Holidays_Changed;

            Save?.Invoke();
        }
    }

    public bool BypassAudio() { return IsDuringWorkingHours(DateTime.Now) || IsHoliday(DateTime.Now); }

    private void ExceptionCategories_Changed(object? sender, NotifyCollectionChangedEventArgs e) { Save?.Invoke(); }

    /// <summary>
    ///     Helper method to get the last occurrence of a specific day of the week in a month.
    /// </summary>
    private static DateTime GetLastDayOfWeek(int year, int month, DayOfWeek dayOfWeek)
    {
        DateTime date = new(year, month, DateTime.DaysInMonth(year, month));
        while (date.DayOfWeek != dayOfWeek) date = date.AddDays(-1);
        return date;
    }

    /// <summary>
    ///     Helper method to calculate the nth occurrence of a specific day of the week in a month.
    /// </summary>
    private static DateTime GetNthDayOfWeek(int year, int month, DayOfWeek dayOfWeek, int occurrence)
    {
        DateTime date = new(year, month, 1);
        while (date.DayOfWeek != dayOfWeek) date = date.AddDays(1);
        date = date.AddDays((occurrence - 1) * 7);
        return date;
    }

    private void Holidays_Changed(object? sender, NotifyCollectionChangedEventArgs e) { Save?.Invoke(); }

    /// <summary>
    ///     Checks if the given time falls within the working period.
    /// </summary>
    /// <param name="time">The time to check.</param>
    /// <returns><c>true</c> if the time is within the working period; otherwise, <c>false</c>.</returns>
    public bool IsDuringWorkingHours(DateTime time)
    {
        if (!WorkDays.Contains(time.DayOfWeek)) return false;

        var currentTime = time.TimeOfDay;
        var workingStartTime = WorkingStartTime.TimeOfDay;
        var workingEndTime = WorkingEndTime.TimeOfDay;

        return currentTime >= workingStartTime && currentTime <= workingEndTime;
    }

    /// <summary>
    ///     Checks if the given date is a holiday.
    /// </summary>
    /// <param name="date">The date to check.</param>
    /// <returns><c>true</c> if the date is a holiday; otherwise, <c>false</c>.</returns>
    public bool IsHoliday(DateTime date) { return Holidays.Any(holiday => holiday.Date.Date == date.Date); }

    private void WorkDays_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) { Save?.Invoke(); }
}