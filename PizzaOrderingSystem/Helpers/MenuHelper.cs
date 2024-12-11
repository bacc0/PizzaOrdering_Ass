using System;
using System.Collections.Generic;

namespace PizzaOrderingSystem.Helpers;

public static class MenuHelper
{
    public static string ChoosePizza()
    {
        while (true)
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Pizza Menu ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("1. Margherita");
            Console.WriteLine("2. Pepperoni");
            Console.WriteLine("3. Hawaiian");
            Console.WriteLine("4. Neapolitan");
            Console.WriteLine("5. Meat Lover's");
            Console.WriteLine("6. Margherita con Funghi");
            Console.WriteLine("7. BBQ Chicken");
            Console.WriteLine("8. Sicilian");
            Console.Write("\nEnter your choice (1-8): ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 8)
            {
                return choice switch
                {
                    1 => "Margherita",
                    2 => "Pepperoni",
                    3 => "Hawaiian",
                    4 => "Neapolitan",
                    5 => "Meat Lover's",
                    6 => "Margherita con Funghi",
                    7 => "BBQ Chicken",
                    8 => "Sicilian",
                    _ => throw new InvalidOperationException()
                };
            }

            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please select a valid option! ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
    }

    public static string ChoosePizzaSize()
    {
        while (true)
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Pizza Size ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("1. Xtra Large");
            Console.WriteLine("2. Large");
            Console.WriteLine("3. Regular");
            Console.Write("\nEnter your choice (1-3): ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3)
            {
                return choice switch
                {
                    1 => "Xtra Large",
                    2 => "Large",
                    3 => "Regular",
                    _ => throw new InvalidOperationException()
                };
            }

            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please select a valid option! ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
    }

    public static List<string> ChooseToppings(string pizzaType)
    {
        Console.WriteLine($"\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Do you want to add TOPPINGS to your {pizzaType} pizza? (y/n) ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        if (!Utilities.AskYesNo(" "))
        {
            return new List<string>();
        }

        var toppings = new List<string>();

        Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** TOPPINGS ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("1. Pepperoni");
        Console.WriteLine("2. Mushroom");
        Console.WriteLine("3. Extra cheese");
        Console.WriteLine("4. Sausage");
        Console.WriteLine("5. Onion");
        Console.WriteLine("6. Black olives");
        Console.WriteLine("7. Green pepper");
        Console.WriteLine("8. Fresh garlic");
        Console.WriteLine("\nEnter the topping numbers separated by space (e.g., 1 2 3): ");

        string[] toppingChoices = Console.ReadLine()?.Split();

        if (toppingChoices != null)
        {
            foreach (var choice in toppingChoices)
            {
                if (int.TryParse(choice, out int toppingNumber) && toppingNumber >= 1 && toppingNumber <= 8)
                {
                    string topping = toppingNumber switch
                    {
                        1 => "Pepperoni",
                        2 => "Mushroom",
                        3 => "Extra cheese",
                        4 => "Sausage",
                        5 => "Onion",
                        6 => "Black olives",
                        7 => "Green pepper",
                        8 => "Fresh garlic",
                        _ => null
                    };

                    toppings.Add(topping);
                }
                else
                {
                    Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Invalid topping number! Please try again. ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    return ChooseToppings(pizzaType); // Re-prompt for valid toppings
                }
            }
        }

        // Console.WriteLine("\nToppings added: " + string.Join(", ", toppings + "\n"));
        Console.WriteLine("\nToppings added: " + string.Join(", ", toppings) + "\n");
        Utilities.WaitForKey();

        return toppings;
    }
}

