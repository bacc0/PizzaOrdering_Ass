using System;
using System.Collections.Generic;
using PizzaOrderingSystem.Helpers;
using PizzaOrderingSystem.Models;

namespace PizzaOrderingSystem;

class Program
{
    // private static readonly string ConnectionString = "Data Source=PizzaOrderingSystem.db;Version=3;";

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

            // // Gather customer information
            // Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please add your name ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            // string customerName = Console.ReadLine();

            // Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please add your address ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            // string customerAddress = Console.ReadLine();

            // Save the customer to the database
            var customer = new Customer();

            // Gather customer information   
            customer.Input();
            // int customerId = DatabaseHelper.InsertCustomer(customer);
            int customerId = customer.Save();
                // Print Welcome message
            customer.PrintWelcome();
            // Console.WriteLine($"\n\n*** Thank you, {customer.Name} from {customer.Address}! ***\n\n");

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


}











//  all Class together ( for demonstration purposes )


// using System;
// using System.Collections.Generic;
// using System.Data.SQLite;

// namespace PizzaOrderingSystem
// {
//     class Program
//     {
// // Connection string to SQLite database
//         private static readonly string ConnectionString = "Data Source=PizzaOrderingSystem.db;Version=3;";

//         static void Main(string[] args)
//         {
// // Initialize the database if it doesn't exist
//             InitializeDatabase();

// // Main program loop
//             while (true)
//             {
//                 Console.WriteLine(" \n \n \n \n \n \n  ~~~~~~~~~~~~~~~~~~~~~~~~~~\n|                          | \n|   WELCOME TO OUR PIZZA   |  \n|                          | \n ~~~~~~~~~~~~~~~~~~~~~~~~~~\n  \n");
// // Prompt the user to make an order
//                 if (!AskYesNo("*** Do you want to make an order? (y/n) *** \n"))
//                 {
//                     Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** GOODBYE ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
//                     break;
//                 }

// // Gather customer information
//                 Console.WriteLine(" \n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please add your name ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n");
//                 string customerName = Console.ReadLine();

//                 Console.WriteLine(" \n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please add your address ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n");
//                 string customerAddress = Console.ReadLine();

// // Save the customer to the database
//                 var customer = new Customer(customerName, customerAddress);
//                 int customerId = InsertCustomer(customer);

//                 Console.WriteLine($"\n \n*** Thank you, {customer.Name} from {customer.Address}! ***\n \n");

// // Create and process order
//                 var order = new Order(customerId);

// // Choose pizza
//                 string pizzaType = ChoosePizza();
//                 string pizzaSize = ChoosePizzaSize();

// // Create pizza
//                 var pizza = new Pizza(pizzaType, pizzaSize);

// // Add toppings
//                 List<string> toppings = ChooseToppings(pizzaType);
//                 pizza.AddToppings(toppings);

//                 order.AddPizza(pizza);

// // Save order and pizza details to database
//                 int orderId = InsertOrder(customerId, "Cancelled: Unsuccessful Payment");
//                 int pizzaId = InsertPizza(orderId, pizzaType, pizzaSize);

//                 foreach (var topping in toppings)
//                 {
//                     InsertTopping(pizzaId, topping);
//                 }

// // Process payment
//                 if (!ProcessPayment(order))
//                 {
//                     Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** GOODBYE ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
//                     break;
//                 }

//                 Console.WriteLine(" \n  \n ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n 👋 Your order is being prepared >>>\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n  \n ");
//                 WaitForKey();

// // Assign delivery
//                 var deliveryPerson = DeliveryPerson.AssignDriver();
//                 Console.WriteLine($"\n ~~~~~ \n*** 🚴Your delivery will arrive shortly from Driver {deliveryPerson.Name}  >>> \n ~~~~~ \n");
//                 WaitForKey();

