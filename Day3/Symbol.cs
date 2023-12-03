namespace AdventOfCode2023.Day3;

internal class Symbol
{
    public char Value { get; set; }
    public int LineNr { get; set; }
    public int StartIndex { get; set; }
    public string Id => $"{LineNr}-{StartIndex}";
}
