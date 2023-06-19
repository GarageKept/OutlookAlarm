using System.Text.Json;
using System.Text.Json.Serialization;
using GarageKept.OutlookAlarm.Alarm.Audio;
using GarageKept.OutlookAlarm.Alarm.Interfaces;

namespace GarageKept.OutlookAlarm.Alarm.Settings
{
    /// <summary>
    /// Represents the application settings.
    /// Provides methods to save and load settings from a JSON file.
    /// </summary>
    public class OutlookAlarmSettings : ISettings
    {
        private const string SettingsFilePath = "settings.json";

        public OutlookAlarmSettings()
        {
            LoadOrCreate();
        }

        [JsonConstructor]
        public OutlookAlarmSettings(bool loaded)
        {
            // This constructor is used by the JSON deserializer.
            // It does not perform any specific actions.
        }

        public AlarmSettings Alarm { get; set; } = new ();
        public AlarmSourceSettings AlarmSource { get; set; } = new ();
        public AudioSettings Audio { get; set; } = new ();
        public ColorSettings Color { get; set; } = new();
        public MainSettings Main { get; set; } = new ();

        /// <summary>
        /// Saves the settings to a JSON file.
        /// </summary>
        public void Save()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new ColorJsonConverter() }
            };

            var settingsJson = JsonSerializer.Serialize(this, options);
            File.WriteAllText(SettingsFilePath, settingsJson);
        }

        /// <summary>
        /// Loads the settings from a JSON file or creates a new file if it does not exist.
        /// </summary>
        public void LoadOrCreate()
        {
            if (!File.Exists(SettingsFilePath))
            {
                Save();
            }
            else
            {
                var settingsJson = File.ReadAllText(SettingsFilePath);

                if (string.IsNullOrEmpty(settingsJson))
                    return;

                var options = GetJsonSerializeOptions();

                var savedSettings = JsonSerializer.Deserialize<OutlookAlarmSettings>(settingsJson, options);

                if (savedSettings == null)
                    return;

                foreach (var property in typeof(OutlookAlarmSettings).GetProperties())
                {
                    if (!property.CanRead || !property.CanWrite)
                        continue;

                    var savedValue = property.GetValue(savedSettings);
                    property.SetValue(this, savedValue);
                }
            }
        }

        private static JsonSerializerOptions GetJsonSerializeOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new ColorJsonConverter(), new SettingsJsonConverter() }
            };
        }

        internal static OutlookAlarmSettings FromJsonElement(JsonElement element, bool loaded)
        {
            var settings = new OutlookAlarmSettings(loaded);

            var options = GetJsonSerializeOptions();

            foreach (var property in typeof(OutlookAlarmSettings).GetProperties())
            {
                if (!property.CanRead || !property.CanWrite)
                    continue;

                if (!element.TryGetProperty(property.Name, out var propertyValue))
                    continue;

                var value = JsonSerializer.Deserialize(propertyValue.GetRawText(), property.PropertyType, options);
                property.SetValue(settings, value);
            }

            return settings;
        }
    }

    public class AudioSettings
    {
        public AudioSettings()
        {
            
        }

        private SoundType _defaultSound = SoundType.TickTock;
        private int _turnOffAlarmAfterStart = 15;

        public SoundType DefaultSound
        {
            get => _defaultSound;
            set
            {
                // ReSharper disable once RedundantCheckBeforeAssignment
                if (_defaultSound != value)
                    _defaultSound = value;
            }
        }

        public int TurnOffAlarmAfterStart
        {
            get => _turnOffAlarmAfterStart;
            set => _turnOffAlarmAfterStart = value;
        }
    }

    public class AlarmSettings
    {
        private int _left = -1;

        public int AlarmWarningTime { get; set; } = 15;
        public string TimeLeftStringFormat { get; set; } = "{0:%h}h {0:mm}m {0:ss}s";
        public string AlarmStartStringFormat { get; set; } = "hh:mm tt";
        public int Top { get; set; }

        public int Left
        {
            get
            {
                if (_left < 0)
                    return Screen.PrimaryScreen?.Bounds.Width / 2 ?? 0;

                return _left;
            }
            set => _left = value;
        }
    }

    public class AlarmSourceSettings
    {
        public int FetchIntervalInMinutes { get; set; } = 2;
        public int FetchTimeInHours { get; set; } = 1;
    }

    public class ColorSettings
    {
        public Color AlarmPastStartColor { get; set; } = Color.Red;
        public Color GreenColor { get; set; } = Color.Green;
        public Color RedColor { get; set; } = Color.Red;
        public Color YellowColor { get; set; } = Color.Yellow;
    }

    public class MainSettings
    {
        public int Left { get; set; }
        public int BarSize { get; set; } = 10;
        public int SliderSpeed { get; set; } = 5;
        public int MinimumWidth { get; set; } = 256;
    }
}
