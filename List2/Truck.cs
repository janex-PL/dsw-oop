namespace Lista2;
public class Truck : Vehicle
{
    private static int _idCounter = 1;

    public Truck(string location) : base($"TRU_{_idCounter.ToString().PadLeft(2,'0')}", location)
    {
        _idCounter++;
    }

    protected override int GetMaxCapacity() => 1;

    protected override string GetTravelDetails(string destination) =>
        $"Performing travel with truck form {Location} to {destination}";
}
