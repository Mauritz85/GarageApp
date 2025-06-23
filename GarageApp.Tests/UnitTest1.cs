using GarageApp;
using GarageApp.Handlers;
using Xunit;

namespace GarageApp.Tests;

public class GarageTests
{
    [Fact]
    public void AddVehicle_NullVehicle_ReturnsInvalidVehicle()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);

        // Act
        var result = garage.AddVehicle(null!);

        // Assert
        Assert.Equal(ParkVehicleFeedback.InvalidVehicle, result);
    }

    [Fact]
    public void AddVehicle_WhenGarageIsFull_ReturnsGarageFull()
    {
        // Arrange
        var garage = new Garage<Vehicle>(1);
        var car = new Car("ABC123", "Blå", "Bensin");
        garage.AddVehicle(car);

        // Act
        var result = garage.AddVehicle(new Car("DEF456", "Röd", "Diesel"));

        // Assert
        Assert.Equal(ParkVehicleFeedback.GarageFull, result);
    }

    [Fact]
    public void AddVehicle_DuplicateRegistration_ReturnsDuplicateRegNumber()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);
        var car = new Car("ABC123", "Blå", "Bensin");
        garage.AddVehicle(car);

        // Act
        var result = garage.AddVehicle(new Car("abc123", "Röd", "Diesel"));

        // Assert
        Assert.Equal(ParkVehicleFeedback.DuplicateRegNumber, result);
    }

    [Fact]
    public void AddVehicle_ValidCar_ReturnsSuccess()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);
        var car = new Car("ABC123", "Blå", "Bensin");

        // Act
        var result = garage.AddVehicle(car);

        // Assert
        Assert.Equal(ParkVehicleFeedback.Success, result);
        Assert.Equal(1, garage.Count);
    }

    [Fact]
    public void RemoveVehicle_ExistingVehicle_ReturnsTrueAndDecreasesCount()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);
        var car = new Car("ABC123", "Blå", "Bensin");
        garage.AddVehicle(car);

        // Act
        var removed = garage.RemoveVehicle("ABC123");

        // Assert
        Assert.True(removed);
        Assert.Equal(0, garage.Count);
    }

    [Fact]
    public void RemoveVehicle_NonExistentVehicle_ReturnsFalse()
    {
        // Arrange
        var garage = new Garage<Vehicle>(1);

        // Act
        var removed = garage.RemoveVehicle("XYZ999");

        // Assert
        Assert.False(removed);
    }

    [Fact]
    public void Garage_Enumerator_ReturnsAllVehicles()
    {
        // Arrange
        var garage = new Garage<Vehicle>(2);
        var car = new Car("ABC123", "Blå", "Bensin");
        var bike = new MotorCycle("DEF456", "Röd", false);
        garage.AddVehicle(car);
        garage.AddVehicle(bike);

        // Act
        var vehicles = garage.ToList();

        // Assert
        Assert.Contains(car, vehicles);
        Assert.Contains(bike, vehicles);
        Assert.Equal(2, vehicles.Count);
    }
}
public class GarageHandlerTests
    {
        private GarageHandler? handler;

    [Fact]
        public void AddVehicle_WithInvalidRegNumber_InvalidVehicle()
        {
            // Arrange
            handler = new GarageHandler(5);
            var car = new Car("ABC12", "Blå", "Bensin");
            
            // Act
            var result = handler.ParkVehicle(car);

            // Assert
            Assert.Equal(ParkVehicleFeedback.InvalidVehicle, result);
        }

    }


