namespace DSW_ProgramowanieObiektowe.Lista1;
public class Vehicle
{
    public string Name { get; }
    public double Position { get; private set; }
    public Vector VehicleVector { get; }

    public Vehicle(string name, double position, Vector vehicleVector)
    {
        Name = name;
        Position = position;
        VehicleVector = vehicleVector;
    }
    public override string ToString()
    {
        return $"Name: {Name}; Position: {Position} meters; Vector: {VehicleVector}";
    }

    public Vector GetTravelVector(Vector windVector) => Vector.Sum(VehicleVector, windVector);

    public static Time CalculateTravelTime(double travelDistance, Vector travelVector)
    {
        var totalSeconds = travelDistance / travelVector.Velocity.MetersPerSecond;

        return new Time(totalSeconds);
    }

    public static double CalculateTravelDistance(Time travelTime, Vector travelVector)
    {
        return travelVector.Velocity.MetersPerSecond * travelTime.TotalSeconds;
    }

    public bool IsAtPosition(double target) => Math.Abs(Position - target) < double.Epsilon;

    public void TravelByTime(Time travelTime, Vector windVector, Heading routeHeading)
    {
        var travelVector = GetTravelVector(windVector);

        var distance = CalculateTravelDistance(travelTime, travelVector);

        if (travelVector.Heading == routeHeading)
            Position += distance;
        else if (travelVector.Heading == Vector.GetOpposedHeading(routeHeading))
            Position -= distance;

        Position = Math.Round(Position,4);
    }
}
