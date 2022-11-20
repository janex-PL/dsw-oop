namespace Lista2;
public abstract class Vehicle
{
    public string Id { get; }
    public string Location { get; private set; }
    private List<Container> _containers { get; } = new();
    public IReadOnlyCollection<Container> Container => _containers;

    public static int TotalTravelCount { get; private set; } = 0;

    private static readonly object _travelCountLock = new();

    public Vehicle(string id, string location)
    {
        Id = id;
        Location = location;
    }

    protected abstract int GetMaxCapacity();
    protected abstract string GetTravelDetails(string destination);
    public int GetAvailableCapacity() => GetMaxCapacity() - _containers.Count;

    public void Travel(Warehouse destination)
    {
        if (destination.Location.Equals(Location, StringComparison.CurrentCulture))
        {
            Console.WriteLine($"Vehicle {Id} is already at {destination.Location}");
            return;
        }

        Console.WriteLine($"[{Id}] {GetTravelDetails(destination.Location)}");
        Location = destination.Location;

        lock (_travelCountLock)
        {
            TotalTravelCount++;
        }
    }


    public void Load(Container container)
    {
        if (_containers.Count >= GetMaxCapacity())
            throw new Exception($"Cannot load {container} to vehicle {Id}, because it's full");

        //Console.WriteLine($"Loading {container} to vehicle {Id}");

        container.Location = Id;
        _containers.Add(container);
    }

    public void LoadRange(List<Container> containers)
    {
        if (_containers.Count + containers.Count > GetMaxCapacity())
            throw new Exception(
                $"Cannot load range of containers: {string.Join(',', containers)}, because maximum capacity will be exceeded");

        containers.ForEach(Load);
    }

    public int GetContainersCount()
    {
        return _containers.Count;
    }
    public void UnloadToWarehouse(Warehouse warehouse)
    {
        warehouse.LoadContainers(_containers);

        _containers.Clear();
    }

}
