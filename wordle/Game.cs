namespace wordle;

internal class Game
{
    public List<Round> Rounds { get; } = new List<Round>() { new Round()};
    public List<Field> CurrentFields => Rounds.Last().Fields;
}
