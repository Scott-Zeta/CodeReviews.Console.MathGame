// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Random rnd = new Random();
int difficulty = 1;
List<GameRecord> gameHistory = new List<GameRecord>();
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
                PlayGame(1, difficulty);
                break;
            case "2":
                Console.WriteLine("You chose Subtraction.");
                PlayGame(2, difficulty);
                break;
            case "3":
                Console.WriteLine("You chose Multiplication.");
                PlayGame(3, difficulty);
                break;
            case "4":
                Console.WriteLine("You chose Division.");
                PlayGame(4, difficulty);
                break;
            case "5":
                Console.WriteLine("You chose Random Game.");
                PlayGame(rnd.Next(1, 5), difficulty, isRandom: true);
                break;
            case "6":
                Console.WriteLine("You chose to Set Difficulty.");
                difficulty = setDifficulty();
                Console.WriteLine($"Difficulty set to {difficulty}.");
                break;
            case "7":
                Console.WriteLine("You chose to View Game History.");
                ViewGameHistory();
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

void PlayGame(int op_key = 1, int difficulty = 1, bool isRandom = false)
{
    string[] symbol = ["+", "-", "*", "/"];

    int threshold = difficulty switch
    {
        1 => 10,
        2 => 50,
        3 => 100,
        _ => 10 // Default case
    };

    int result, num2, num1;

    while (true)
    {
        if (isRandom)
        {
            op_key = rnd.Next(1, 5); // Randomly select an operation
        }

        Console.WriteLine($"Playing game with {symbol[op_key - 1]} at difficulty {difficulty}.");
        switch (op_key)
        {
            case 1: // Addition
                result = rnd.Next(0, threshold + 1);
                num2 = rnd.Next(0, result + 1);
                num1 = result - num2;
                break;
            case 2: // Subtraction
                num1 = rnd.Next(0, threshold + 1);
                num2 = rnd.Next(0, num1 + 1);
                result = num1 - num2;
                break;
            case 3: // Multiplication
                num2 = rnd.Next(0, threshold + 1);
                num1 = rnd.Next(0, num2 == 0 ? threshold + 1 : threshold / Math.Max(num2, 1) + 1);
                result = num1 * num2;
                break;
            case 4: // Division
                num2 = rnd.Next(1, threshold + 1);
                result = rnd.Next(0, threshold / num2 + 1);
                num1 = result * num2;
                break;
            default:
                Console.WriteLine("Invalid operation key.");
                return; // Exit the function if the operation key is invalid
        }

        Console.WriteLine($"What is the answer of : {num1} {symbol[op_key - 1]} {num2} = ?");
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        string input = Console.ReadLine()?.Trim() ?? string.Empty;
        stopwatch.Stop();
        TimeSpan timeTaken = stopwatch.Elapsed;

        if (int.TryParse(input, out int userAnswer))
        {
            if (userAnswer == result)
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine($"Incorrect. The correct answer is {result}.");
            }
        }
        else
        {
            Console.WriteLine($"Invalid input. The correct answer is {result}.");
        }

        // Record the game result
        gameHistory.Add(new GameRecord
        {
            Question = $"{num1} {symbol[op_key - 1]} {num2} = {result}",
            Difficulty = difficulty,
            IsCorrect = userAnswer == result,
            TimeTaken = timeTaken.TotalSeconds,
            Timestamp = DateTime.Now
        });

        while (true)
        {
            Console.WriteLine("Would you like to play again?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. Back to Main Menu");
            string choice = Console.ReadLine()?.Trim() ?? string.Empty;
            if (choice == "2")
            {
                Console.Clear();
                return; // Exit the current game
            }
            else if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine("Great, let's try again...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
        }

    }
}

void ViewGameHistory()
{
    Console.Clear();
    Console.WriteLine("Game History:");
    if (gameHistory.Count == 0)
    {
        Console.WriteLine("No games played yet.");
    }
    else
    {
        int correctCount = 0;
        double timeTaken = 0;
        Console.WriteLine("Question | Difficulty | Is Correct | Time Taken | Timestamp");

        foreach (var record in gameHistory)
        {
            if (record.IsCorrect)
            {
                correctCount++;
                timeTaken += record.TimeTaken;
            }
            Console.WriteLine(record.GetResultString());
        }
        double correctionRate = (double)correctCount / gameHistory.Count * 100;
        double averageTime = correctCount == 0 ? -1 : timeTaken / correctCount;

        Console.WriteLine($"Your correction rate is {correctionRate:F2}%");
        Console.WriteLine($"Average time taken for correct answer: {averageTime:F2}s");
    }
    Console.WriteLine("Press any key to return to the main menu...");
    Console.ReadKey();
    return;
}

class GameRecord
{
    public required string Question { get; init; }
    public int Difficulty { get; init; }
    public bool IsCorrect { get; init; }
    public double TimeTaken { get; init; }
    public DateTime Timestamp { get; init; }

    public string GetResultString()
    {
        return $"{Question} | {Difficulty} | {IsCorrect} | {TimeTaken:F2}s | {Timestamp}";
    }
}