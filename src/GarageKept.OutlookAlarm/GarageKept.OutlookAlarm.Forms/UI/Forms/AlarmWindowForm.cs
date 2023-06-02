using GarageKept.OutlookAlarm.Forms.Audio;
using GarageKept.OutlookAlarm.Forms.Common;
using Timer = System.Windows.Forms.Timer;

namespace GarageKept.OutlookAlarm.Forms.UI.Forms;

public partial class AlarmWindowForm : BaseForm
{
    private readonly Media _mediaPlayer = new();
    private readonly Timer _refreshTimer = new() { Interval = 1000 };
    private AlarmAction _alarmAction = AlarmAction.Dismiss;

    public AlarmWindowForm(Alarm alarm, Action<AlarmAction> alarmFormClosed) : base(false)
    {
        if(alarm.PlaySound)
            _mediaPlayer.PlaySound(SoundType.Warning0);

        MyCallBack = alarmFormClosed;

        InitializeComponent();

        MyAlarm = alarm;

        SubjectLabel.Text = alarm.Subject;
        TimeRight.Text = alarm.Time.ToString("hh:mm");
        TimeLeft.Text = string.Format(Program.ApplicationSettings.TimeLeftStringFormat, DateTime.Now - alarm.Time);

        ActionSelector.SelectedIndex = 0;

        _refreshTimer.Tick += FormRefresh;
        _refreshTimer.Start();

        // Subscribe to MouseEnter and MouseLeave events for each child control
        SubscribeToMouseEvents(this);
    }

    public Action<AlarmAction> MyCallBack { get; set; }
    public Alarm MyAlarm { get; set; }

    private void FormRefresh(object? sender, EventArgs e)
    {
        TimeLeft.Text = string.Format(Program.ApplicationSettings.TimeLeftStringFormat, DateTime.Now - MyAlarm.Time);
    }

    private void AlarmWindowForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        MyCallBack(_alarmAction);
    }

    private void SnoozeButton_Click(object sender, EventArgs e)
    {
        // 5 minutes before start
        // 0 hours before start
        // 5 minutes
        // 10 minutes

        _alarmAction = ActionSelector.SelectedIndex switch
        {
            0 => AlarmAction.FiveMinBefore,
            1 => AlarmAction.ZeroMinBefore,
            2 => AlarmAction.SnoozeFiveMin,
            3 => AlarmAction.SnoozeTenMin,
            _ => _alarmAction
        };

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