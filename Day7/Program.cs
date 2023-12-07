namespace AdventOfCode2023.Day7;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] playersArray)
    {
        var players = playersArray.Select((p, i) => new Player(i, p)).ToList();
        var totalWinnings = GetTotalWinningsForPlayers(players);

        Console.WriteLine($"Total winnings: {totalWinnings}");
    }

    public static void Part2(string[] raceInfo)
    {
        var joker = CardValue.Jack;
        var players = raceInfo.Select((p, i) => new Player(i, p)).ToList();

        // Create hands with replaced jokers
        foreach (var player in players)
        {
            var jokerHand = ReplaceJokerCards(player.Hand.Cards, joker);
            Console.WriteLine($"Player {player.Id}: Hand: {string.Join(" ", player.Hand.Cards)}, Joker Hand: {string.Join(" ", jokerHand)}");
            player.JokerHand = new Hand(string.Empty)
            {
                Cards = jokerHand
            };
        }

        Console.WriteLine();
        Console.WriteLine("Sorted players:");
        var totalWinnings = SortPlayersByScore(players, userJokerHands: true, jokerCard: joker).Select((p, i) => {
            var score = (players.Count - i) * p.Bet;
            Console.WriteLine($"Player {p.Id}, pos: {i}, winning: {score} (bet: {p.Bet} * {players.Count-i})");
            return score;
        }).Sum();

        Console.WriteLine($"Total winnings: {totalWinnings}");
    }

    private static List<CardValue> ReplaceJokerCards(List<CardValue> cardValues, CardValue joker)
    {
        var numberOfJokers = cardValues.Count(c => c == joker);
        if(numberOfJokers == 0)
        {
            return cardValues;
        }

        var allPossibleCombinations = new List<List<CardValue>>();
        GenerateCombinations(cardValues, 0, allPossibleCombinations, joker);

        // Setup fake players for each combination
        var fakePlayers = allPossibleCombinations.Select((c, i) =>
        {
            var p = new Player(i, "A 0");
            p.Hand.Cards = c;
            return p;
        }).ToList();

        var sortedPlayers = SortPlayersByScore(fakePlayers);
        var winningPlayer = sortedPlayers.First();

        return winningPlayer.Hand.Cards;
    }

    static void GenerateCombinations(List<CardValue> cardValues, int index, List<List<CardValue>> combinations, CardValue wildcard)
    {
        if (index == cardValues.Count)
        {
            combinations.Add([..cardValues]);
            return;
        }

        if (cardValues.ElementAt(index) == wildcard)
        {
            foreach (var cardValue in Enum.GetValues<CardValue>())
            {
                if(cardValue == wildcard)
                {
                    continue;
                }
                cardValues[index] = cardValue;
                GenerateCombinations(cardValues, index + 1, combinations, wildcard);
            }
            
            cardValues[index] = wildcard; // Reset wildcard for backtracking
        }
        else
        {
            GenerateCombinations(cardValues, index + 1, combinations, wildcard);
        }
    }

    private static List<Player> SortPlayersByScore(List<Player> players, bool userJokerHands = false, CardValue? jokerCard = null)
    {
        var playersWithHandType = new Dictionary<HandType, List<Player>>();
        foreach (var player in players)
        {
            var handType = userJokerHands ? player.JokerHand!.GetHandType(out _) : 
                player.Hand.GetHandType(out _);
            if (!playersWithHandType.TryGetValue(handType, out List<Player>? value))
            {
                value = ([]);
                playersWithHandType.Add(handType, value);
            }

            value.Add(player);
        }

        var sortedPlayers = new List<Player>();
        foreach (var handType in playersWithHandType.OrderByDescending(p => p.Key))
        {
            var playersWithHandTypeSorted = (userJokerHands && jokerCard != null) ?
                [ .. handType.Value.OrderByDescending(p => p.Hand.GetHandOrderValue(overrideValues: new Dictionary<CardValue, string>() { { jokerCard.Value, "01" } })) ] :
                handType.Value.OrderByDescending(p => p.Hand.GetHandOrderValue()).ToList();
            sortedPlayers.AddRange(playersWithHandTypeSorted);
        }

        return sortedPlayers;
    }

    private static int GetTotalWinningsForPlayers(List<Player> players)
        => SortPlayersByScore(players).Select((p, i) => (players.Count - i) * p.Bet)
            .Sum();
}