using GarageApp.Handlers;
using System;

namespace GarageApp.Helpers;

internal static class GarageSeeder
{
    public static void SeedData(GarageHandler handler, int capacity)
    {
        Vehicle[] initVehicles = new Vehicle[]
        {
            new Car("ABC123", "Blå", "Bensin"),
            new MotorCycle("ABC123", "Vit", false),
            new Airplane("VWX555", "Blå", 4),
            new Boat("BCD777", "Grön", 12),
            new Bus("NOP111", "Gul", 40),
            new Car("DEF456", "Röd", "Diesel"),
            new Car("GHI789", "Grön", "El"),
            new MotorCycle("JKL111", "Svart", true),
            new MotorCycle("PQR333", "Grå", true),
            new Airplane("STU444", "Vit", 2),
            new Airplane("YZA666", "Silver", 6),          
            new Boat("EFG888", "Blå", 20),
            new Boat("HIJ999", "Vit", 15),
            new Bus("KLM000", "Röd", 50),      
            new Bus("QRS222", "Blå", 35),
            new Car("TUV333", "Orange", "Hybrid"),
            new MotorCycle("WXY444", "Lila", false),
            new Airplane("ZAB555", "Svart", 1),
            new Boat("CDE666", "Marinblå", 10),
            new Bus("FGH777", "Silver", 60)
        };

        int maximumInit = capacity <= 20 ? capacity : 20;
        while (true)
        {
            
            Console.Write($"Hur många fordon vill du initiera med? (Max {maximumInit}): ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int initCount) &&
                initCount >= 0 &&
                initCount <= 20 &&
                initCount <= capacity)
            {
                for (int i = 0; i < initCount; i++)
                {
                    handler.ParkVehicle(initVehicles[i]);
                }

                Console.WriteLine($"{initCount} fordon har lagts till i garaget.");
                break;
            }
            else
            {
                if (initCount > capacity)
                    Console.WriteLine("Felaktig inmatning. Du kan inte ha fler än bilar än garagets kapacitet.");
                else Console.WriteLine("Felaktig inmatning. Du kan förparkera max 20 bilar.");
            }

        }
    }
}
