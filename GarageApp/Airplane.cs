using System;


namespace GarageApp;

public class Airplane : Vehicle
{
    public int NumberOfEngines { get; set; }

    public Airplane(string regNumber, string color, int numberOfEngines)
        : base(regNumber, color, wheels: 3)
    {
        NumberOfEngines = numberOfEngines;
    }

}
