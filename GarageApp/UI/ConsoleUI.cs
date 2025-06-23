using GarageApp.Handlers;
using GarageApp.Helpers;


namespace GarageApp.UI;

public class ConsoleUI : IUI
{
    private GarageHandler handler;

    public void Start()
    {
        Console.WriteLine("Välkommen till Garaget!");
        
        int capacity = Ask.ForInt("Hur många platser ska garaget ha? ");
        handler = new GarageHandler(capacity);
        Console.WriteLine($"Ett garage med {capacity} platser har skapats.");

        GarageSeeder.SeedData(handler, capacity);

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
        Console.WriteLine("\nVälj fordonstyp att parkera:");
        Console.WriteLine("1. Bil\n2. Motorcykel\n3. Flygplan\n4. Båt\n5. Buss");
        Console.Write("Ditt val: ");
        string choice = Ask.ForChoice();

        Vehicle? vehicle = CreateVehicleFromUser(choice);
        if (vehicle is null)
        {
            Console.WriteLine("Ogiltigt val.");
            return;
        }

        var result = handler.ParkVehicle(vehicle);
        switch (result)
        {
            case ParkVehicleFeedback.Success:
                Console.WriteLine("Fordonet är nu parkerat!");
                break;
            case ParkVehicleFeedback.DuplicateRegNumber:
                Console.WriteLine("\nParkering misslyckades. Det finns redan ett fordon med detta registreringsnummer i garaget.");
                break;
            case ParkVehicleFeedback.GarageFull:
                Console.WriteLine("Garaget är fullt.");
                break;
            case ParkVehicleFeedback.InvalidVehicle:
                Console.WriteLine("Felaktigt fordon.");
                break;
        }
    }

    private Vehicle? CreateVehicleFromUser(string choice)
    {
        string regNumber = Ask.ForRegNumber("Regnummer (Format: ABC123): ");
        string color = Ask.ForString("Färg (Blå, Grön..): ");

        return choice switch
        {
            "1" => new Car(regNumber, color, Ask.ForString("Bränsle (Bensin/Diesel): ")),
            "2" => new MotorCycle(regNumber, color, Ask.ForBool("Har sidovagn? (j/n): ")),
            "3" => new Airplane(regNumber, color, Ask.ForInt("Antal motorer: ")),
            "4" => new Boat(regNumber, color, Ask.ForInt("Längd (m): ")),
            "5" => new Bus(regNumber, color, Ask.ForInt("Antal säten: ")),
            _ => null
        };
    }


    public void RemoveVehicle()
    {
        Console.WriteLine("\nHÄMTA FORDON");
        string regNumber = Ask.ForRegNumber("Ange registreringsnummer (Format: ABC123) på fordon som ska hämtas: ");

        var result = handler.RemoveVehicle(regNumber);

        switch (result)
        {
            case RemoveVehicleFeedback.Success:
                Console.WriteLine($"Fordon med registreringsnummer {regNumber} har nu lämnat garaget.");
                break;
            case RemoveVehicleFeedback.NotFound:
                Console.WriteLine($"Inget fordon med registreringsnummer {regNumber} hittades.");
                break;
            default:
                Console.WriteLine("Ett oväntat fel inträffade.");
                break;
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
        Console.WriteLine("\nSummering (antal typer):");
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


