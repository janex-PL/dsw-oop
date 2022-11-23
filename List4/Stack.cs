using List4.Exceptions.Stack;

namespace List4;

public class Stack
{
    private readonly int[] _array;
    public int Size { get; private set; } = 0;
    public int MaxLength => _array.Length;
    public Stack(int maxLength)
    {
        Console.WriteLine($"Creating stack with maximum length of {maxLength}");
        if (maxLength < 0)
            throw new IllegalArgumentException("Stack length must be greater or equal 0");

        _array = new int[maxLength];
    }

    public int Top()
    {
        Console.WriteLine("Performing top operation on stack");
        if(Size == 0)
            throw new StackEmptyException($"Could not get top value from the stack, because stack is empty");
        
        var result = _array[Size - 1];
        Console.WriteLine($"Top result is {result}");

        return result;
    }

    public void Push(int value)
    {
        Console.WriteLine($"Pushing value {value} to stack");
        if (Size == _array.Length)
            throw new StackFullException($"Could not push value {value} to the stack, because maximum length of the stack ({_array.Length} was reached");
        _array[Size] = value;
        Size++;
    }

    public int Pop()
    {
        Console.WriteLine("Popping item from stack");
        if (Size == 0)
            throw new StackEmptyException($"Could not pop value from the stack, because stack is empty");
        
        Size--;
        var result = _array[Size];
        Console.WriteLine($"Pop result is {result}");

        return result;
    }

    public void Clear()
    {
        Console.WriteLine("Clearing stack");
        
        Size = 0;
    }
}