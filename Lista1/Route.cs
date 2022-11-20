namespace DSW_ProgramowanieObiektowe.Lista1;
public class Route
{
    private List<Destination> Destinations { get; } = new();
    public Heading Heading { get; } = Heading.None;

    public Route()
    {
    }
    public Route(Heading heading, List<Destination> destinations)
    {
        Heading = heading;
        Destinations = destinations;
    }

    public override string ToString() =>
        $"Route heading: {Heading}\nDestinations:\n" +
        string.Join('\n',Destinations.Select(d => d.ToString()));

    public Destination? GetDestinationByName(string name) =>
        Destinations.FirstOrDefault(d => d.Name.Equals(name, StringComparison.CurrentCulture));

    public bool IsVectorOnRoute(Vector vector) =>
        vector.Heading == Heading || vector.Heading == Vector.GetOpposedHeading(Heading);
}
