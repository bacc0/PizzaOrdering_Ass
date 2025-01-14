using PizzaOrderingSystem.Helpers;
using PizzaOrderingSystem.Models;

namespace PizzaOrderingSystem;


class Program
{
    static void Main(string[] args)
    {
        // Initialize the database if it doesn't exist
        DatabaseHelper.InitializeDatabase();

        while (true)
        {
            Console.WriteLine("\n\n\n\n\n\n★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ \n  ");
            Console.WriteLine(ArtAssets.Welcome);

            if (!Utilities.AskYesNo("★       Do you want to make an order? (y/n)      ★  \n \n \n"))
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** GOODBYE ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                Console.WriteLine(ArtAssets.Welcome);
                break;
            }
            // Save the customer to the database
            var customer = new Customer();

            // Gather customer information   
            customer.Input();

            // int customerId = DatabaseHelper.InsertCustomer(customer);
            int customerId = customer.Save();

            // Print Welcome message
            customer.PrintWelcome();

            // Create and process order
            var order = new Order(customerId);

            // Choose pizza
            string pizzaType = MenuHelper.ChoosePizza();
            string pizzaSize = MenuHelper.ChoosePizzaSize();

            // Create pizza
            var pizza = new Pizza(pizzaType, pizzaSize);

            // Add toppings
            List<string> toppings = MenuHelper.ChooseToppings(pizzaType);
            pizza.AddToppings(toppings);

            order.AddPizza(pizza);

            // Save order and pizza details to database
            int orderId = DatabaseHelper.InsertOrder(customerId, "Cancelled: Unsuccessful Payment");
            int pizzaId = DatabaseHelper.InsertPizza(orderId, pizzaType, pizzaSize);

            foreach (var topping in toppings)
            {
                DatabaseHelper.InsertTopping(pizzaId, topping);
            }
            // Process payment
            if (!ProcessPayment(order))
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** GOODBYE ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                break;
            }

            Console.WriteLine(ArtAssets.PREPARING);
            Console.WriteLine("\n  👋  Your order is just starting to be prepared\n");
            Utilities.WaitForKey();

            // Assign delivery
            var deliveryPerson = DeliveryPerson.AssignDriver();

            Console.WriteLine(ArtAssets.DELIVERY);

            Console.WriteLine($"\n\n*** 🚴Your delivery will arrive shortly from Driver {deliveryPerson.Name} >>> \n \n");
            Utilities.WaitForKey();

            // Confirm delivery
            if (Utilities.AskYesNo("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n *** Has the delivery been successful? (y/n) ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n\n"))
            {
                Console.WriteLine(ArtAssets.BON_APPETIT_GOOD_BUY);
                DatabaseHelper.UpdateOrderStatus(orderId, "Completed");
                break;
            }
            else
            {
                Console.WriteLine("\n\n\n- - - - - - - - - - - - - - - - - - - - - - - - -\n|                                                |\n|    We are really sorry for the inconvenience   |\n|            FULL REFUND HAS BEEN MADE           |\n|                                                |\n - - - - - - - - - - - - - - - - - - - - - - - - -\n \n \n");
                DatabaseHelper.UpdateOrderStatus(orderId, "Cancelled: Failed delivery");
                break;
            }
        }
    }

    static bool ProcessPayment(Order order)
    {
        // Ask the user how they want to pay
        if (!Utilities.AskYesNo("\n\n💵 💶 💰   Do you want to pay online (y)?  💴 💳 💰\n\n           Or on delivery in cash (n)?\n\n           Please choose one option (y/n)!    \n\n"))
        {
            // Handle on-delivery payment
            Console.WriteLine(ArtAssets.PREPARING);
            Console.WriteLine("\n  👋  Your order is just starting to be prepared!\n    \n  🚚  You need to pay the driver upon delivery!\n \n");
            Utilities.WaitForKey();
            return true; // Proceed as the payment will be handled on delivery
        }

        // Proceed with online payment
        if (!Utilities.AskYesNo("\n\n🔴 🟡 🟢 Is payment successful? (y/n) 🔴 🟡 🟢\n\n"))
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Unsuccessful payment ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            order.CancelOrder("Unsuccessful payment");
            return false; // Payment failed
        }

        // Payment successful
        Console.WriteLine(ArtAssets.PAYMENT_SUCCESS);
        Utilities.WaitForKey();
        return true;
    }
};
