namespace MathGame;

public class GameRecord
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
