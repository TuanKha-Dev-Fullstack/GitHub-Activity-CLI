using GitHub_Activity_CLI.services;

namespace GitHub_Activity_CLI;

public static class Program
{
    private static async Task Main()
    {
        const string nameValidation = "\nPlease enter the username to fetch data.";
        const string helpMessage = "Use \"help\" for more information.\n";
        Console.WriteLine("---------------------------------GitHub Activity CLI---------------------------------");
        Console.WriteLine(helpMessage);
        string? userInput;
        do
        {
            userInput = UserInput();
            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine(nameValidation + " " + helpMessage);
            }
            if (userInput == "help")
            {
                PrintHelp();
            }
            if(!string.IsNullOrEmpty(userInput))
            {
                await FetchActivity.Fetch(userInput);
            }
        } while(userInput != "exit");
    }
    /// <summary>Prompts the user for input and returns the trimmed input string.</summary>
    /// <returns>The user's input</returns>
    private static string? UserInput()
    {
        Console.Write("github-activity ");
        return Console.ReadLine()?.Trim();
    }
    /// <summary>Displays help message.</summary>
    private static void PrintHelp()
    {
        Console.WriteLine("\nAvailable commands and usage instructions:");
        Console.WriteLine("<username> - Fetches data for the given username.");
        Console.WriteLine("help - Displays help message.");
        Console.WriteLine("exit - Exits the program.\n");
    }
}