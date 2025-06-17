using GarageApp.Handlers;
using GarageApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.UI;

public class ConsoleUI : IUI
{
    private GarageHandler handler;

    public void Start()
    {
        Console.WriteLine("Välkommen till Garaget!");
        int size = AskHelpers.AskInt("Hur många platser ska garaget ha? ");
        handler = new GarageHandler(size);

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n1. Parkera fordon\n2. Lista fordon\n0. Avsluta");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ParkVehicle();
                    break;
                case "2":
                    
                    break;
                case "3":
                    ListVehicles();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Felaktig inmatning. Försök igen.");
                    break;
            }
        }
    }

    public void ParkVehicle()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nVälj fordonstyp att parkera:");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorcycle");
            Console.WriteLine("3. Airplane");
            Console.WriteLine("4. Boat");
            Console.WriteLine("5. Bus");
            Console.WriteLine("0. Gå tillbaka till huvudmenyn");
            Console.Write("Ditt val: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    running = false;
                    Console.WriteLine("Återgår till huvudmenyn...");
                    break;

                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                    string regNumber = AskHelpers.AskRegNumber();
                    string color = AskHelpers.AskString("Färg: ");
                    Vehicle? vehicle = choice switch
                    {
                        "1" => new Car(regNumber, color, AskHelpers.AskString("Bränsletyp (t.ex. Bensin/Diesel): ")),
                        "2" => new MotorCycle(regNumber, color, AskHelpers.AskBool("Har sidovagn? (j/n): ")),
                        "3" => new Airplane(regNumber, color, AskHelpers.AskInt("Antal motorer: ")),
                        "4" => new Boat(regNumber, color, AskHelpers.AskInt("Längd i meter: ")),
                        "5" => new Bus(regNumber, color, AskHelpers.AskInt("Antal säten: ")),
                        _ => null
                    };

                    if (vehicle != null)
                    {
                        var result = handler.ParkVehicle(vehicle);

                        switch (result)
                        {
                            case ParkVehicleFeedback.Success:
                                Console.WriteLine("Fordonet är nu parkerat!");
                                break;
                            case ParkVehicleFeedback.DuplicateRegNumber:
                                Console.WriteLine("Det finns redan ett fordon med samma registreringsnummer parkerat.");
                                break;
                            case ParkVehicleFeedback.GarageFull:
                                Console.WriteLine("Garaget är fullt. Försök igen senare");
                                break;
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Felaktigt val. Ange en siffra mellan 0 och 5.");
                    break;
            }
        }
    }

    public void RemoveVehicle()
    {
        string regNumber = AskHelpers.AskRegNumber();
        if (handler.RemoveVehicle(regNumber))
        {
            Console.WriteLine($"Fordon med registreringsnummer {regNumber} togs bort.");
        }
        else
        {
            Console.WriteLine($"Inget fordon med registreringsnummer {regNumber} hittades.");
        }
    }


    private void ListVehicles()
    {
        foreach (var vehicle in handler.ListVehicles())
        {
            Console.WriteLine($"{vehicle.GetType().Name} - {vehicle.RegistrationNumber}");
        }
    }
}


