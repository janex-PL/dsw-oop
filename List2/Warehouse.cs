namespace List2;
public class Warehouse
{
    public string Location { get; }
    public List<Container> Containers { get; } = new();
    public Dictionary<string, List<Container>> ReservedContainers { get; } = new();

    public Warehouse(string location)
    {
        Location = location;
    }

    public void LoadContainers(List<Container> containers)
    {
        containers.ForEach(c =>
        {
            c.Location = Location;
            Containers.Add(c);
        });

        Console.WriteLine($"[{Location}] Loaded {containers.Count} containers to warehouse");
    }
    public bool ReserveContainers(Func<Container, bool> query, Vehicle vehicle)
    {
        if (ReservedContainers.TryGetValue(vehicle.Id, out _))
        {
            Console.WriteLine($"[{Location}] Vehicle {vehicle.Id} has already reserved containers in this warehouse");
            return false;
        }
        
        var containers = Containers.Where(query).Take(vehicle.GetAvailableCapacity()).ToList();
        if (!containers.Any())
            return false;

        ReservedContainers.Add(vehicle.Id,containers);

        containers.ForEach(c => Containers.Remove(c));
            
        Console.WriteLine($"[{Location}] {containers.Count} containers reserved for vehicle {vehicle.Id}");

        return true;
    }
    public void LoadToVehicle(Vehicle vehicle)
    {
        if (ReservedContainers.TryGetValue(vehicle.Id, out var containers))
        {
            vehicle.LoadRange(containers);

            Console.WriteLine($"[{Location}] Loaded {containers.Count} containers to vehicle {vehicle.Id}");

            ReservedContainers.Remove(vehicle.Id);

            return;
        }

        Console.WriteLine($"[{Location}] Could not load containers to vehicle {vehicle.Id}, because no containers were reserved");
    }
    public void DisplayStock()
    {
        Console.WriteLine($"[{Location}] Current stock:");
        Containers.ForEach(c =>
        {
            Console.WriteLine($"[{Location}]\t- {c}");
        });
    }
}
