namespace GarageKept.OutlookAlarm.Forms.UI.Forms;

public partial class SettingsWindow : BaseForm
{
    public SettingsWindow() : base(false)
    {
        InitializeComponent();

        Top = (ScreenHeight - Height) / 2;
        Left = (ScreenWidth - Width) / 2;

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SubscribeToMouseEvents(this);
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        Program.ApplicationSettings?.Save();
        Close();
    }
}