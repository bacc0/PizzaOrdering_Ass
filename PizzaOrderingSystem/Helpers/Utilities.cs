using System;

namespace PizzaOrderingSystem.Helpers;
public interface IUtilities
{
    bool AskYesNo(string message);
    void WaitForKey();
}
public static class Utilities
{
    public static bool AskYesNo(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            // string input = Console.ReadLine()?.ToLower();

            // Convert input to lowercase, default to an empty string if null
            string input = Console.ReadLine()?.ToLower() ?? string.Empty;
            if (input == "y") return true;
            if (input == "n") return false;

            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please select a valid option! ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
    }

    public static void WaitForKey()
    {
        Console.WriteLine("\n✅ ✅ ✅ CLICK ANY BUTTON TO CONTINUE  ✅ ✅ ✅");
        Console.ReadKey();
    }
}

