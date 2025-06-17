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
            Console.WriteLine("\nHUVUDMENYN");
            Console.WriteLine("\n1. Parkera fordon\n2. Hämta fordon\n3. Lista fordon\n0. Avsluta");
            Console.Write("Ditt val: ");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ParkVehicle();
                    break;
                case "2":
                    RemoveVehicle();
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
        Console.WriteLine("\nPARKERA FORDON");
        Console.WriteLine("\nVälj fordonstyp att parkera:");
        Console.WriteLine("1. Bil");
        Console.WriteLine("2. Motorcykel");
        Console.WriteLine("3. Flygplan");
        Console.WriteLine("4. Båt");
        Console.WriteLine("5. Buss");
        Console.Write("Ditt val: ");

        string choice = AskHelpers.AskChoice();

        switch (choice)
        {

            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
                string regNumber = AskHelpers.AskRegNumber("Vänligen ange regnummer (format: ABC123): ");
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
                Console.WriteLine("Något gick fel. Tillbaka till huvudmenyn");
                break;
        }

    }

    public void RemoveVehicle()
    {
        Console.WriteLine("\nPARKERA FORDON");
        string regNumber = AskHelpers.AskRegNumber("Vänligen ange registreringsnummer på bil som ska hämtas: ");
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


