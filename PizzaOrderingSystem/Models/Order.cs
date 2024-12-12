namespace PizzaOrderingSystem.Models;

public interface IOrder
{
    int CustomerId { get; }
    List<Pizza> Pizzas { get; }
    string Status { get; }

    void AddPizza(Pizza pizza);
    void CompleteOrder();
    void CancelOrder(string reason);
}
public class Order
{
    public int CustomerId { get; }
    public List<Pizza> Pizzas { get; private set; } = new List<Pizza>();
    public string Status { get; private set; } = "In Progress";

    public Order(int customerId)
    {
        CustomerId = customerId;
    }

    public void AddPizza(Pizza pizza)
    {
        Pizzas.Add(pizza);
    }
    // Method Marks the order as completed.
    public void CompleteOrder()
    {
        Status = "Completed";
    }
    // Method Cancels the order with a specified reason.
    public void CancelOrder(string reason)
    {
        Status = $"Cancelled: {reason}";
    }
}

