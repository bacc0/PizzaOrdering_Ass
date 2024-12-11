using System.Data.SQLite;
using PizzaOrderingSystem.Helpers;

namespace PizzaOrderingSystem.Models;

public class Customer
{
    public string Name { get; set; }
    public string Address { get; set; }


    // Methods Gather customer information
    public void Input()
    {
        Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please add your name ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        Name = Console.ReadLine();

        Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n*** Please add your address ***\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        Address = Console.ReadLine();
    }

    // Methods Save customer information to the database
    public int Save()
    {
        try
        {
            using (var connection = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Customers (Name, Address) VALUES (@Name, @Address); SELECT last_insert_rowid();";
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Address", Address);

                // Convert the result to an integer
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting customer: {ex.Message}");
            return -1; // Return -1 to indicate failure
        }
    }
    // Methods Print customer information (Welcome message)
    public void PrintWelcome()
    {
        Console.WriteLine(ArtAssets.PersonalWelcome);
        Console.WriteLine($"\n  \n   Welcome, {Name.ToUpper()} from ( {Address} )!    \n   \n  \n");

    }
}

