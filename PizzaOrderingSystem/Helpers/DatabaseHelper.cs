using System.Data.SQLite;

namespace PizzaOrderingSystem.Helpers;

public static class DatabaseHelper
{
    public static readonly string ConnectionString = "Data Source=PizzaOrderingSystem.db;Version=3;";

    public static void InitializeDatabase()
    {
        try
        {
            // Check if database file exists
            if (!File.Exists("PizzaOrderingSystem.db"))
            {
                SQLiteConnection.CreateFile("PizzaOrderingSystem.db");
                Console.WriteLine("\n \n 💿 Database file 'PizzaOrderingSystem.db' created successfully.");
            }
            else
            {
                Console.WriteLine("\n \n 💿 Database file 'PizzaOrderingSystem.db' already exists.");
            }

            // Open connection and create tables if they do not exist
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Customers (
                            CustomerID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Address TEXT NOT NULL
                        );
                        CREATE TABLE IF NOT EXISTS Orders (
                            OrderID INTEGER PRIMARY KEY AUTOINCREMENT,
                            CustomerID INTEGER NOT NULL,
                            Status TEXT NOT NULL,
                            FOREIGN KEY (CustomerID) REFERENCES Customers (CustomerID)
                        );
                        CREATE TABLE IF NOT EXISTS Pizzas (
                            PizzaID INTEGER PRIMARY KEY AUTOINCREMENT,
                            OrderID INTEGER NOT NULL,
                            Type TEXT NOT NULL,
                            Size TEXT NOT NULL,
                            FOREIGN KEY (OrderID) REFERENCES Orders (OrderID)
                        );
                        CREATE TABLE IF NOT EXISTS Toppings (
                            ToppingID INTEGER PRIMARY KEY AUTOINCREMENT,
                            PizzaID INTEGER NOT NULL,
                            ToppingName TEXT NOT NULL,
                            FOREIGN KEY (PizzaID) REFERENCES Pizzas (PizzaID)
                        );
                    ";

                command.ExecuteNonQuery();
                Console.WriteLine("\n \n 📊 Tables created or verified successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Initializing Database: {ex.Message}");
        }
    }

    public static int InsertOrder(int customerId, string status)
    {
        try
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Orders (CustomerID, Status) VALUES (@CustomerID, @Status); SELECT last_insert_rowid();";
                command.Parameters.AddWithValue("@CustomerID", customerId);
                command.Parameters.AddWithValue("@Status", status);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting order: {ex.Message}");
            return -1;
        }
    }

    public static int InsertPizza(int orderId, string type, string size)
    {
        try
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Pizzas (OrderID, Type, Size) VALUES (@OrderID, @Type, @Size); SELECT last_insert_rowid();";
                command.Parameters.AddWithValue("@OrderID", orderId);
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@Size", size);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting pizza: {ex.Message}");
            return -1;
        }
    }

    public static void InsertTopping(int pizzaId, string toppingName)
    {
        try
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Toppings (PizzaID, ToppingName) VALUES (@PizzaID, @ToppingName);";
                command.Parameters.AddWithValue("@PizzaID", pizzaId);
                command.Parameters.AddWithValue("@ToppingName", toppingName);

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting topping: {ex.Message}");
        }
    }

    public static void UpdateOrderStatus(int orderId, string status)
    {
        try
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID;";
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@OrderID", orderId);

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating order status: {ex.Message}");
        }
    }
}

