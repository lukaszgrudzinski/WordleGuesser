namespace wordle;

public class Round
{
    public List<Field> Fields {  get; set; }
    public Round FromLastRound()
    {
        return new Round() { Fields = Fields.Select(field => field.Copy()).ToList() };
    }
    public static Round FromGuess(string guess)
    {
        return new() { Fields = guess.Select(a => new Field() { Content = a }).ToList() } ;
    }
    public Round()
    {
        Fields = new List<Field>
        { new(), new(), new(), new(), new() };
    }
}