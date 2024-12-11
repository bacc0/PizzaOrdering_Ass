namespace PizzaOrderingSystem.Models;

public class Pizza
{
    public string Type { get; set; }
    public string Size { get; set; }
    public List<string> Toppings { get; private set; } = new List<string>();

    public Pizza(string type, string size)
    {
        Type = type;
        Size = size;
    }

    public void AddToppings(List<string> toppings)
    {
        Toppings.AddRange(toppings);
    }
}

