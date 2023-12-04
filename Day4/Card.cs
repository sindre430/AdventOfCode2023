using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day4;

internal class Card
{
    public int Number { get; set; }

    public string RawLine { get; set; }

    public List<int> WinningNumbers { get; set; }

    public List<int> CardNumbers { get; set; }

    public Card(string cardLine)
    {
        // CardLine has this format:
        // Card X: 1 2 3 4 5 | 1 2 3 4 5 6 7 8
        // X = Id, Left of *pipe* are winning numbers, right of *pipe* are card numbers
        RawLine = cardLine;
        
        var cardIdSplit = RawLine.Split(':');
        Number = cardIdSplit[0].GetFirstNumber() ??
            throw new InvalidOperationException($"No Card number found in line: {RawLine}");

        var numberSplit = cardIdSplit[1].Split('|');
        WinningNumbers = numberSplit[0].Split(' ')
            .Where(s => !string.IsNullOrEmpty(s))
            .Select(int.Parse)
            .ToList();
        
        CardNumbers = numberSplit[1].Split(' ')
            .Where(s => !string.IsNullOrEmpty(s))
            .Select(int.Parse)
            .ToList();
    }

    public Card(Card card)
    {
        Number = card.Number;
        RawLine = card.RawLine;
        WinningNumbers = card.WinningNumbers;
        CardNumbers = card.CardNumbers;
    }

    public int GetPoints()
    {
        var winningNumbers = GetWinningNumbers();
        if (winningNumbers.Count == 0)
        {
            return 0;
        }

        return (int)Math.Pow(2, winningNumbers.Count - 1);
    }

    public List<int> GetWinningNumbers() =>
        CardNumbers.Intersect(WinningNumbers).ToList();
}