// // Confirm delivery
//                 if (AskYesNo("\n \n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n *** Has the delivery been successful? (y/n) ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n \n"))
//                 {
//                     Console.WriteLine("\n \n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** BON APPETIT! GOODBYE ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n \n");
//                     UpdateOrderStatus(orderId, "Completed");
//                     break;
//                 }
//                 else
//                 {
//                     Console.WriteLine("\n \n \n*** We are really sorry for the inconvenience ***");
//                     Console.WriteLine("*** FULL REFUND HAS BEEN MADE ***\n \n \n");
//                     UpdateOrderStatus(orderId, "Cancelled: Failed delivery");
//                     break;
//                 }
//             }
//         }

//         static void InitializeDatabase()
//         {
//       try
//     {
// // Check if the database file exists
//         if (!File.Exists("PizzaOrderingSystem.db"))
//         {
//             SQLiteConnection.CreateFile("PizzaOrderingSystem.db");
//             Console.WriteLine("Database file 'PizzaOrderingSystem.db' created successfully.");
//         }
//         else
//         {
//             Console.WriteLine("Database file 'PizzaOrderingSystem.db' already exists.");
//         }

// // Open connection and create tables
//         using (var connection = new SQLiteConnection(ConnectionString))
//         {
//             connection.Open();
//             Console.WriteLine("Database connection opened successfully.");

//             var command = connection.CreateCommand();
//             command.CommandText = @"
//                 CREATE TABLE IF NOT EXISTS Customers (
//                     CustomerID INTEGER PRIMARY KEY AUTOINCREMENT,
//                     Name TEXT NOT NULL,
//                     Address TEXT NOT NULL
//                 );
//                 CREATE TABLE IF NOT EXISTS Orders (
//                     OrderID INTEGER PRIMARY KEY AUTOINCREMENT,
//                     CustomerID INTEGER NOT NULL,
//                     Status TEXT NOT NULL,
//                     FOREIGN KEY (CustomerID) REFERENCES Customers (CustomerID)
//                 );
//                 CREATE TABLE IF NOT EXISTS Pizzas (
//                     PizzaID INTEGER PRIMARY KEY AUTOINCREMENT,
//                     OrderID INTEGER NOT NULL,
//                     Type TEXT NOT NULL,
//                     Size TEXT NOT NULL,
//                     FOREIGN KEY (OrderID) REFERENCES Orders (OrderID)
//                 );
//                 CREATE TABLE IF NOT EXISTS Toppings (
//                     ToppingID INTEGER PRIMARY KEY AUTOINCREMENT,
//                     PizzaID INTEGER NOT NULL,
//                     ToppingName TEXT NOT NULL,
//                     FOREIGN KEY (PizzaID) REFERENCES Pizzas (PizzaID)
//                 );
//             ";
//             command.ExecuteNonQuery();
//             Console.WriteLine("Tables created or verified successfully.");
//         }
//     }
//     catch (Exception ex)
//     {
// // Log any exceptions that occur during initialization
//         Console.WriteLine($"Error Initializing Database: {ex.Message}");
//     }
//         }

//         static int InsertCustomer(Customer customer)
//         {
//             using (var connection = new SQLiteConnection(ConnectionString))
//             {
//                 connection.Open();
//                 var command = connection.CreateCommand();
//                 command.CommandText = "INSERT INTO Customers (Name, Address) VALUES (@Name, @Address); SELECT last_insert_rowid();";
//                 command.Parameters.AddWithValue("@Name", customer.Name);
//                 command.Parameters.AddWithValue("@Address", customer.Address);
//                 return Convert.ToInt32(command.ExecuteScalar());
//             }
//         }

//         static int InsertOrder(int customerId, string status)
//         {
//             using (var connection = new SQLiteConnection(ConnectionString))
//             {
//                 connection.Open();
//                 var command = connection.CreateCommand();
//                 command.CommandText = "INSERT INTO Orders (CustomerID, Status) VALUES (@CustomerID, @Status); SELECT last_insert_rowid();";
//                 command.Parameters.AddWithValue("@CustomerID", customerId);
//                 command.Parameters.AddWithValue("@Status", status);
//                 return Convert.ToInt32(command.ExecuteScalar());
//             }
//         }

