namespace AdventOfCode2023.Day7;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] playersArray)
    {
        var players = playersArray.Select((p, i) => new Player(i, p)).ToList();

        var playersWithHandType = new Dictionary<HandType, List<Player>>();
        foreach (var player in players)
        {
            var handType = player.Hand.GetHandType(out _);
            if (!playersWithHandType.TryGetValue(handType, out List<Player>? value))
            {
                value = ([]);
                playersWithHandType.Add(handType, value);
            }

            value.Add(player);
        }
        
        var sortedPlayers = new List<Player>();
        foreach(var handType in playersWithHandType.OrderByDescending(p => p.Key))
        {
            var playersWithHandTypeSorted = handType.Value.OrderByDescending(p => p.Hand.GetHandOrderValue()).ToList();
            sortedPlayers.AddRange(playersWithHandTypeSorted);
        }

        var totalWinnings = sortedPlayers.Select((p, i) => (sortedPlayers.Count - i) * p.Bet)
            .Sum();

        Console.WriteLine($"Total winnings: {totalWinnings}");
    }

    public static void Part2(string[] raceInfo)
    {
    }
}