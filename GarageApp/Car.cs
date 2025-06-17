using System;


namespace GarageApp;

public class Car : Vehicle
{
    public string FuelType { get; set; }

    public Car(string regNumber, string color, string fuelType)
        : base(regNumber, color, wheels: 4)
    {
        FuelType = fuelType;
    }

}
