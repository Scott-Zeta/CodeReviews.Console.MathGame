// See https://aka.ms/new-console-template for more information
int difficulty = 1;
mainMenu();

void mainMenu()
{
    string[] menuOptions =
    [
        "1. Addition",
        "2. Subtraction",
        "3. Multiplication",
        "4. Division",
        "5. Random Game",
        "6. Set Difficulty",
        "7. View Game History",
        "8. Quit"
    ];
    Random rnd = new Random();

    Console.Clear();
    while (true)
    {
        Console.WriteLine("Welcome to the Math Game!");
        Console.WriteLine("Please choose an option:");
        foreach (string option in menuOptions)
        {
            Console.WriteLine(option);
        }
        string choice = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Clear();
        switch (choice)
        {
            case "1":
                Console.WriteLine("You chose Addition.");
                playGame(1, difficulty);
                break;
            case "2":
                Console.WriteLine("You chose Subtraction.");
                playGame(1, difficulty);
                break;
            case "3":
                Console.WriteLine("You chose Multiplication.");
                playGame(1, difficulty);
                break;
            case "4":
                Console.WriteLine("You chose Division.");
                playGame(1, difficulty);
                break;
            case "5":
                Console.WriteLine("You chose Random Game.");
                playGame(rnd.Next(1, 5), difficulty);
                break;
            case "6":
                Console.WriteLine("You chose to Set Difficulty.");
                difficulty = setDifficulty();
                Console.WriteLine($"Difficulty set to {difficulty}.");
                break;
            case "7":
                Console.WriteLine("You chose to View Game History.");
                break;
            case "8":
                Console.WriteLine("Thank you for playing! Goodbye!");
                return; // Exit the game
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}

int setDifficulty()
{
    Console.WriteLine("Current difficulty level: " + difficulty);
    Console.WriteLine("Please set the difficulty level (1-3):");
    string input = Console.ReadLine()?.Trim() ?? string.Empty;
    if (int.TryParse(input, out int level) && level >= 1 && level <= 3)
    {
        return level;
    }
    else
    {
        Console.WriteLine("Invalid difficulty level. Setting to default (1).");
        return 1; // Default difficulty
    }
}

void playGame(int operation, int difficulty)
{
    Console.WriteLine($"Playing game with operation {operation} at difficulty {difficulty}.");
}