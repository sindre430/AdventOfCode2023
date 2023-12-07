namespace AdventOfCode2023.Day7;

internal class Hand
{
    public List<CardValue> Cards { get; set; } = [];

    public Hand(string handString) {
        var charToCardValueDict = new Dictionary<string, CardValue> {
            { "2", CardValue.Two },
            { "3", CardValue.Three },
            { "4", CardValue.Four },
            { "5", CardValue.Five },
            { "6", CardValue.Six },
            { "7", CardValue.Seven },
            { "8", CardValue.Eight },
            { "9", CardValue.Nine },
            { "T", CardValue.Ten },
            { "J", CardValue.Jack },
            { "Q", CardValue.Queen },
            { "K", CardValue.King },
            { "A", CardValue.Ace },
        };

        foreach (char value in handString)
        {
            charToCardValueDict.TryGetValue(value.ToString(), out var cardValue);
            Cards.Add(cardValue);
        }
    }

    public long GetHandOrderValue(Dictionary<CardValue, string>? overrideValues = null) =>
        GetHandOrderValue(Cards, overrideValues);

    public static long GetHandOrderValue(List<CardValue> cards, Dictionary<CardValue, string>? overrideValues = null)
    {
        Dictionary<CardValue, string> defaultCardValues = new()
        {
            { CardValue.Two, "02" },
            { CardValue.Three, "03" },
            { CardValue.Four, "04" },
            { CardValue.Five, "05" },
            { CardValue.Six, "06" },
            { CardValue.Seven, "07" },
            { CardValue.Eight, "08" },
            { CardValue.Nine, "09" },
            { CardValue.Ten, "10" },
            { CardValue.Jack, "11" },
            { CardValue.Queen, "12" },
            { CardValue.King, "13" },
            { CardValue.Ace, "14" }
        };

        var str = string.Empty;
        foreach (CardValue value in cards)
        {
            string cardString = overrideValues?.GetValueOrDefault(value) ?? defaultCardValues[value];
            str += cardString;
        }

        return long.Parse(str);
    }

    public HandType GetHandType(out List<CardValue> cards) =>
        GetHandType(Cards, out cards);

    public static HandType GetHandType(List<CardValue> cards, out List<CardValue> pointCards)
    {
        if (cards.Count == 0)
        {
            pointCards = [];
            return HandType.Unknown;
        }

        var cardGroups = cards.GroupBy(x => x).ToList();

        // Five of a kind
        var fiveOfAKind = cardGroups.FirstOrDefault(x => x.Count() == 5);
        if (fiveOfAKind != null)
        {
            pointCards = [.. fiveOfAKind];
            return HandType.FiveOfAKind;
        }

        // Four of a kind
        var fourOfAKind = cardGroups.FirstOrDefault(x => x.Count() == 4);
        if (fourOfAKind != null)
        {
            pointCards = [.. fourOfAKind];
            return HandType.FourOfAKind;
        }

        // Full house
        var threeOfAKind = cardGroups.Where(x => x.Count() >= 3).ToList();
        var twoOfAKind = cardGroups.Where(x => x.Count() >= 2).ToList();
        var uniqueTwoOfAKind = twoOfAKind.Where(k => !threeOfAKind.Any(tk => tk.Key == k.Key)).ToList();
        if (threeOfAKind.Count != 0 && uniqueTwoOfAKind.Count != 0)
        {
            pointCards = threeOfAKind.Concat(uniqueTwoOfAKind).SelectMany(x => x).ToList();
            return HandType.FullHouse;
        }

        // Three of a kind
        if (threeOfAKind.Count != 0)
        {
            pointCards = threeOfAKind.SelectMany(x => x).ToList();
            return HandType.ThreeOfAKind;
        }

        // Two pairs
        if (twoOfAKind.Count >= 2)
        {
            pointCards = twoOfAKind.SelectMany(x => x).ToList();
            return HandType.TwoPairs;
        }

        // One pair
        if (twoOfAKind.Count == 1)
        {
            pointCards = twoOfAKind.SelectMany(x => x).ToList();
            return HandType.OnePair;
        }

        // High card
        pointCards = [cards.OrderByDescending(x => x).First()];
        return HandType.HighCard;
    }
}

