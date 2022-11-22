namespace List2;
public class Simulation
{
    public Dictionary<string,List<Vehicle>> Vehicles { get; }= new();
    public Dictionary<GoodsType,Goods> Goods { get; }= new();
    public Dictionary<string,Warehouse> Warehouses { get; }= new();
    public List<Container> Containers { get; }= new();

    public void DisplaySimulationInfo()
    {
        Console.WriteLine("Vehicles: ");
        foreach (var pair in Vehicles)
        {
            Console.WriteLine($"\t- {pair.Key}");
            foreach (var vehicle in pair.Value)
            {
                Console.WriteLine($"\t\t -{vehicle.Id} at {vehicle.Location}");
            }
        }
    }

    public void RunSimulation()
    {
        Console.WriteLine("Starting simulation");
        InitializeSimulation();

        Console.WriteLine("Transporting containers from New York to Gdansk");

        TransportContainers(x => true, Warehouses["New York"], Warehouses["Gdansk"], Vehicles[nameof(Ship)]).GetAwaiter().GetResult();

        Console.WriteLine("Transporting containers from Gdansk to Wroclaw");

        TransportContainers(x => true, Warehouses["Gdansk"], Warehouses["Wroclaw"], Vehicles[nameof(Train)]).GetAwaiter().GetResult();

        Console.WriteLine("Transporting shoes and clothes from Wroclaw to Poznan");

        TransportContainers(x => x.Content.GoodsType is GoodsType.Clothes or GoodsType.Shoes, Warehouses["Wroclaw"],
            Warehouses["Poznan"], Vehicles[nameof(Truck)]).GetAwaiter().GetResult();

        Console.WriteLine("Transporting electronic parts from Wroclaw to Krakow");
        TransportContainers(x => x.Content.GoodsType is GoodsType.ElectronicParts, Warehouses["Wroclaw"],
            Warehouses["Krakow"], Vehicles[nameof(Truck)]).GetAwaiter().GetResult();

        Console.WriteLine(new string('#',25));

        foreach (var warehouse in Warehouses)
        {
            warehouse.Value.DisplayStock();
        }

        Console.WriteLine($"Total travel count: {Vehicle.TotalTravelCount}");
    }

    private void InitializeSimulation()
    {
        var goods = new List<Goods>
        {
            new(GoodsType.ElectronicParts),
            new(GoodsType.Cellphones),
            new(GoodsType.Clothes),
            new(GoodsType.Shoes)
        };
        goods.ForEach(g => Goods.Add(g.GoodsType, g));

        var warehouses = new List<Warehouse>
        {
            new("New York"),
            new("Gdansk"),
            new("Wroclaw"),
            new("Krakow"),
            new("Poznan")
        };
        warehouses.ForEach(w => Warehouses.Add(w.Location, w));

        Vehicles.Add(nameof(Ship),new List<Vehicle>
        {
            new Ship("Gdansk")
        });

        Vehicles.Add(nameof(Train), new List<Vehicle>
        {
            new Train("Gdansk"),
            new Train("Gdansk"),
        });

        Vehicles.Add(nameof(Truck),new List<Vehicle>
        {
            new Truck("Gdansk"),
            new Truck("Gdansk"),
            new Truck("Gdansk"),
            new Truck("Gdansk"),
            new Truck("Gdansk"),
            new Truck("Gdansk"),
            new Truck("Gdansk"),
            new Truck("Gdansk"),
            new Truck("Gdansk"),
            new Truck("Gdansk"),
        });

        Containers.AddRange(Enumerable.Range(0,10).Select(x => new Container(Goods[GoodsType.ElectronicParts])));
        Containers.AddRange(Enumerable.Range(0,20).Select(x => new Container(Goods[GoodsType.Cellphones])));
        Containers.AddRange(Enumerable.Range(0,5).Select(x => new Container(Goods[GoodsType.Shoes])));
        Containers.AddRange(Enumerable.Range(0,5).Select(x => new Container(Goods[GoodsType.Clothes])));

        Warehouses["New York"].LoadContainers(Containers);
    }

    private void ImportContainersFromAbroad()
    {
        var ships = Vehicles[nameof(Ship)];

        var source = Warehouses["New York"];

        var target = Warehouses["Gdansk"];

        TransportContainers((c => true), source, target, ships).GetAwaiter().GetResult();
    }

    private void MoveAllContainersToWroclaw()
    {
        var trains = Vehicles[nameof(Train)];

        var source = Warehouses["Gdansk"];

        var target = Warehouses["Wroclaw"];

        TransportContainers(c => true, source,target,trains).GetAwaiter().GetResult();
    }

    private void TransportContainers(Func<Container, bool> containerQuery, Warehouse source, Warehouse target,
        Vehicle transport)
    {
        while (source.ReserveContainers(containerQuery,transport))
        {
            if (transport.Location != source.Location)
                transport.Travel(source);

            source.LoadToVehicle(transport);

            transport.Travel(target);

            transport.UnloadToWarehouse(target);
        }
    }

    private async Task TransportContainers(Func<Container, bool> containerQuery, Warehouse source, Warehouse target,
        List<Vehicle> transport)
    {
        var tasks = transport.Select(vehicle => Task.Run(() => TransportContainers(containerQuery,source,target,vehicle))).ToList();

        await Task.WhenAll(tasks);
    }

}
