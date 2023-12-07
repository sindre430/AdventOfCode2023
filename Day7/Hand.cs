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
        var str = string.Empty;
        foreach (CardValue value in cards)
        {
            switch (value)
            {
                case CardValue.Two:
                    str += overrideValues?.GetValueOrDefault(CardValue.Two) ?? "02";
                    break;
                case CardValue.Three:
                    str += overrideValues?.GetValueOrDefault(CardValue.Three) ?? "03";
                    break;
                case CardValue.Four:
                    str += overrideValues?.GetValueOrDefault(CardValue.Four) ?? "04";
                    break;
                case CardValue.Five:
                    str += overrideValues?.GetValueOrDefault(CardValue.Five) ?? "05";
                    break;
                case CardValue.Six:
                    str += overrideValues?.GetValueOrDefault(CardValue.Six) ?? "06";
                    break;
                case CardValue.Seven:
                    str += overrideValues?.GetValueOrDefault(CardValue.Seven) ?? "07";
                    break;
                case CardValue.Eight:
                    str += overrideValues?.GetValueOrDefault(CardValue.Eight) ?? "08";
                    break;
                case CardValue.Nine:
                    str += overrideValues?.GetValueOrDefault(CardValue.Nine) ?? "09";
                    break;
                case CardValue.Ten:
                    str += overrideValues?.GetValueOrDefault(CardValue.Ten) ?? "10";
                    break;
                case CardValue.Jack:
                    str += overrideValues?.GetValueOrDefault(CardValue.Jack) ?? "11";
                    break;
                case CardValue.Queen:
                    str += overrideValues?.GetValueOrDefault(CardValue.Queen) ?? "12";
                    break;
                case CardValue.King:
                    str += overrideValues?.GetValueOrDefault(CardValue.King) ?? "13";
                    break;
                case CardValue.Ace:
                    str += overrideValues?.GetValueOrDefault(CardValue.Ace) ?? "14";
                    break;
            }
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

        // Five of a kind
        var fiveOfAKind = cards.GroupBy(x => x).FirstOrDefault(x => x.Count() == 5);
        if (fiveOfAKind != null)
        {
            pointCards = [.. fiveOfAKind];
            return HandType.FiveOfAKind;
        }

        // Four of a kind
        var fourOfAKind = cards.GroupBy(x => x).FirstOrDefault(x => x.Count() == 4);
        if (cards.GroupBy(x => x).Any(x => x.Count() == 4))
        {
            pointCards = [.. fourOfAKind];
            return HandType.FourOfAKind;
        }

        // Full house
        var threeOfAKind = cards.GroupBy(x => x).Where(x => x.Count() >= 3).ToList();
        var twoOfAKind = cards.GroupBy(x => x).Where(x => x.Count() >= 2).ToList();
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

