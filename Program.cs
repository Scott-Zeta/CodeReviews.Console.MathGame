// See https://aka.ms/new-console-template for more information

using MathGame;

List<GameRecord> gameHistory = new();
int difficulty = 1;
var game = new Game(gameHistory, difficulty);

mainMenu();

void mainMenu()
{
    string[] menu =
    [
        "1. Addition", "2. Subtraction", "3. Multiplication", "4. Division",
        "5. Random Game", "6. Set Difficulty", "7. View Game History", "8. Quit"
    ];

    while (true)
    {
        Console.WriteLine("Welcome to the Math Game!");
        foreach (var opt in menu) Console.WriteLine(opt);
        string choice = Console.ReadLine()?.Trim() ?? "";
        Console.Clear();
        switch (choice)
        {
            case "1": game.Play(1); break;
            case "2": game.Play(2); break;
            case "3": game.Play(3); break;
            case "4": game.Play(4); break;
            case "5": game.Play(isRandom: true); break;
            case "6":
                Console.Write("Set difficulty (1-3): ");
                if (int.TryParse(Console.ReadLine(), out int d) && d is >= 1 and <= 3)
                {
                    game.SetDifficulty(d);
                    Console.WriteLine($"Difficulty set to {d}.");
                }
                else
                {
                    Console.WriteLine("Invalid difficulty level. Please enter a number between 1 and 3.");
                }
                break;
            case "7": game.ViewGameHistory(); break;
            case "8": return;
            default: Console.WriteLine("Invalid choice."); break;
        }
    }
}