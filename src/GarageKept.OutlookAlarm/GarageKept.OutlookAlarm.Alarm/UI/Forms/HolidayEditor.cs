using GarageKept.OutlookAlarm.Alarm.Interfaces;
using GarageKept.OutlookAlarm.Alarm.Settings;

namespace GarageKept.OutlookAlarm.Alarm.UI.Forms;

public partial class HolidayEditor : BaseForm, IHolidayEditor
{
    private Holiday _holiday = new();
    public HolidayEditor() { InitializeComponent(); }

    public Holiday Holiday
    {
        get => _holiday;
        set
        {
            _holiday = value;

            NameTextBox.Text = _holiday.Name;
            DatePicker.Value = _holiday.Date;
            DescriptionTextBox.Text = _holiday.Description;
        }
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        Holiday.Date = DatePicker.Value;
        Holiday.Name = NameTextBox.Text;
        Holiday.Description = DescriptionTextBox.Text;

        var result = ValidateHoliday();

        if (result)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            MessageBox.Show(@"Name cannot be empty", @"Validation Error", MessageBoxButtons.OK);
            NameTextBox.Focus();
        }
    }


    private bool ValidateHoliday() { return !string.IsNullOrEmpty(Holiday.Name); }
}