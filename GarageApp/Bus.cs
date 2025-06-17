using System;


namespace GarageApp;

public class Bus : Vehicle
{
    public int NumberOfSeats { get; set; }

    public Bus(string regNumber, string color, int numberOfSeats)
        : base(regNumber, color, wheels: 0)
    {
        NumberOfSeats = numberOfSeats;
    }

}
