using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class AlarmForm : Form, IAlarmForm
{
    public AlarmForm(IAlarm alarm)
    {
        InitializeComponent();
    }
}