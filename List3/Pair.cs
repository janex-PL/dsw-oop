namespace List3;
internal class Pair<T> where T : IComparable
{
    private T Left { get; set; }
    private T Right { get; set; }

    public Pair(T left, T right)
    {
        Left = left;
        Right = right;
    }

    public void Swap()
    {
        (Left, Right) = (Right, Left);
    }

    public override string ToString()
    {
        return $"Left: {Left}\tRight: {Right}";
    }

    public T Max()
    {
        return Left.CompareTo(Right) < 0 ? Left : Right;
    }
}
