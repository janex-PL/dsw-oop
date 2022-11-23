using List4.Exceptions.Factorial;
using List4.Exceptions.Stack;

namespace List4;

public class TaskSolver
{
    public static void SolveFactorialTask()
    {
        Console.WriteLine(new string('#',20));
        Console.WriteLine("Task #1 - Factorial");
        Console.WriteLine(new string('#',20));

        var input = string.Empty;
        long factorialInput;

        while (!long.TryParse(input, out factorialInput))
        {
            Console.Write("Input number for factorial:");
            input = Console.ReadLine();
        }

        try
        {
            var result = Factorial.Calculate(factorialInput);
            Console.WriteLine($"Factorial of number {factorialInput} equals {result}");
        }
        catch (NegativeValueException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void SolveAddressTask()
    {
        Console.WriteLine(new string('#',20));
        Console.WriteLine("Task #2 - Address");
        Console.WriteLine(new string('#',20));

        Console.Write("Input street:");
        var street = Console.ReadLine();
        Console.Write("Input building number:");
        var buildingNumber = Console.ReadLine();
        Console.Write("Input postal code:");
        var postalCode = Console.ReadLine();
        Console.Write("Input city:");
        var city = Console.ReadLine();

        try
        {
            var address = new Address(street, buildingNumber, postalCode, city);
            
            Console.WriteLine(address);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occured while parsing address data");
            Console.WriteLine(e.Message);
        }

    }

    public static void SolveStackTask()
    {
        Console.WriteLine(new string('#',20));
        Console.WriteLine("Task #3 - Stack");
        Console.WriteLine(new string('#',20));

        Console.WriteLine("1. Valid stack operations");

        try
        {
            ValidStackExample();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine("\n2. Invalid stack size");
        try
        {
            InvalidStackSizeExample();
        }
        catch (IllegalArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        
        Console.WriteLine("\n3. Pushing to full stack");
        try
        {
            FullStackPushExample();
        }
        catch (StackFullException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine("\n4. Popping from empty stack");

        try
        {
            EmptyStackPopExample();
        }
        catch (StackEmptyException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void EmptyStackPopExample()
    {
        var stack = new Stack(10);

        stack.Pop();
    }

    private static void FullStackPushExample()
    {
        var stack = new Stack(1);

        foreach (var item in Enumerable.Range(1,10))
        {
            stack.Push(item);
        }
    }

    public static void ValidStackExample()
    {
        var stack = new Stack(5);

        foreach (var item in Enumerable.Range(1,5))
        {
            stack.Push(item);
        }

        stack.Top();
        stack.Pop();
        stack.Top();
       
        stack.Clear();

        Console.WriteLine($"Stack currently holds {stack.Size} items");
    }

    public static void InvalidStackSizeExample()
    {
        var stack = new Stack(-10);
    }
    
    public static void WaitForKeyAndClean()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}