namespace wordle;

internal class Dislpayer
{
    public static void Display(Game game)
    {
        Console.WriteLine("_______________________");
        foreach (var board in game.Rounds)
        {
            Display(true, board.Fields);
        }
    }

    public static void Display(bool shouldDisplay, List<Field> board)
    {
        if (!shouldDisplay)
            return;

        Console.Write('[');
        for (int i = 0; i < board.Count(); i++)
        {
            if (board[i].IsGreen)
            {
                Write(board[i].Content, ConsoleColor.Green);
            }
            else if (board[i].IsYellow)
            {
                Write(board[i].Content, ConsoleColor.Yellow);
            }
            else
            {
                Console.Write(board[i].Content);
            }
            if (i != board.Count - 1)
            {
                Console.Write(',');
            }
            else
            {
                Console.WriteLine("]");
            }
        }
    }

    static void Write(char letter, ConsoleColor consoleColor)
    {
        Console.ForegroundColor = consoleColor;
        Console.Write(letter);
        Console.ResetColor();
    }
}
