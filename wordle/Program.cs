using wordle;

string projectDirectory = AppDomain.CurrentDomain.BaseDirectory; // Get the project directory

// Specify the file path relative to the project directory
string filePath = Path.Combine(projectDirectory, "words.txt");


var allWords = File.ReadAllLines(filePath).Where(IsNotDuplicate).ToList();

bool IsNotDuplicate(string arg)
{
    bool hasDuplicateLetters = arg
            .GroupBy(c => c)
            .Any(group => group.Count() > 1);

    return !hasDuplicateLetters;
}

Game min = null;
Game max = null;
var gamePlayer = new GamePlayer(allWords, true);

List<Result> results = new();

for (int i = 0; i < 5000; i++)
{
    var result = gamePlayer.Play();
    if (results.FirstOrDefault(x => x.Tries == result.Rounds.Count) is Result relevant)
    {
        relevant.Count++;
    }
    else
    {
        results.Add(new Result() { Tries = result.Rounds.Count, Count=1 });
    }

    if (min == null)
        min = result;
    if(max == null)
        max = result;
    if (min.Rounds.Count > result.Rounds.Count)
        min = result;
    if(max.Rounds.Count < result.Rounds.Count)
        max = result;
}

Dislpayer.Display( min);
Dislpayer.Display( max);


foreach (var result in results.OrderBy(result => result.Tries))
{
    Console.WriteLine($"Tries {result.Tries}, Count {result.Count}");
}

double average = results.Sum(result => result.Count * result.Tries) /(double) results.Sum(result => result.Count);

Console.WriteLine($"Mean : {average})");
