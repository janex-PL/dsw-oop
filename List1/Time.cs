namespace DSW_ProgramowanieObiektowe.Lista1;
public class Time
{
    public double TotalSeconds { get; private set; }

    private const int MinutesInHour = 60;
    private const int SecondsInMinute = 60;

    public Time()
    {

    }
    public Time(double totalSeconds)
    {
        TotalSeconds = totalSeconds;
    }

    public Time(int hours, int minutes, double seconds)
    {
        TotalSeconds = seconds + hours * MinutesInHour * SecondsInMinute + minutes * SecondsInMinute;
    }

    public int Hours => (int)Math.Floor(TotalSeconds / (MinutesInHour * SecondsInMinute));
    public int Minutes => (int)Math.Floor(TotalSeconds % (MinutesInHour * SecondsInMinute) / SecondsInMinute);
    public int Seconds => (int)Math.Floor(TotalSeconds % SecondsInMinute);

    public void Add(Time time)
    {
        TotalSeconds += time.TotalSeconds;
    }
    public void Add(double totalSeconds)
    {
        TotalSeconds += totalSeconds;
    }

    public bool Equals(Time other) => Compare(this, other) == 0;

    public Time GetDifference(Time other)
    {
        return new Time(Math.Abs(TotalSeconds - other.TotalSeconds));
    }

    public override bool Equals(object? obj)
    {
        if(obj is Time time)
            return Equals(time);
        return false;
    }

    public override string ToString()
    {
        return
            $"{Hours.ToString().PadLeft(2, '0')}:{Minutes.ToString().PadLeft(2, '0')}:{Seconds.ToString().PadLeft(2, '0')}";
    }

    public static Time None => new(0);

    public static int Compare(Time first, Time second) =>
        (first, second) switch
        {
            _ when first.TotalSeconds > second.TotalSeconds => 1,
            _ when first.TotalSeconds < second.TotalSeconds => -1,
            _ when Math.Abs(first.TotalSeconds - second.TotalSeconds) < double.Epsilon => 0,
            _ => throw new ArgumentOutOfRangeException()
        };

    
}
