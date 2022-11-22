namespace List2;
public class Train : Vehicle
{
    private static int _idCounter = 1;

    public Train(string location) : base($"TRA_{_idCounter.ToString().PadLeft(2,'0')}", location)
    {
        _idCounter++;
    }

    protected override int GetMaxCapacity() => 20;

    protected override string GetTravelDetails(string destination) =>
        $"Performing travel with train from {Location} to {destination}";
}
