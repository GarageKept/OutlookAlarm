using NAudio.Wave;

namespace GarageKept.OutlookAlarm.Alarm.Audio;

public class UnmanagedMemoryStreamWaveStream : WaveStream
{
    private readonly long _length;
    private readonly UnmanagedMemoryStream _stream;

    private long _position;

    public UnmanagedMemoryStreamWaveStream(UnmanagedMemoryStream stream, long length)
    {
        _stream = stream;
        _length = length;
        Position = 0;
    }

    public UnmanagedMemoryStreamWaveStream(UnmanagedMemoryStream stream)
    {
        _stream = stream;
        _length = stream.Length;
        Position = 0;
    }

    public override WaveFormat WaveFormat => new(44100, 16, 2);

    public override long Length => _length;

    public sealed override long Position
    {
        get => _position;
        set
        {
            if (value == 0)
                _stream.Seek(0, SeekOrigin.Begin);

            _position = value;
        }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        var remainingBytes = _length - Position;
        var bytesToRead = (int)Math.Min(count, remainingBytes);
        var bytesRead = _stream.Read(buffer, offset, bytesToRead);

        Position += bytesRead;

        return bytesRead;
    }
}