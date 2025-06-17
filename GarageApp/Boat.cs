using System;


namespace GarageApp;

public class Boat : Vehicle
{
    public int Length { get; set; }

    public Boat(string regNumber, string color, int length)
        : base(regNumber, color, wheels: 0)
    {
        Length = length;
    }

}
