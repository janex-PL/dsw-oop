namespace List3;
internal class Enclosure<T> where T : Animal
{
    private List<T> Animals { get; set; } = new();

    public int Count => Animals.Count;

    public void Add(T animal)
    {
        if (animal is not null)
            Animals.Add(animal);
    }

    public void RemoveAll()
    {
        Animals = new List<T>();
    }
}
