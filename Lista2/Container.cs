namespace Lista2;
public class Container
{
    private static int _idCounter = 1;

    public string Id { get; }
    public string Location { get; set; } = "None";
    public Goods Content { get; private set; } = Goods.None();

    public Container()
    {
        Id = $"C_{_idCounter}";
        _idCounter++;
    }

    public Container(Goods goods) : this()
    {
        Load(goods);
    }

    public override string ToString()
    {
        return $"Container with id: {Id} containing {Content}";
    }

    public void Load(Goods goods)
    {
        if (Content is not {GoodsType:GoodsType.None})
            throw new Exception($"Cannot load {goods} because container {Id} already contains {Content}");

        Content = goods;

        Console.WriteLine($"[{Id}] {goods} was loaded to container");
    }

    public Goods Unload()
    {
        if (Content is {GoodsType:GoodsType.None})
            throw new Exception($"Cannot container {Id}, because it's empty");

        var result = Content;
        Content = Goods.None();
        return result;
    }
}
