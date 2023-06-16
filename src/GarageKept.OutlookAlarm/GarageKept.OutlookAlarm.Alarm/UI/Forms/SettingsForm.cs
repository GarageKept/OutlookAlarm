using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class SettingsForm : BaseForm, ISettingsForm
{
    public SettingsForm(ISettings appSettings) : base(false)
    {
        InitializeComponent();

        Top = (ScreenHeight - Height) / 2;
        Left = (ScreenWidth - Width) / 2;

        AppSettings = appSettings;

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SetDraggable(this);
    }

    public ISettings AppSettings { get; set; }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        AppSettings.Save();
        Close();
    }
}