namespace AdventOfCode2023.Day3;

internal class Number
{
    public int Value { get; set; }
    public int ValueLength => Value.ToString().Length;

    public int StartIndex { get; set; }

    public int EndIndex => StartIndex + ValueLength - 1;

    public int LineNr { get; set; }

    public List<Symbol> Symbols { get; set; } = new List<Symbol>();
}
