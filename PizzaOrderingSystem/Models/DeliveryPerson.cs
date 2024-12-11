namespace PizzaOrderingSystem.Models;
public class DeliveryPerson
{
    // These are the names of the drivers.
    private static readonly string[] Names = { "George", "Simon", "Jack", "Olivia" };
    public string Name { get; private set; }

    private DeliveryPerson(string name)
    {
        Name = name;
    }
    // Method for assigning
    public static DeliveryPerson AssignDriver()
    {
        // Selecting a random number to mimic the process of assigning a delivery to one of the available drivers.
        Random random = new Random();
        string name = Names[random.Next(Names.Length)];
        return new DeliveryPerson(name);
    }
}

