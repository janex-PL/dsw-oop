namespace Lista2;
public class Ship : Vehicle
{
    private static int _idCounter = 1;

    public Ship(string location) : base($"SHP_{_idCounter.ToString().PadLeft(2,'0')}", location)
    {
        _idCounter++;
    }

    protected override int GetMaxCapacity() => 90;

    protected override string GetTravelDetails(string destination) =>
        $"Performing travel with ship from {Location} to {destination}";
}
