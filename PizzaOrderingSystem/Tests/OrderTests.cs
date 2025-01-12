using Xunit;
using PizzaOrderingSystem.Models; // Import the namespace for your models
using System.Collections.Generic;

namespace PizzaOrderingSystem.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Order_Should_Store_OrderId_Correctly()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                Customer = new Customer { Name = "John Doe", Email = "john.doe@example.com" },
                DeliveryPerson = new DeliveryPerson { Name = "Michael Scott", Email = "michael.scott@example.com" },
                OrderItems = new List<OrderItem>(),
                TotalPrice = 29.99M
            };

            // Act
            var orderId = order.OrderId;

            // Assert
            Assert.Equal(1, orderId);
        }

        [Fact]
        public void Order_Should_Store_Customer_Correctly()
        {
            // Arrange
            var customer = new Customer { Name = "Jane Doe", Email = "jane.doe@example.com" };
            var order = new Order
            {
                OrderId = 2,
                Customer = customer,
                DeliveryPerson = new DeliveryPerson { Name = "Dwight Schrute", Email = "dwight.schrute@example.com" },
                OrderItems = new List<OrderItem>(),
                TotalPrice = 19.99M
            };

            // Act
            var actualCustomer = order.Customer;

            // Assert
            Assert.Equal(customer, actualCustomer);
        }

        [Fact]
        public void Order_Should_Store_DeliveryPerson_Correctly()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson { Name = "Pam Beesly", Email = "pam.beesly@example.com" };
            var order = new Order
            {
                OrderId = 3,
                Customer = new Customer { Name = "Jim Halpert", Email = "jim.halpert@example.com" },
                DeliveryPerson = deliveryPerson,
                OrderItems = new List<OrderItem>(),
                TotalPrice = 49.99M
            };

            // Act
            var actualDeliveryPerson = order.DeliveryPerson;

            // Assert
            Assert.Equal(deliveryPerson, actualDeliveryPerson);
        }

        [Fact]
        public void Order_Should_Store_OrderItems_Correctly()
        {
            // Arrange
            var orderItems = new List<OrderItem>
            {
                new OrderItem { Name = "Pepperoni Pizza", Quantity = 1, Price = 15.99M },
                new OrderItem { Name = "Garlic Bread", Quantity = 2, Price = 5.00M }
            };
            var order = new Order
            {
                OrderId = 4,
                Customer = new Customer { Name = "Angela Martin", Email = "angela.martin@example.com" },
                DeliveryPerson = new DeliveryPerson { Name = "Oscar Martinez", Email = "oscar.martinez@example.com" },
                OrderItems = orderItems,
                TotalPrice = 25.99M
            };

            // Act
            var actualOrderItems = order.OrderItems;

            // Assert
            Assert.Equal(orderItems, actualOrderItems);
        }

        [Fact]
        public void Order_Should_Store_TotalPrice_Correctly()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 5,
                Customer = new Customer { Name = "Stanley Hudson", Email = "stanley.hudson@example.com" },
                DeliveryPerson = new DeliveryPerson { Name = "Kevin Malone", Email = "kevin.malone@example.com" },
                OrderItems = new List<OrderItem>(),
                TotalPrice = 39.99M
            };

            // Act
            var totalPrice = order.TotalPrice;

            // Assert
            Assert.Equal(39.99M, totalPrice);
        }

        [Fact]
        public void Order_Should_Allow_Empty_OrderItems()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 6,
                Customer = new Customer { Name = "Creed Bratton", Email = "creed.bratton@example.com" },
                DeliveryPerson = new DeliveryPerson { Name = "Andy Bernard", Email = "andy.bernard@example.com" },
                OrderItems = new List<OrderItem>(),
                TotalPrice = 0M
            };

            // Act
            var orderItems = order.OrderItems;

            // Assert
            Assert.Empty(orderItems);
        }

        [Fact]
        public void Order_Should_Allow_Null_Customer()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 7,
                Customer = null,
                DeliveryPerson = new DeliveryPerson { Name = "Erin Hannon", Email = "erin.hannon@example.com" },
                OrderItems = new List<OrderItem>(),
                TotalPrice = 0M
            };

            // Act
            var customer = order.Customer;

            // Assert
            Assert.Null(customer);
        }

        [Fact]
        public void Order_Should_Allow_Null_DeliveryPerson()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 8,
                Customer = new Customer { Name = "Ryan Howard", Email = "ryan.howard@example.com" },
                DeliveryPerson = null,
                OrderItems = new List<OrderItem>(),
                TotalPrice = 15.99M
            };

            // Act
            var deliveryPerson = order.DeliveryPerson;

            // Assert
            Assert.Null(deliveryPerson);
        }
    }
}
