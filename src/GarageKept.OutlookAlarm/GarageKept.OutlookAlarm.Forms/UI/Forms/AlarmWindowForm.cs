using System.DirectoryServices.ActiveDirectory;
using GarageKept.OutlookAlarm.Forms.Audio;
using GarageKept.OutlookAlarm.Forms.Common;
using GarageKept.OutlookAlarm.Forms.Outlook;
using Microsoft.Office.Interop.Outlook;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.UI.Forms;

public partial class AlarmWindowForm : BaseForm
{
    private readonly Media _mediaPlayer = new();
    private readonly Timer _refreshTimer = new() { Interval = 1000 };
    private AlarmAction _alarmAction = AlarmAction.Dismiss;

    public AlarmWindowForm(Common.Alarm alarm, Action<AlarmAction> alarmFormClosed) : base(false)
    {
        InitializeComponent();
        
        ShowInTaskbar = false;

        ActionSelector.Items.Clear();
        ActionSelector.DataSource = Enum.GetValues(typeof(AlarmAction))
            .Cast<AlarmAction>()
            .Select(s => new
            {
                Value = s,
                Text = AlarmActionHelpers.GetEnumDisplayValue(s)
            }).Where(a=>a.Text !="Dismissed")
            .ToList();
        ActionSelector.DisplayMember = "Text";
        ActionSelector.ValueMember = "Value";
        
        if (alarm.PlaySound && !DesignMode)
        {
            if(string.IsNullOrEmpty(alarm.Appointment?.CustomSound))
                _mediaPlayer.PlaySound(SoundType.Warning0);
            else
                _mediaPlayer.PlaySound(alarm.Appointment.CustomSound);
        }

        MyCallBack = alarmFormClosed;
        
        MyAlarm = alarm;

        SubjectLabel.Text = alarm.Appointment?.Subject;
        TimeRight.Text = alarm.AlarmTime.ToString("hh:mm");
       // TimeLeft.Text = string.Format(Program.ApplicationSettings.TimeLeftStringFormat, DateTime.Now - MyAlarm.Appointment?.Start);

        UpdateDropdown();

        _refreshTimer.Tick += FormRefresh;
        _refreshTimer.Start();

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SubscribeToMouseEvents(this);
    }

    private void UpdateDropdown()
    {
        if (DateTime.Now - MyAlarm.AlarmTime > TimeSpan.FromMinutes(5))
        {
            ActionSelector.Items.Remove(AlarmAction.FiveMinBefore);
        }

        if (DateTime.Now - MyAlarm.AlarmTime > TimeSpan.FromMinutes(0))
        {
            ActionSelector.Items.Remove(AlarmAction.ZeroMinBefore);
        }

        if(ActionSelector.SelectedIndex <= 0) {ActionSelector.SelectedIndex = 0;}
    }

    public Action<AlarmAction> MyCallBack { get; set; }

    public Common.Alarm MyAlarm { get; set; }

    private void FormRefresh(object? sender, EventArgs e)
    {
//        TimeLeft.Text = string.Format(Program.ApplicationSettings.TimeLeftStringFormat, DateTime.Now - MyAlarm.Appointment?.Start);

        UpdateDropdown();
    }

    private void AlarmWindowForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        MyCallBack(_alarmAction);
    }

    private void SnoozeButton_Click(object sender, EventArgs e)
    {
        _alarmAction = (AlarmAction)(ActionSelector.SelectedValue ?? AlarmAction.FiveMinBefore);

        Close();
    }

    private void DismissButton_Click(object sender, EventArgs e)
    {
        _alarmAction = AlarmAction.Dismiss;

        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _mediaPlayer.Stop();

        base.OnFormClosing(e);
    }
}