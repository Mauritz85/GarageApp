using System.Text.RegularExpressions;


namespace GarageApp.Helpers;

internal class AskHelpers
{
    public static string AskRegNumber()
    {
        var regNumberPattern = @"^[A-Za-z]{3}\d{3}$"; 
        var regex = new Regex(regNumberPattern);

        while (true)
        {
            Console.Write("Registreringsnummer: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Registreringsnumret får inte vara tomt. Försök igen.");
                continue;
            }

            input = input.Trim().ToUpper(); 

            if (!regex.IsMatch(input))
            {
                Console.WriteLine("Ogiltigt format. Ange registreringsnummer i formatet ABC123.");
                continue;
            }

            return input;
        }
    }

    public static string AskString(string question)
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

    public static bool AskBool(string question)
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

    public static int AskInt(string question)
    {
        while (true)
        {
            Console.Write(question);
            if (int.TryParse(Console.ReadLine(), out int input))
                return input;
            Console.WriteLine($"Felaktig inmatning. Ange ett heltal. {question}");
        }
    }


}

