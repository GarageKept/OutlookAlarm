using GarageKept.OutlookAlarm.Forms.Interfaces;

namespace GarageKept.OutlookAlarm.Forms.UI.Forms;

public partial class SettingsForm : BaseForm , ISettingsForm
{
    //public ISettings AppSettings { get; set; }

    public SettingsForm(ISettings appSettings) : base(false)
    {
        InitializeComponent();

        Top = (ScreenHeight - Height) / 2;
        Left = (ScreenWidth - Width) / 2;

        //AppSettings = appSettings;

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SubscribeToMouseEvents(this);
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        //AppSettings.Save();
        Close();
    }
}