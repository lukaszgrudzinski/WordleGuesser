public class Field
{
    public bool IsGreen { get; set; }
    public bool IsYellow { get; set; }
    public char Content { get; set; } = '_';

    public Field Copy()
    {
        return new() { Content = Content, IsGreen = IsGreen, IsYellow = IsYellow };
    }
}
