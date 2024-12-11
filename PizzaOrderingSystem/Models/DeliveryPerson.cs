namespace PizzaOrderingSystem.Models;
public class DeliveryPerson
{
    private static readonly string[] Names = { "George", "Simon", "Jack", "Olivia" };
    public string Name { get; private set; }

    private DeliveryPerson(string name)
    {
        Name = name;
    }

    public static DeliveryPerson AssignDriver()
    {
        Random random = new Random();
        string name = Names[random.Next(Names.Length)];
        return new DeliveryPerson(name);
    }
}

