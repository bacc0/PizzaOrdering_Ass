using Xunit;
using PizzaOrderingSystem.Models; // Import the namespace for your models

namespace PizzaOrderingSystem.Tests
{
    public class DeliveryPersonTests
    {
        [Fact]
        public void DeliveryPerson_Should_Store_Name_Correctly()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson
            {
                Name = "Michael Scott",
                Email = "michael.scott@example.com",
                PhoneNumber = "123-456-7890"
            };

            // Act
            var name = deliveryPerson.Name;

            // Assert
            Assert.Equal("Michael Scott", name);
        }

        [Fact]
        public void DeliveryPerson_Should_Store_Email_Correctly()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson
            {
                Name = "Dwight Schrute",
                Email = "dwight.schrute@example.com",
                PhoneNumber = "987-654-3210"
            };

            // Act
            var email = deliveryPerson.Email;

            // Assert
            Assert.Equal("dwight.schrute@example.com", email);
        }

        [Fact]
        public void DeliveryPerson_Should_Store_PhoneNumber_Correctly()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson
            {
                Name = "Jim Halpert",
                Email = "jim.halpert@example.com",
                PhoneNumber = "555-123-4567"
            };

            // Act
            var phoneNumber = deliveryPerson.PhoneNumber;

            // Assert
            Assert.Equal("555-123-4567", phoneNumber);
        }

        [Fact]
        public void DeliveryPerson_Should_Allow_Null_Name()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson
            {
                Name = null,
                Email = "test@example.com",
                PhoneNumber = "555-987-6543"
            };

            // Act
            var name = deliveryPerson.Name;

            // Assert
            Assert.Null(name);
        }

        [Fact]
        public void DeliveryPerson_Should_Allow_Null_Email()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson
            {
                Name = "Pam Beesly",
                Email = null,
                PhoneNumber = "555-222-3333"
            };

            // Act
            var email = deliveryPerson.Email;

            // Assert
            Assert.Null(email);
        }

        [Fact]
        public void DeliveryPerson_Should_Allow_Null_PhoneNumber()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson
            {
                Name = "Stanley Hudson",
                Email = "stanley.hudson@example.com",
                PhoneNumber = null
            };

            // Act
            var phoneNumber = deliveryPerson.PhoneNumber;

            // Assert
            Assert.Null(phoneNumber);
        }

        [Fact]
        public void DeliveryPerson_Should_Allow_Empty_Name()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson
            {
                Name = string.Empty,
                Email = "test@example.com",
                PhoneNumber = "123-456-7890"
            };

            // Act
            var name = deliveryPerson.Name;

            // Assert
            Assert.Equal(string.Empty, name);
        }

        [Fact]
        public void DeliveryPerson_Should_Allow_Empty_Email()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson
            {
                Name = "John Doe",
                Email = string.Empty,
                PhoneNumber = "987-654-3210"
            };

            // Act
            var email = deliveryPerson.Email;

            // Assert
            Assert.Equal(string.Empty, email);
        }

        [Fact]
        public void DeliveryPerson_Should_Allow_Empty_PhoneNumber()
        {
            // Arrange
            var deliveryPerson = new DeliveryPerson
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                PhoneNumber = string.Empty
            };

            // Act
            var phoneNumber = deliveryPerson.PhoneNumber;

            // Assert
            Assert.Equal(string.Empty, phoneNumber);
        }
    }
}
