using System;


namespace GarageApp;

public abstract class Vehicle: IVehicle
{
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public int NumberOfWheels { get; set; }
    protected Vehicle(string regNumber, string color, int wheels)
    {
        RegistrationNumber = regNumber.ToUpper();
        Color = color;
        NumberOfWheels = wheels;
    }
}
