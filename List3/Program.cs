using List3;

Console.WriteLine(new string('#',20));
Console.WriteLine("Task #1");

Console.WriteLine("Creating pairs and displaying max values\n");

var stringPair = new Pair<string>("left-value", "right-value");
var intPair = new Pair<int>(-10, 10);
var doublePair = new Pair<double>(-10.5, 10.5);

Console.WriteLine(stringPair + ", Max value: " + stringPair.Max());
Console.WriteLine(intPair + ", Max value: " + intPair.Max());
Console.WriteLine(doublePair + ", Max value: " + doublePair.Max());

Console.WriteLine("\nSwapping and displaying pairs with max values");
stringPair.Swap();
intPair.Swap();
doublePair.Swap();
Console.WriteLine(stringPair + ", Max value: " + stringPair.Max());
Console.WriteLine(intPair + ", Max value: " + intPair.Max());
Console.WriteLine(doublePair + ", Max value: " + doublePair.Max());

Console.WriteLine(new string('#',20));
Console.WriteLine("Task #2");
Console.WriteLine(new string('#',20));
Console.WriteLine("Creating bird enclosure");
var birdsEnclosure = new Enclosure<Bird>();
Console.WriteLine("Adding birds to enclosure");
birdsEnclosure.Add(new Bird("Bird_A"));
birdsEnclosure.Add(new Bird("Bird_B"));
birdsEnclosure.Add(new Bird("Bird_C"));
Console.WriteLine($"Current animal count in enclosure: {birdsEnclosure.Count}");
Console.WriteLine("Removing birds from enclosure");
birdsEnclosure.RemoveAll();
Console.WriteLine($"Current animal count in enclosure: {birdsEnclosure.Count}");

// Compile time error
//birdsEnclosure.Add(new Mammal("Mammal_A"));

// Compile time error
//birdsEnclosure.Add((Animal) new Mammal("Mammal_A"));

// Runtime exception System.InvalidCastException
//birdsEnclosure.Add((Bird)(Animal) new Mammal("Mammal_A"));

// No compile time errors and runtime exceptions, but the cast fails with fallback to null
//birdsEnclosure.Add((Animal) new Mammal("Mammal_A") as Bird);
Console.Write("Press any key to continue");
Console.ReadKey();