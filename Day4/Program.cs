using System.IO.Compression;

namespace AdventOfCode2023.Day4;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] cardLines)
    {
        var cards = cardLines.Select(cl => new Card(cl))
            .ToList();

        foreach(var card in cards)
        {
            var cardPoints = card.GetPoints();
            Console.WriteLine($"{card.RawLine} (Points: {cardPoints}) {(cardPoints > 0 ? $"(Numbers: {string.Join(", ", card.GetWinningNumbers())})" : string.Empty)}");
        }

        var points = cards.Select(c => c.GetPoints())
            .Aggregate((sum, val) => sum + val);

        Console.WriteLine();
        Console.WriteLine($"Total points: {points}");
    }

    public static void Part2(string[] cardLines)
    {
        // Setup Paths and remove old files
        var defaultOutput = Console.Out;
        var outputDirectory = "./Day4/output";
        var outputFileName = "Output.txt";
        var outputZipFilePath = "./Day4/Day4_output.zip";
        Directory.CreateDirectory(outputDirectory);
        File.Delete(Path.Combine(outputDirectory, outputFileName));
        File.Delete(outputZipFilePath);

        // Setup StreamWriter
        using StreamWriter fileWriter = new(Path.Combine(outputDirectory, outputFileName));
        Console.SetOut(fileWriter);

        var originalCards = cardLines.Select(cl => new Card(cl))
            .ToList();
        var cardCopies = new List<CardCopy>();

        void createCardCopies(Card card)
        {
            var numberOfCopies = card.GetWinningNumbers().Count;
            for (var i = 0; i < numberOfCopies; i++)
            {
                var cardToCopyIndex = card.Number + i;
                if (cardToCopyIndex >= originalCards.Count)
                {
                    break;
                }
                cardCopies.Add(new CardCopy(card, originalCards[cardToCopyIndex]));
            }

            Console.WriteLine($"{card.RawLine} (Matches: {card.GetWinningNumbers().Count}) {(card is CardCopy cc ? $"(Copy: {cc.Lineage}) " : string.Empty)}");
        }

        // Loop through all cards and create copies for each winning number
        for (var i = 0; i < originalCards.Count; i++)
        {
            var currentCard = originalCards[i];
            createCardCopies(currentCard);

            var matchingCardCopies = cardCopies.Where(cc => cc.Number == currentCard.Number).ToList();
            foreach (var cardCopy in matchingCardCopies)
            {
                createCardCopies(cardCopy);
            }
        }

        // Print Summary
        var summary = $"Total number of cards: {originalCards.Count + cardCopies.Count} (Original Cards: {originalCards.Count}, Copies: {cardCopies.Count})";
        Console.WriteLine();
        Console.WriteLine(summary);

        // Close StreamWriter
        fileWriter.Close();
        fileWriter.Dispose();

        // Zip Output
        ZipFile.CreateFromDirectory("./Day4/output", outputZipFilePath);
        Directory.Delete("./Day4/output", true);

        // Print Summary to Console
        Console.SetOut(defaultOutput);
        Console.WriteLine(summary);
        Console.WriteLine();
        Console.WriteLine($"Detailed output written here: {Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), outputZipFilePath))}");

        // Get current directory
        var currentDirectory = Directory.GetCurrentDirectory();
    }
}