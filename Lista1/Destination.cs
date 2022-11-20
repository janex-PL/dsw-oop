namespace DSW_ProgramowanieObiektowe.Lista1;
public class Destination
{
    public string Name { get; }
    public double Position { get; }

    public Destination(string name, double position)
    {
        Name = name;
        Position = position;
    }

    public override string ToString()
    {
        return $"Destination '{Name}' at position {Position} meters = {Position/1000} kilometers";
    }
}
