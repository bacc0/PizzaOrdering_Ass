using Xunit;
using PizzaOrderingSystem.Models; // Import the namespace for your models
using System.Collections.Generic;

namespace PizzaOrderingSystem.Tests
{
    public class PizzaTests
    {
        [Fact]
        public void Pizza_Should_Store_Name_Correctly()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = "Margherita",
                Size = "Medium",
                Price = 8.99M,
                Toppings = new List<string> { "Cheese", "Tomato" },
                IsVegetarian = true
            };

            // Act
            var name = pizza.Name;

            // Assert
            Assert.Equal("Margherita", name);
        }

        [Fact]
        public void Pizza_Should_Store_Size_Correctly()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = "Pepperoni",
                Size = "Large",
                Price = 12.99M,
                Toppings = new List<string> { "Cheese", "Pepperoni" },
                IsVegetarian = false
            };

            // Act
            var size = pizza.Size;

            // Assert
            Assert.Equal("Large", size);
        }

        [Fact]
        public void Pizza_Should_Store_Price_Correctly()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = "Hawaiian",
                Size = "Small",
                Price = 6.99M,
                Toppings = new List<string> { "Cheese", "Ham", "Pineapple" },
                IsVegetarian = false
            };

            // Act
            var price = pizza.Price;

            // Assert
            Assert.Equal(6.99M, price);
        }

        [Fact]
        public void Pizza_Should_Store_Toppings_Correctly()
        {
            // Arrange
            var toppings = new List<string> { "Cheese", "Tomato", "Basil" };
            var pizza = new Pizza
            {
                Name = "Margherita",
                Size = "Medium",
                Price = 8.99M,
                Toppings = toppings,
                IsVegetarian = true
            };

            // Act
            var actualToppings = pizza.Toppings;

            // Assert
            Assert.Equal(toppings, actualToppings);
        }

        [Fact]
        public void Pizza_Should_Store_IsVegetarian_Correctly()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = "Vegetarian",
                Size = "Large",
                Price = 10.99M,
                Toppings = new List<string> { "Cheese", "Tomato", "Peppers", "Olives" },
                IsVegetarian = true
            };

            // Act
            var isVegetarian = pizza.IsVegetarian;

            // Assert
            Assert.True(isVegetarian);
        }

        [Fact]
        public void Pizza_Should_Allow_Empty_Toppings()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = "Plain",
                Size = "Medium",
                Price = 5.99M,
                Toppings = new List<string>(),
                IsVegetarian = true
            };

            // Act
            var toppings = pizza.Toppings;

            // Assert
            Assert.Empty(toppings);
        }

        [Fact]
        public void Pizza_Should_Allow_Null_Toppings()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = "Cheese Pizza",
                Size = "Medium",
                Price = 7.99M,
                Toppings = null,
                IsVegetarian = true
            };

            // Act
            var toppings = pizza.Toppings;

            // Assert
            Assert.Null(toppings);
        }

        [Fact]
        public void Pizza_Should_Allow_Null_Name()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = null,
                Size = "Small",
                Price = 4.99M,
                Toppings = new List<string> { "Cheese" },
                IsVegetarian = true
            };

            // Act
            var name = pizza.Name;

            // Assert
            Assert.Null(name);
        }

        [Fact]
        public void Pizza_Should_Allow_Empty_Name()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = string.Empty,
                Size = "Medium",
                Price = 5.99M,
                Toppings = new List<string> { "Cheese", "Tomato" },
                IsVegetarian = true
            };

            // Act
            var name = pizza.Name;

            // Assert
            Assert.Equal(string.Empty, name);
        }

        [Fact]
        public void Pizza_Should_Allow_Empty_Size()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = "Custom Pizza",
                Size = string.Empty,
                Price = 9.99M,
                Toppings = new List<string> { "Cheese", "Tomato", "Mushroom" },
                IsVegetarian = true
            };

            // Act
            var size = pizza.Size;

            // Assert
            Assert.Equal(string.Empty, size);
        }

        [Fact]
        public void Pizza_Should_Allow_Null_Size()
        {
            // Arrange
            var pizza = new Pizza
            {
                Name = "Custom Pizza",
                Size = null,
                Price = 9.99M,
                Toppings = new List<string> { "Cheese", "Tomato", "Mushroom" },
                IsVegetarian = true
            };

            // Act
            var size = pizza.Size;

            // Assert
            Assert.Null(size);
        }
    }
}
