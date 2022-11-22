using List4;

foreach (var x in Enumerable.Range(1, 20))
{
    Console.WriteLine($"X: {x}; Result: {Factorial.Calculate(x)}");
}