
using MyMonkeyApp.Helpers;
using MyMonkeyApp.Models;

namespace MyMonkeyApp;

internal class Program
{
    private static readonly List<string> asciiArtList = new()
    {
        @"  (\__/)",
        @"  (o.o)",
        "  (\"\")(\")",
        @"   /\_/\  ((  ))",
        @"  ( o.o )  (..) ",
        @"   > ^ <   /\_/\",
        @"  (\\(\\  (\\(\\  (\\(\\",
        @"  (='.'=) (='.'=) (='.'=)",
        "  (\"_\")_(\") (\"_\")_(\") (\"_\")_(\")"
    };

    private static void DisplayRandomAsciiArt()
    {
        var random = new Random();
        var art = asciiArtList[random.Next(asciiArtList.Count)];
        Console.WriteLine(art);
    }

    public static async Task Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            DisplayRandomAsciiArt();
            Console.WriteLine("\nMonkey Console Application");
            Console.WriteLine("1. List all monkeys");
            Console.WriteLine("2. Get details for a specific monkey by name");
            Console.WriteLine("3. Get a random monkey");
            Console.WriteLine("4. Exit app");
            Console.Write("Select an option: ");

            var userInput = Console.ReadLine();
            Console.WriteLine();

            switch (userInput)
            {
                case "1":
                    var monkeys = await MonkeyHelper.GetMonkeysAsync();
                    if (monkeys.Count == 0)
                    {
                        Console.WriteLine("No monkey data available. Please check your network or server URL.");
                    }
                    else
                    {
                        Console.WriteLine("Available Monkeys:");
                        Console.WriteLine("--------------------------------------------------------------");
                        Console.WriteLine($"{"Name",-20} {"Location",-25} {"Population",-10}");
                        Console.WriteLine("--------------------------------------------------------------");
                        foreach (var monkey in monkeys)
                        {
                            Console.WriteLine($"{monkey.Name,-20} {monkey.Location,-25} {monkey.Population,-10}");
                        }
                        Console.WriteLine("--------------------------------------------------------------");
                    }
                    break;
                case "2":
                    Console.Write("Enter monkey name: ");
                    var name = Console.ReadLine() ?? string.Empty;
                    var foundMonkey = await MonkeyHelper.GetMonkeyByNameAsync(name);
                    if (foundMonkey != null)
                    {
                        Console.WriteLine($"Name: {foundMonkey.Name}");
                        Console.WriteLine($"Location: {foundMonkey.Location}");
                        Console.WriteLine($"Population: {foundMonkey.Population}");
                        Console.WriteLine($"Details: {foundMonkey.Details}");
                        Console.WriteLine($"Image: {foundMonkey.Image}");
                        Console.WriteLine($"Coordinates: {foundMonkey.Latitude}, {foundMonkey.Longitude}");
                    }
                    else
                    {
                        Console.WriteLine("Monkey not found.");
                    }
                    break;
                case "3":
                    var randomMonkey = await MonkeyHelper.GetRandomMonkeyAsync();
                    if (randomMonkey != null)
                    {
                        Console.WriteLine($"Random Monkey: {randomMonkey.Name}");
                        Console.WriteLine($"Location: {randomMonkey.Location}");
                        Console.WriteLine($"Population: {randomMonkey.Population}");
                        Console.WriteLine($"Details: {randomMonkey.Details}");
                        Console.WriteLine($"Image: {randomMonkey.Image}");
                        Console.WriteLine($"Coordinates: {randomMonkey.Latitude}, {randomMonkey.Longitude}");
                        Console.WriteLine($"Random monkey accessed {MonkeyHelper.GetRandomMonkeyAccessCount()} times.");
                    }
                    else
                    {
                        Console.WriteLine("No monkeys available.");
                    }
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
