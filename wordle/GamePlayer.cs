namespace wordle;

internal class GamePlayer
{
    private readonly List<string> allWords;
    private readonly bool shouldDislay;

    public GamePlayer(List<string> allWords, bool shouldDislay)
    {
        this.allWords = allWords;
        this.shouldDislay = shouldDislay;
    }

    public Game Play()
    {
        var game = new Game();
        string toGuess = allWords[new Random().Next(allWords.Count)];
        if(shouldDislay)
            Console.WriteLine(toGuess);
        int tries = 0;

        var guesser = new Guesser(allWords, game);

        while (game.CurrentFields.Any(spot => !spot.IsGreen))
        {
            tries++;
            //string? input = Console.ReadLine();
            string input = guesser.Guess();
            if (input?.Length != 5 || input.Any(letter => !char.IsLetter(letter)) || !allWords.Contains(input))
            {
                if(shouldDislay)
                    Console.WriteLine("Incorrect input");
                continue;
            }
            game.Rounds.Add(Round.FromGuess(input));

            for (int i = 0; i < game.CurrentFields.Count; i++)
            {
                game.CurrentFields[i].Content = input[i];

                if (toGuess[i] == input[i])
                {
                    game.CurrentFields[i].IsGreen = true;
                    game.CurrentFields[i].IsYellow = false;
                }
                else if (toGuess.Contains(input[i]))
                {
                    game.CurrentFields[i].IsYellow = true;
                    game.CurrentFields[i].IsGreen = false;
                }
                else
                {
                    game.CurrentFields[i].IsGreen = false;
                    game.CurrentFields[i].IsYellow = false;
                }
            }

            Dislpayer.Display(shouldDislay, game.CurrentFields);

        }

        if(shouldDislay)
            Console.WriteLine($"Congrats! you won in {tries}!");

        return game;
    }
}
