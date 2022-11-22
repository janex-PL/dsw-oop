namespace List1;
public class Vector
{
    public Heading Heading { get; } = Heading.None;
    public Velocity Velocity { get; } = Velocity.None;

    public Vector()
    {
    }
    public Vector(Heading heading, Velocity velocity)
    {
        Heading = heading;
        Velocity = velocity;
    }
    public override string ToString()
    {
        return $"Heading: {Heading}; Velocity: {Velocity}";
    }

    public static Heading GetOpposedHeading(Heading heading)
    {
        return heading switch
        {
            Heading.North => Heading.South,
            Heading.South => Heading.North,
            Heading.West => Heading.East,
            Heading.East => Heading.West,
            _ => throw new ArgumentOutOfRangeException(nameof(heading), heading, null)
        };
    }

    public static Vector Sum(Vector first, Vector second)
    {
        if (first.Heading == second.Heading)
            return new Vector(first.Heading, Velocity.Sum(first.Velocity, second.Velocity));

        if (GetOpposedHeading(first.Heading) != second.Heading)
            return first;

        return new Vector(Velocity.Compare(first.Velocity, second.Velocity) > 0 ? first.Heading : second.Heading,
            Velocity.Difference(first.Velocity, second.Velocity));


    }
}
