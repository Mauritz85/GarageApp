using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Helpers
{
    internal class Translate
    {
        public static string EngToSwe(string typeName) => typeName switch
        {
            "Car" => "Bil",
            "MotorCycle" => "Motorcykel",
            "Boat" => "Båt",
            "Bus" => "Buss",
            "Airplane" => "Flygplan",
            _ => typeName
        };

        public static string SweToEng(string typeName) => typeName.Trim().ToLowerInvariant() switch
        {
            "bil" => "Car",
            "motorcykel" => "MotorCycle",
            "båt" => "Boat",
            "buss" => "Bus",
            "flygplan" => "Airplane",
            _ => typeName
        };

    }
}
