using System;


namespace GarageApp;

public class MotorCycle : Vehicle
{
    public bool SideCar  { get; set; }

    public MotorCycle(string regNumber, string color, bool sideCar)
        : base(regNumber, color, wheels: 2)
    {
       SideCar = sideCar;
    }

}
