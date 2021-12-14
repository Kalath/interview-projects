using DS.TaxCalculator.Services.TaxCalculatorServices;

const string ImaginariaCurrency = "IDR";

Console.WriteLine("Welcome to Imaginaria Tax Calculator!");
Console.WriteLine("Given person's gross salary it will calculate net salary based on Imaginaria's current tax rules.");
Console.WriteLine("Please insert gross salary.");

Console.WriteLine();
Console.WriteLine();

var keepCalculating = true;

while (keepCalculating)
{
    Console.Write("Gross salary: ");

    if (decimal.TryParse(Console.ReadLine(), out decimal personSalary)) // Check if user input is correct. Would prevent crashes and better experience. Working with decimals as they provide better precision when working with money.
    {
        var imaginariaTaxCalculator = new ImaginariaTaxCalculatorService();

        Console.ForegroundColor = ConsoleColor.Blue; // Makes the result distinguished from other wall of text.
        Console.WriteLine($"Calculated net salary: {imaginariaTaxCalculator.GetNetSalary(personSalary):0.00} {ImaginariaCurrency}");
        Console.ResetColor();

        keepCalculating = WaitForUser(); // Providing an option to the user to continue calculating or exit the program
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red; // Points out that something is not correct.
        Console.WriteLine("Please insert valid numbers only.");
        Console.ResetColor();
        Console.WriteLine();
    }
}

static bool WaitForUser()
{
    const char continueChar = 'c';
    const char exitChar = 'e';

    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Type 'C' for new Calculation or 'E' to Exit:");
        var userKey = Console.ReadKey();

        if (char.IsWhiteSpace(userKey.KeyChar))
        {
            continue;
        }

        // As we don't care about upper or lower symbol I'm using ToLowerInvariant because it tends to be faster.
        if (char.ToLowerInvariant(userKey.KeyChar).Equals(continueChar))
        {
            Console.WriteLine();
            return true;
        }

        if (char.ToLowerInvariant(userKey.KeyChar).Equals(exitChar))
        {
            Environment.Exit(0);
        }
    }
}