namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IMainForm : IDisposable
{
    int Left { get; set; }
    void AddMouseEvents(Control control);
    void CheckMouseLeaveForm();

    void MainWindow_MouseEnter(object? sender, EventArgs e);
    void MainWindow_MouseLeave(object? sender, EventArgs e);
}