using System.Text.RegularExpressions;


namespace GarageApp.Helpers;

internal class Ask
{
    public static string ForChoice()
    {
        char[] allowedChoices = ['1', '2', '3', '4', '5'];
        string? choice;

        while (true)
        {
            choice = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(choice) || choice.Length != 1 || !allowedChoices.Contains(choice[0]))
            {
                Console.WriteLine("Felaktigt val. Vänligen ange en siffra 1 och 5:");
            }
            else return choice;
        }
    }
    public static string ForRegNumber(string question)
    {
        var regNumberPattern = @"^[A-Za-z]{3}\d{3}$";
        var regex = new Regex(regNumberPattern);

        while (true)
        {
            Console.Write(question);
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Registreringsnumret får inte vara tomt. Försök igen.");
                continue;
            }

            input = input.Trim().ToUpper();

            if (!regex.IsMatch(input))
            {
                Console.WriteLine("Ogiltigt format.");
                continue;
            }

            return input;
        }
    }

    public static string ForString(string question)
    {
        while (true)
        {
            Console.Write(question);
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"Inmatningen får inte vara tom. Försök igen. {question}");
            }
            else
            {
                return input.Trim();
            }
        }
    }

    public static string ForStringOrNull(string question)
    {
        while (true)
        {
            Console.Write(question);
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            else
            {
                return input.Trim();
            }
        }
    }

    public static bool ForBool(string question)
    {
        while (true)
        {
            Console.Write(question);
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "j" || input == "n")
                return input == "j" ? true : false;
            else
                Console.WriteLine($"Felaktig inmatning. Försök igen. {question}");
        }
    }

    public static int ForInt(string question)
    {
        while (true)
        {
            Console.Write(question);
            if (int.TryParse(Console.ReadLine(), out int input))
                return input;
            Console.WriteLine($"Felaktig inmatning. Ange ett heltal. {question}");
        }
    }

    public static int? ForIntOrNull(string question)
    {
        while (true)
        {
            Console.Write(question);
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            if (int.TryParse(input, out int parsedInput))
            {
                return parsedInput;
            }
            Console.WriteLine($"Felaktig inmatning. Ange ett heltal. {question}");
        }
    }



}

