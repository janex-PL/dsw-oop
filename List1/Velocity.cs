namespace List1;
public class Velocity
{
    public double MetersPerSecond { get; private set; }

    private const double KnotsToMetersPerSecondRate = 0.51;

    public Velocity()
    {
    }

    public Velocity(double metersPerSecond)
    {
        MetersPerSecond = metersPerSecond;
    }

    public static Velocity FromTravelTimeAndDistance(double distance, Time time) => new(Math.Round(distance / time.TotalSeconds,4));
    public static Velocity FromMetersPerSecond(double value) => new(value);
    public static Velocity FromKnots(double value) => new(ConvertKnotsToMetersPerSecond(value));
    public static Velocity None => new(0);

    public override string ToString()
    {
        return $"{MetersPerSecond} m/s";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Velocity other)
            return Compare(this, other) == 0;
        return false;
    }

    public void Add(Velocity other)
    {
        MetersPerSecond += other.MetersPerSecond;
    }

    public static double ConvertMetersPerSecondToKnots(double value) => value / KnotsToMetersPerSecondRate;
    public static double ConvertKnotsToMetersPerSecond(double value) => value * KnotsToMetersPerSecondRate;
    public static Velocity Sum(Velocity first, Velocity second) => new(first.MetersPerSecond + second.MetersPerSecond);
    public static Velocity Difference(Velocity first, Velocity second) => new(Math.Abs(first.MetersPerSecond - second.MetersPerSecond));
    
    public static int Compare(Velocity first, Velocity second) =>
        (first, second) switch
        {
            _ when first.MetersPerSecond > second.MetersPerSecond => 1,
            _ when first.MetersPerSecond < second.MetersPerSecond => -1,
            _ when Math.Abs(first.MetersPerSecond - second.MetersPerSecond) < double.Epsilon => 0,
            _ => throw new ArgumentOutOfRangeException()
        };

}