//         static int InsertPizza(int orderId, string type, string size)
//         {
//             using (var connection = new SQLiteConnection(ConnectionString))
//             {
//                 connection.Open();
//                 var command = connection.CreateCommand();
//                 command.CommandText = "INSERT INTO Pizzas (OrderID, Type, Size) VALUES (@OrderID, @Type, @Size); SELECT last_insert_rowid();";
//                 command.Parameters.AddWithValue("@OrderID", orderId);
//                 command.Parameters.AddWithValue("@Type", type);
//                 command.Parameters.AddWithValue("@Size", size);
//                 return Convert.ToInt32(command.ExecuteScalar());
//             }
//         }

//         static void InsertTopping(int pizzaId, string topping)
//         {
//             using (var connection = new SQLiteConnection(ConnectionString))
//             {
//                 connection.Open();
//                 var command = connection.CreateCommand();
//                 command.CommandText = "INSERT INTO Toppings (PizzaID, ToppingName) VALUES (@PizzaID, @ToppingName);";
//                 command.Parameters.AddWithValue("@PizzaID", pizzaId);
//                 command.Parameters.AddWithValue("@ToppingName", topping);
//                 command.ExecuteNonQuery();
//             }
//         }

//         static void UpdateOrderStatus(int orderId, string status)
//         {
//             using (var connection = new SQLiteConnection(ConnectionString))
//             {
//                 connection.Open();
//                 var command = connection.CreateCommand();
//                 command.CommandText = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID";
//                 command.Parameters.AddWithValue("@Status", status);
//                 command.Parameters.AddWithValue("@OrderID", orderId);
//                 command.ExecuteNonQuery();
//             }
//         }

//         static bool AskYesNo(string message)
//         {
//             while (true)
//             {
//                 Console.WriteLine(message);
//                 string input = Console.ReadLine()?.ToLower();
//                 if (input == "y") return true;
//                 if (input == "n") return false;

//                 Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please click the supported option ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
//             }
//         }

//      static string ChoosePizza()
//      {
//      while (true)
//           {
//                Console.WriteLine(" \n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Pizza Menu ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n");
//                Console.WriteLine("1. Margherita");
//                Console.WriteLine("2. Pepperoni");
//                Console.WriteLine("3. Hawaiian");
//                Console.WriteLine("4. Neapolitan");
//                Console.WriteLine("5. Meat Lover's");
//                Console.WriteLine("6. Margherita con Funghi");
//                Console.WriteLine("7. BBQ Chicken");
//                Console.WriteLine("8. Sicilian");
//                Console.Write("\nEnter your choice (1-8): \n");

//                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 8)
//                {
//                     return choice switch
//                     {
//                          1 => "Margherita",
//                          2 => "Pepperoni",
//                          3 => "Hawaiian",
//                          4 => "Neapolitan",
//                          5 => "Meat Lover's",
//                          6 => "Margherita con Funghi",
//                          7 => "BBQ Chicken",
//                          8 => "Sicilian",
//                          _ => throw new InvalidOperationException()
//                     };
//                }

//                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please click the supported option ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
//           }
//      }

//         static string ChoosePizzaSize()
//         {
//             while (true)
//             {
//                 Console.WriteLine(" \n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Pizza Size ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n");
//                 Console.WriteLine("1. Xtra Large");
//                 Console.WriteLine("2. Large");
//                 Console.WriteLine("3. Regular");
//                 Console.Write("\nEnter your choice (1/2/3): \n");

//                 if (int.TryParse(Console.ReadLine(), out int choice) && choice is >= 1 and <= 3)
//                 {
//                     return choice switch
//                     {
//                         1 => "Xtra Large",
//                         2 => "Large",
//                         3 => "Regular",
//                         _ => throw new InvalidOperationException()
//                     };
//                 }

//                 Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please click the supported option ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
//             }
//         }

// static List<string> ChooseToppings(string pizzaType)
// {
//     Console.WriteLine($" \n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Do you want to add TOPPINGS to your {pizzaType} pizza? (y/n) ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n");

//     if (!AskYesNo("Enter your choice (y/n): "))
//     {
//         return new List<string>();
//     }

//     var toppings = new List<string>();

