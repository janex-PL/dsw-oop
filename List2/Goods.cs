namespace List2;
public class Goods
{
    public GoodsType GoodsType { get; }

    public Goods(GoodsType goodsType)
    {
        GoodsType = goodsType;
    }

    public override string ToString()
    {
        return GoodsType.ToString();
    }

    public static Goods None() => new(GoodsType.None);
}
