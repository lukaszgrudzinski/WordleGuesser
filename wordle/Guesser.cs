namespace wordle;

internal class Guesser
{
    private readonly List<string> allWords;
    private readonly Game game;
    private readonly HashSet<char> _forbiddenLetters;
    private bool isFirst = true;

    private IEnumerable<Field> Fields => game.Rounds.Last().Fields;

    public Guesser(List<string> allWords, Game game)
    {
        this.allWords = allWords;
        this.game = game;
        _forbiddenLetters = new HashSet<char>();

    }
    public string Guess()
    {
        foreach (var gray in Fields.Where(field => field.IsGreen == false && field.IsYellow == false).Select(field => field.Content))
        {
            _forbiddenLetters.Add(gray);
        }

        if (isFirst)
        {
            isFirst = false;
            return "slate";
        }

        var availableGuesses = allWords.Where(word => IsOkGuess(word)).ToList();

        string result =  availableGuesses.ElementAt(new Random().Next(availableGuesses.Count));

        return result;
    }
    private bool IsOkGuess(string word)
    {
        for (int i = 0; i< word.Length; i++)
        {
            if (Fields.ElementAt(i).IsGreen && Fields.ElementAt(i).Content != word[i])
                return false;
            if(Fields.ElementAt(i).IsYellow && Fields.ElementAt(i).Content == word[i]) 
                return false;

            if (_forbiddenLetters.Contains(word[i]))
                return false;
        }

        foreach (var field in Fields.Where(field => field.IsYellow))
        {
            if(!word.Contains(field.Content)) 
                return false;
        }

        return true;
    }

    private List<char> GrayLetters ()
    {
        return game.Rounds.Select(a => a.Fields).SelectMany(a => a).Where(a => a.IsYellow == false && a.IsGreen == false).Select(a => a.Content).Distinct().ToList();
    }

    private bool WasLetterGray(char v, int i, IEnumerable<Field> board)
    {
        if (board.ElementAt(i).Content == v && board.ElementAt(i).IsGreen == false)
            return true ;

        return false;
    }
}