//     Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** TOPPINGS ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
//     Console.WriteLine("1. Pepperoni");
//     Console.WriteLine("2. Mushroom");
//     Console.WriteLine("3. Extra cheese");
//     Console.WriteLine("4. Sausage");
//     Console.WriteLine("5. Onion");
//     Console.WriteLine("6. Black olives");
//     Console.WriteLine("7. Green pepper");
//     Console.WriteLine("8. Fresh garlic");
//     Console.WriteLine("\nEnter the topping numbers separated by space (e.g., 1 2 3): \n");

//     string[] toppingChoices = Console.ReadLine()?.Split();

//     if (toppingChoices != null)
//     {
//         foreach (var choice in toppingChoices)
//         {
//             if (int.TryParse(choice, out int toppingNumber) && toppingNumber >= 1 && toppingNumber <= 8)
//             {
//                 string topping = toppingNumber switch
//                 {
//                     1 => "Pepperoni",
//                     2 => "Mushroom",
//                     3 => "Extra cheese",
//                     4 => "Sausage",
//                     5 => "Onion",
//                     6 => "Black olives",
//                     7 => "Green pepper",
//                     8 => "Fresh garlic",
//                     _ => null
//                 };

//                 toppings.Add(topping);
//             }
//             else
//             {
//                 Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Invalid topping number! Please try again. ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
//                 return ChooseToppings(pizzaType); // Re-prompt for valid toppings
//             }
//         }
//     }

//     Console.WriteLine("\nToppings added: " + string.Join(", ", toppings) + "\n");
//     WaitForKey();

//     return toppings;
// }

//         static bool ProcessPayment(Order order)
//         {
//             if (!AskYesNo("\n \n 💰💰💰 Do you want to pay? (y/n) 💰💰💰 \n  \n"))
//             {
//                 order.CancelOrder("Payment declined");
//                 return false;
//             }

//             if (!AskYesNo("\n \n 💰 💁‍♀️ Is payment successful? (y/n) 💰 💁 \n \n "))
//             {
//                 Console.WriteLine(" \n ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Unsuccessful payment ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n");
//                 order.CancelOrder("Unsuccessful payment");
//                 return false;
//             }

//             Console.WriteLine(" \n ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n***👨‍🦰 Successful payment 👩 ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ \n  \n ");
//             WaitForKey();
//             return true;
//         }

//         static void WaitForKey()
//         {
//             Console.WriteLine("➡️➡️➡️➡️➡️➡️CLICK  ANY BUTTON TO CONTINUE ➡️➡️➡️➡️➡️➡️");
//             Console.ReadKey();
//         }
//     }

//     class Customer
//     {
//         public string Name { get; set; }
//         public string Address { get; set; }

//         public Customer(string name, string address)
//         {
//             Name = name;
//             Address = address;
//         }
//     }

//     class Pizza
//     {
//         public string Type { get; set; }
//         public string Size { get; set; }
//         public List<string> Toppings { get; private set; } = new List<string>();

//         public Pizza(string type, string size)
//         {
//             Type = type;
//             Size = size;
//         }

//         public void AddToppings(List<string> toppings)
//         {
//             Toppings.AddRange(toppings);
//         }
//     }

//     class Order
//     {
//         public int CustomerId { get; }
//         public List<Pizza> Pizzas { get; private set; } = new List<Pizza>();
//         public string Status { get; private set; } = "Cancelled: Unsuccessful Payment";

//         public Order(int customerId)
//         {
//             CustomerId = customerId;
//         }

//         public void AddPizza(Pizza pizza)
//         {
//             Pizzas.Add(pizza);
//         }

//         public void CompleteOrder()
//         {
//             Status = "Completed";
//         }

//         public void CancelOrder(string reason)
//         {
//             Status = $"Cancelled: {reason}";
//         }
//     }

//     class DeliveryPerson
//     {
//         private static readonly string[] Names = { "Driver 1", "Driver 2", "Driver 3", "Driver 4" };
//         public string Name { get; private set; }

//         private DeliveryPerson(string name)
//         {
//             Name = name;
//         }

//         public static DeliveryPerson AssignDriver()
//         {
//             Random random = new Random();
//             string name = Names[random.Next(Names.Length)];
//             return new DeliveryPerson(name);
//         }
//     }
// }
