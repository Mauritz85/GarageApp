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
        int size = Ask.ForInt("Hur många platser ska garaget ha? ");
        handler = new GarageHandler(size);

        handler.ParkVehicle(new Car("ABC123", "Blå", "Bensin"));
        handler.ParkVehicle(new MotorCycle("ABC124", "Grön", true));
        handler.ParkVehicle(new Bus("CBA", "Blå", 124));

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nHUVUDMENYN");
            Console.WriteLine("\n1. Parkera fordon\n2. Hämta fordon\n3. Lista parkerade fordon\n4. Sök bland parkerade fordon\n5. Sök efter registreringsnummer\n0. Avsluta");
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
                    ListAllVehiclesAndTypes();
                    break;
                case "4":
                    SearchVehicles();
                    break;
                case "5":
                    SearchForRegNumber();
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

        string choice = Ask.ForChoice();

        switch (choice)
        {

            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
                string regNumber = Ask.ForRegNumber("Vänligen ange regnummer (format: ABC123): ");
                string color = Ask.ForString("Färg: ");
                Vehicle? vehicle = choice switch
                {
                    "1" => new Car(regNumber, color, Ask.ForString("Bränsletyp (t.ex. Bensin/Diesel): ")),
                    "2" => new MotorCycle(regNumber, color, Ask.ForBool("Har sidovagn? (j/n): ")),
                    "3" => new Airplane(regNumber, color, Ask.ForInt("Antal motorer: ")),
                    "4" => new Boat(regNumber, color, Ask.ForInt("Längd i meter: ")),
                    "5" => new Bus(regNumber, color, Ask.ForInt("Antal säten: ")),
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
        Console.WriteLine("\nHÄMTA FORDON");
        string regNumber = Ask.ForRegNumber("Vänligen ange registreringsnummer på bil som ska hämtas: ");
        if (handler.RemoveVehicle(regNumber))
        {
            Console.WriteLine($"Fordon med registreringsnummer {regNumber} har nu lämnat garaget.");
        }
        else
        {
            Console.WriteLine($"Inget fordon med registreringsnummer {regNumber} hittades.");
        }
    }


    public void ListAllVehiclesAndTypes()
    {
        var vehicles = handler.GetVehicles();
        if (!vehicles.Any())
        {
            Console.WriteLine("Inga fordon parkerade.");
            return;
        }
        Console.WriteLine("\nLista på fordon:");
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine($"{Translate.EngToSwe(vehicle.GetType().Name)} - {vehicle.RegistrationNumber} - Färg: {vehicle.Color} - Antal hjul: {vehicle.NumberOfWheels}");
        }

        var typeCounts = handler.GetVehicleTypeCounts();
        Console.WriteLine("\nSummering:");
        foreach (var (type, count) in typeCounts)
        {
            Console.WriteLine($"{Translate.EngToSwe(type)}: {count}");
        }
    }

    public void SearchVehicles()
    {
        Console.WriteLine("\n--- Sök fordon ---");
        string? type = Ask.ForStringOrNull("Fordonstyp (t.ex. Bil, Båt), lämna tomt för att ignorera: ");
        string? color = Ask.ForStringOrNull("Färg, lämna tomt för att ignorera: ");
        int? wheels = Ask.ForIntOrNull("Antal hjul, lämna tomt för att ignorera: ");
       
        var results = handler.SearchVehicles(
            type,
            color,
            wheels
        );

        if (!results.Any())
            Console.WriteLine("Inga fordon matchade sökkriterierna.");
        else
        {
            Console.WriteLine("\nMatchande fordon:");
            foreach (var vehicle in results)    
                Console.WriteLine($"{Translate.EngToSwe(vehicle.GetType().Name)} - {vehicle.RegistrationNumber} - Färg: {vehicle.Color} - Antal hjul: {vehicle.NumberOfWheels}");
        }
    }

    public void SearchForRegNumber()
    {
        Console.WriteLine("\n--- Sök fordon med registreringsnummer ---");
        string? regNumber = Ask.ForRegNumber("Fyll i registreringsnummer (Format: ABC123): ");

        var results = handler.SearchRegNumber(
            regNumber
        );

        if (!results.Any())
            Console.WriteLine("Inga fordon matchade sökkriterierna.");
        else
        {
            Console.WriteLine("\nMatchande fordon:");
            foreach (var vehicle in results)
                Console.WriteLine($"{Translate.EngToSwe(vehicle.GetType().Name)} - {vehicle.RegistrationNumber} - Färg: {vehicle.Color} - Antal hjul: {vehicle.NumberOfWheels}");
        }
    }






}


