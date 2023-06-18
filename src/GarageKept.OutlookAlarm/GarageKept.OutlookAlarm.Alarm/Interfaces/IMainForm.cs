namespace GarageKept.OutlookAlarm.Alarm.Interfaces;

public interface IMainForm
{
    void AddMouseEvents(Control control);
    void CheckMouseLeaveForm();
    int Left { get; set; }

    void MainWindow_MouseEnter(object? sender, EventArgs e);
    void MainWindow_MouseLeave(object? sender, EventArgs e);
}