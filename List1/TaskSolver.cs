namespace List1;
public class TaskSolver
{
    public static void SolveFirstTask()
    {
        Console.WriteLine("Task #1");

        var startTime = new Time(17, 0, 0);
        var route = new Route(Heading.West, new List<Destination>
        {
            new("HQ", 0),
            new("Concert", 300 * 1000)
        });
        var carpet = new Vehicle("Carpet", 0, new Vector(Heading.West, Velocity.FromMetersPerSecond(25)));
        var windVector = new Vector(Heading.West, Velocity.FromKnots(10));

        var targetTime = new Time(20, 30, 0);

        var simulation = new Simulation(route, new List<Vehicle> { carpet }, windVector, startTime);

        simulation.DisplaySimulationInfo();

        PrintSeparator();
        Console.WriteLine($"Target time = {targetTime}");
        PrintSeparator();

        simulation.TravelToDestination("Carpet", "Concert");

        PrintSeparator();

        Console.WriteLine($"Does magician arrive on time: {Time.Compare(targetTime, simulation.CurrentTime) >= 0}");

        PrintSeparator();

        WaitForKeyAndClean();
    }

    public static void SolveSecondTask()
    {
        Console.WriteLine("Task #2");

        var startTime = new Time(11, 20, 0);
        var route = new Route(Heading.South, new List<Destination>
        {
            new("Stegna", 0),
            new("Wrocław", 500 * 1000)
        });
        var carpet = new Vehicle("Carpet", 0, new Vector(Heading.South, Velocity.FromMetersPerSecond(25)));

        var firstWindVector = new Vector(Heading.North, Velocity.FromKnots(2));
        var secondWindVector = new Vector(Heading.West, Velocity.FromKnots(3));

        var firstPartTravelDuration = new Time(2, 30, 0);

        var simulation = new Simulation(route, new List<Vehicle> { carpet }, firstWindVector, startTime);

        simulation.DisplaySimulationInfo();

        PrintSeparator();
        Console.WriteLine(@$"Duration of first part of travel: {firstPartTravelDuration}
Wind vector for second part of travel: {secondWindVector}");
        PrintSeparator();
        simulation.RunSimulationByTime(firstPartTravelDuration);

        simulation.ChangeWindVector(secondWindVector);

        simulation.TravelToDestination("Carpet","Wrocław");

        PrintSeparator();
        Console.WriteLine($"Arrival time: {simulation.CurrentTime}");
        PrintSeparator();

        WaitForKeyAndClean();
    }

    public static void SolveThirdTask()
    {
        Console.WriteLine("Task #3");

        var startTime = new Time(10, 15, 0);

        var startDestination = new Destination("Wroclaw", 0);
        var endDestination = new Destination("Kraków", 270 * 1000);
        var route = new Route(Heading.East, new List<Destination> { startDestination, endDestination });

        var firstCarpet = new Vehicle("Carpet_1", 0, new Vector(Heading.East, Velocity.FromMetersPerSecond(25)));
        var secondCarpet = new Vehicle("Carpet_2", endDestination.Position,
            new Vector(Heading.West, Velocity.FromMetersPerSecond(25)));

        var windVector = new Vector(Heading.East, Velocity.FromKnots(8));

        var headStartDuration = new Time(0, 15, 0);

        var simulation = new Simulation(route, new List<Vehicle> { firstCarpet }, windVector, startTime);

        simulation.DisplaySimulationInfo();
        PrintSeparator();
        Console.WriteLine(@$"Vehicle {firstCarpet} starts from {startDestination}
Vehicle {secondCarpet} starts from {endDestination} after {headStartDuration}");
        PrintSeparator();

        simulation.RunSimulationByTime(headStartDuration);

        simulation.AddVehicle(secondCarpet);

        var result = simulation.CalculateIntersection(firstCarpet.Name, secondCarpet.Name);

        if (result == null)
            return;

        PrintSeparator();
        Console.WriteLine("Verifying calculations using simulation");
        PrintSeparator();

        simulation.RunSimulationByTime(result.Value.duration);
        PrintSeparator();
        Console.WriteLine(@$"Both carpets at the same position: {firstCarpet.IsAtPosition(secondCarpet.Position)}
Distance from starting destination: {firstCarpet.Position} meters = {firstCarpet.Position / 1000} kilometers
Meeting time: {simulation.CurrentTime}");
        PrintSeparator();

        WaitForKeyAndClean();
    }

    public static void SolveFourthTask()
    {
        var travelVelocity = Velocity.FromTravelTimeAndDistance(70 * 1000, new Time(0, 40, 0));

        var speedLimit = Velocity.FromKnots(40);

        Console.WriteLine(@$"Travel velocity: {travelVelocity}
Speed limit {speedLimit}
Did carpet exceeded speed limit: {Velocity.Compare(travelVelocity, speedLimit) > 0}");
        PrintSeparator();

        WaitForKeyAndClean();
    }

    public static void PrintSeparator()
    {
        Console.WriteLine(new string('-', 20));
    }

    public static void WaitForKeyAndClean()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}
