using System.Runtime.InteropServices;

namespace GarageKept.OutlookAlarm.Forms.Audio;

internal static class AudioEngine
{
    // Get the system volume
    public static bool? IsSystemVolumeMuted()
    {
        try
        {
            var deviceEnumeratorType = Type.GetTypeFromCLSID(Clsid.MMDeviceEnumerator);

            if (deviceEnumeratorType == null) return null;

            if (Activator.CreateInstance(deviceEnumeratorType) is not IMMDeviceEnumerator enumerator) return null;

            var hr = enumerator.GetDefaultAudioEndpoint(0, 1, out var device); // eRender (0) and eMultimedia (1)

            if (hr != 0) return null;

            var iidAudioEndpointVolume = Iid.IAudioEndpointVolume;

            hr = device.Activate(ref iidAudioEndpointVolume, unchecked((int)CLSCTX.CLSCTX_ALL), nint.Zero,
                out var endpointVolumeObj);
            if (hr != 0) return null;

            if (endpointVolumeObj is not IAudioEndpointVolume endpointVolume) return null;

            hr = endpointVolume.GetMute(out var muteState);

            if (hr != 0) return null;

            return muteState;
        }
        catch
        {
            return null;
        }
    }

    // Mute or un mute the system volume
    public static void SetSystemVolumeMuted(bool mute)
    {
        var type = Type.GetTypeFromCLSID(Clsid.MMDeviceEnumerator);

        if (type == null) return;

        var enumerator = Activator.CreateInstance(type) as IMMDeviceEnumerator;

        IMMDevice? device = null;

        enumerator?.GetDefaultAudioEndpoint(0, 1, out device); // eRender (0) and eMultimedia (1)

        if (device == null) return;

        var iidAudioEndpointVolume = Iid.IAudioEndpointVolume;


        device.Activate(ref iidAudioEndpointVolume, unchecked((int)CLSCTX.CLSCTX_ALL), nint.Zero,
            out var endpointVolumeObj);

        var endpointVolume = (IAudioEndpointVolume)endpointVolumeObj;

        var empty = Guid.Empty;

        endpointVolume.SetMute(mute, ref empty);
    }

    public static void ToggleSystemVolumeMute()
    {
        var hWnd = nint.Zero; // Use the HWND_BROADCAST to send the message to all top-level windows
        var wParam = nint.Zero;
        nint lParam = NativeMethods.APPCOMMAND_VOLUME_MUTE;
        NativeMethods.SendMessageW(hWnd, NativeMethods.WM_APPCOMMAND, wParam, lParam);
    }

    internal static void UnMuteSystemVolume()
    {
        if (IsSystemVolumeMuted() == null) return;

        var isMuted = IsSystemVolumeMuted() ?? false;

        if (isMuted) SetSystemVolumeMuted(false);
    }

    // Get the system volume
    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioEndpointVolume
    {
        int RegisterControlChangeNotify(nint pNotify);
        int UnregisterControlChangeNotify(nint pNotify);
        int GetChannelCount(out int pnChannelCount);
        int SetMasterVolumeLevel(float fLevelDB, ref Guid pguidEventContext);
        int SetMasterVolumeLevelScalar(float fLevel, ref Guid pguidEventContext);
        int GetMasterVolumeLevel(out float pfLevelDB);
        int GetMasterVolumeLevelScalar(out float pfLevel);
        int SetChannelVolumeLevel(uint nChannel, float fLevelDB, ref Guid pguidEventContext);
        int SetChannelVolumeLevelScalar(uint nChannel, float fLevel, ref Guid pguidEventContext);
        int GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);
        int GetChannelVolumeLevelScalar(uint nChannel, out float pfLevel);
        int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, ref Guid pguidEventContext);
        int GetMute(out bool pbMute);
        int GetVolumeStepInfo(out uint pnStep, out uint pnStepCount);
        int VolumeStepUp(ref Guid pguidEventContext);
        int VolumeStepDown(ref Guid pguidEventContext);
        int QueryHardwareSupport(out uint pdwHardwareSupportMask);
        int GetVolumeRange(out float pflVolumeMindB, out float pflVolumeMaxdB, out float pflVolumeIncrementdB);
    }

    [Flags]
    private enum CLSCTX : uint
    {
        CLSCTX_INPROC_SERVER = 0x1,
        CLSCTX_INPROC_HANDLER = 0x2,
        CLSCTX_LOCAL_SERVER = 0x4,
        CLSCTX_INPROC_SERVER16 = 0x8,
        CLSCTX_REMOTE_SERVER = 0x10,
        CLSCTX_INPROC_HANDLER16 = 0x20,
        CLSCTX_RESERVED1 = 0x40,
        CLSCTX_RESERVED2 = 0x80,
        CLSCTX_RESERVED3 = 0x100,
        CLSCTX_RESERVED4 = 0x200,
        CLSCTX_NO_CODE_DOWNLOAD = 0x400,
        CLSCTX_RESERVED5 = 0x800,
        CLSCTX_NO_CUSTOM_MARSHAL = 0x1000,
        CLSCTX_ENABLE_CODE_DOWNLOAD = 0x2000,
        CLSCTX_NO_FAILURE_LOG = 0x4000,
        CLSCTX_DISABLE_AAA = 0x8000,
        CLSCTX_ENABLE_AAA = 0x10000,
        CLSCTX_FROM_DEFAULT_CONTEXT = 0x20000,
        CLSCTX_ACTIVATE_32_BIT_SERVER = 0x40000,
        CLSCTX_ACTIVATE_64_BIT_SERVER = 0x80000,
        CLSCTX_ENABLE_CLOAKING = 0x100000,
        CLSCTX_PS_DLL = 0x80000000,

        CLSCTX_ALL = CLSCTX_INPROC_SERVER | CLSCTX_INPROC_HANDLER | CLSCTX_LOCAL_SERVER | CLSCTX_REMOTE_SERVER |
                     CLSCTX_NO_CODE_DOWNLOAD | CLSCTX_NO_CUSTOM_MARSHAL | CLSCTX_ENABLE_CODE_DOWNLOAD |
                     CLSCTX_NO_FAILURE_LOG | CLSCTX_DISABLE_AAA | CLSCTX_ENABLE_AAA | CLSCTX_FROM_DEFAULT_CONTEXT |
                     CLSCTX_ACTIVATE_32_BIT_SERVER | CLSCTX_ACTIVATE_64_BIT_SERVER | CLSCTX_ENABLE_CLOAKING |
                     CLSCTX_PS_DLL
    }

    internal static class Clsid
    {
        public static readonly Guid MMDeviceEnumerator = new("BCDE0395-E52F-467C-8E3D-C4579291692E");
    }

    internal static class Iid
    {
        public static readonly Guid IMMDeviceEnumerator = new("A95664D2-9614-4F35-A746-DE8DB63617E6");
        public static readonly Guid IAudioEndpointVolume = new("5CDF2C82-841E-4546-9722-0CF74078229A");
    }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceEnumerator
    {
        int NotImpl1();

        [PreserveSig]
        int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice ppDevice);
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDevice
    {
        [PreserveSig]
        int Activate(ref Guid iid, int dwClsCtx, nint pActivationParams,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
    }

    internal static class NativeMethods
    {
        public const int WM_APPCOMMAND = 0x0319;
        public const int APPCOMMAND_VOLUME_MUTE = 0x80000;

        [DllImport("user32.dll")]
        public static extern nint SendMessageW(nint hWnd, int Msg, nint wParam, nint lParam);
    }
}