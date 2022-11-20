namespace DSW_ProgramowanieObiektowe.Lista1;

public class Simulation
{
    private Route Route { get; } = new();
    private List<Vehicle> Vehicles { get; } = new();
    private Vector WindVector { get; set; } = new();
    public Time CurrentTime { get; } = new();

    public Simulation()
    {
    }

    public Simulation(Route route, List<Vehicle> vehicles, Vector windVector, Time currentTime)
    {
        Route = route;
        Vehicles = vehicles;
        WindVector = windVector;
        CurrentTime = currentTime;
    }

    public void DisplaySimulationInfo()
    {
        LogToConsole("Simulation information");
        LogToConsole("Route detail:");
        LogToConsole(Route.ToString());
        LogToConsole("Vehicle details:");
        foreach (var vehicle in Vehicles)
        {
            LogToConsole('\t' + vehicle.ToString());
        }
        LogToConsole($"Wind vector: {WindVector}");
        LogToConsole($"Current time: {CurrentTime}");

    }

    public void ChangeWindVector(Vector wind)
    {
        LogToConsole($"Changing wind vector to {wind}");
        WindVector = wind;
    }

    public void AddVehicle(Vehicle vehicle)
    {
        LogToConsole($"Adding vehicle {vehicle}");
        Vehicles.Add(vehicle);
    }

    public void RunSimulationByTime(Time travelTime)
    {
        if (Route.Heading == Heading.None)
        {
            LogToConsole("Travel cannot be performed, because the route does not have heading specified");
            return;
        }

        LogToConsole($"Performing travel with duration of {travelTime}");

        foreach (var vehicle in Vehicles)
        {
            LogToConsole($"Traveling with duration {travelTime}, current position: {vehicle.Position} meters",
                vehicle.Name);
            vehicle.TravelByTime(travelTime, WindVector, Route.Heading);
            LogToConsole($"Travel completed, current position: {vehicle.Position} meters", vehicle.Name);
        }

        CurrentTime.Add(travelTime);
        LogToConsole($"Travel completed! Current time is: {CurrentTime}");
    }

    public void TravelToDestination(string vehicleName, string destinationName)
    {
        var vehicle = GetVehicleByName(vehicleName);
        if (vehicle == null)
        {
            LogToConsole($"Travel cannot be performed, because vehicle {vehicleName} was not found");
            return;
        }

        var destination = Route.GetDestinationByName(destinationName);
        if (destination == null)
        {
            LogToConsole($"Travel cannot be performed, because destination {destinationName} was not found");
            return;
        }

        if (vehicle.IsAtPosition(destination.Position))
        {
            LogToConsole($"Vehicle {vehicleName} is already at destination {destinationName}");
            return;
        }

        TravelToDestination(vehicle, destination);
    }

    private void TravelToDestination(Vehicle vehicle, Destination destination)
    {
        var travelVector = vehicle.GetTravelVector(WindVector);

        var expectedHeading = vehicle.Position < destination.Position
            ? Route.Heading
            : Vector.GetOpposedHeading(Route.Heading);

        if (travelVector.Heading != expectedHeading)
        {
            LogToConsole(
                $"Travel cannot be performed, because vehicle {vehicle.Name} is moving away from destination {destination.Name}");
            return;
        }

        var travelTime =
            Vehicle.CalculateTravelTime(Math.Abs(vehicle.Position - destination.Position), travelVector);

        RunSimulationByTime(travelTime);
    }

    public (Time duration, double intersectionPosition)? CalculateIntersection(string firstVehicleName,
        string secondVehicleName)
    {
        LogToConsole($"Calculating intersection position for vehicles {firstVehicleName} and {secondVehicleName}");

        if (Route.Heading == Heading.None)
        {
            LogToConsole(
                "Intersection calculation cannot be performed, because the route does not have heading specified");
            return null;
        }

        var (first, second) = (GetVehicleByName(firstVehicleName), GetVehicleByName(secondVehicleName));

        if (first is null || second is null)
        {
            LogToConsole(
                $"Cannot calculate intersection position, because vehicle {(first is null ? firstVehicleName : secondVehicleName)} was not found");
            return null;
        }

        if (first.IsAtPosition(second.Position))
        {
            LogToConsole("Vehicles are already at intersection position");
            return (Time.None, first.Position);
        }

        var (firstTravelVector, secondTravelVector) =
            (first.GetTravelVector(WindVector), second.GetTravelVector(WindVector));

        if (!Route.IsVectorOnRoute(firstTravelVector) || !Route.IsVectorOnRoute(secondTravelVector))
            LogToConsole(
                "Cannot calculate intersection position, because vehicle vector headings doesn't align with route heading");

        if (firstTravelVector.Heading == secondTravelVector.Heading)
            return CalculateIntersectionForChasing(firstTravelVector, first.Position, secondTravelVector,
                second.Position);

        return CalculateIntersectionForMeeting(firstTravelVector, first.Position, secondTravelVector, second.Position);
    }

    private (Time duration, double intersectionPosition)? CalculateIntersectionForMeeting(Vector firstTravelVector,
        double firstPosition, Vector secondTravelVector, double secondPosition)
    {
        if (firstTravelVector.Velocity.Equals(secondTravelVector.Velocity) && secondTravelVector.Velocity.Equals(Velocity.None))
        {
            LogToConsole("Intersection for meeting scenario is not possible, because both vehicles are not moving");
            return null;
        }

        var timeResult =
            (firstTravelVector.Heading == Route.Heading
                ? secondPosition - firstPosition
                : firstPosition - secondPosition) /
            Velocity.Sum(firstTravelVector.Velocity, secondTravelVector.Velocity).MetersPerSecond;

        var positionResult = Math.Round(firstPosition + firstTravelVector.Velocity.MetersPerSecond * timeResult *
                                        (firstTravelVector.Heading == Route.Heading ? 1 : -1), 4);

        LogToConsole($"Vehicles will intersect at position {positionResult} meters after time {new Time(timeResult)}");

        return (new Time(timeResult), positionResult);
    }

    private (Time duration, double intersectionPosition)? CalculateIntersectionForChasing(Vector firstTravelVector,
        double firstPosition, Vector secondTravelVector, double secondPosition)
    {
        if (firstTravelVector.Velocity.Equals(secondTravelVector.Velocity))
        {
            LogToConsole(
                "Intersection for chasing scenario is not possible, because both vehicles travel at the same speed");
            return null;
        }

        var timeResult = (firstPosition - secondPosition) / (firstTravelVector.Heading == Route.Heading
            ? firstTravelVector.Velocity.MetersPerSecond - secondTravelVector.Velocity.MetersPerSecond
            : secondTravelVector.Velocity.MetersPerSecond - firstTravelVector.Velocity.MetersPerSecond);

        if (timeResult < 0)
        {
            LogToConsole(
                "Intersection for chasing scenario is not possible, because chase vehicle is slower than escaping");
            return null;
        }

        var positionResult = Math.Round(firstPosition + firstTravelVector.Velocity.MetersPerSecond * timeResult *
            (firstTravelVector.Heading == Route.Heading ? 1 : -1), 4);

        LogToConsole($"Vehicles will intersect at position {positionResult} after time {new Time(timeResult)}");

        return (new Time(timeResult), positionResult);
    }

    private Vehicle? GetVehicleByName(string name)
    {
        return Vehicles.FirstOrDefault(v => v.Name.Equals(name, StringComparison.CurrentCulture));
    }

    private static void LogToConsole(string message, string prefix = "Simulation")
    {
        Console.WriteLine($"[{prefix}] {message}");
    }
}