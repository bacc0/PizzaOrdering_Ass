using Xunit;
using PizzaOrderingSystem.Models; // Import the namespace for your models

namespace PizzaOrderingSystem.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_Should_Store_Name_Correctly()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var name = customer.Name;

            // Assert
            Assert.Equal("John Doe", name);
        }

        [Fact]
        public void Customer_Should_Store_Email_Correctly()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com"
            };

            // Act
            var email = customer.Email;

            // Assert
            Assert.Equal("jane.doe@example.com", email);
        }

        [Fact]
        public void Customer_Should_Allow_Null_Name()
        {
            // Arrange
            var customer = new Customer
            {
                Name = null,
                Email = "test@example.com"
            };

            // Act
            var name = customer.Name;

            // Assert
            Assert.Null(name);
        }

        [Fact]
        public void Customer_Should_Allow_Null_Email()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Email = null
            };

            // Act
            var email = customer.Email;

            // Assert
            Assert.Null(email);
        }

        [Fact]
        public void Customer_Should_Allow_Empty_Name()
        {
            // Arrange
            var customer = new Customer
            {
                Name = string.Empty,
                Email = "test@example.com"
            };

            // Act
            var name = customer.Name;

            // Assert
            Assert.Equal(string.Empty, name);
        }

        [Fact]
        public void Customer_Should_Allow_Empty_Email()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Email = string.Empty
            };

            // Act
            var email = customer.Email;

            // Assert
            Assert.Equal(string.Empty, email);
        }
    }
}
