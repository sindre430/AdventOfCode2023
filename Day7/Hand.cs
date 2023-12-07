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

    public long GetHandOrderValue()
    {
        var str = string.Empty;
        foreach(CardValue value in Cards)
        {
            switch (value)
            {
                case CardValue.Two:
                    str += "02";
                    break;
                case CardValue.Three:
                    str += "03";
                    break;
                case CardValue.Four:
                    str += "04";
                    break;
                case CardValue.Five:
                    str += "05";
                    break;
                case CardValue.Six:
                    str += "06";
                    break;
                case CardValue.Seven:
                    str += "07";
                    break;
                case CardValue.Eight:
                    str += "08";
                    break;
                case CardValue.Nine:
                    str += "09";
                    break;
                case CardValue.Ten:
                    str += "10";
                    break;
                case CardValue.Jack:
                    str += "11";
                    break;
                case CardValue.Queen:
                    str += "12";
                    break;
                case CardValue.King:
                    str += "13";
                    break;
                case CardValue.Ace:
                    str += "14";
                    break;
            }
        }

        return long.Parse(str);
    }

    public HandType GetHandType(out List<CardValue> cards)
    {
        cards = Cards;
        if(Cards.Count == 0)
        {
            return HandType.Unknown;
        }

        // Five of a kind
        var fiveOfAKind = Cards.GroupBy(x => x).FirstOrDefault(x => x.Count() == 5);
        if(fiveOfAKind != null)
        {
            cards = [.. fiveOfAKind];
            return HandType.FiveOfAKind;
        }

        // Four of a kind
        var fourOfAKind = Cards.GroupBy(x => x).FirstOrDefault(x => x.Count() == 4);
        if(Cards.GroupBy(x => x).Any(x => x.Count() == 4))
        {
            cards = [.. fourOfAKind];
            return HandType.FourOfAKind;
        }

        // Full house
        var threeOfAKind = Cards.GroupBy(x => x).Where(x => x.Count() >= 3).ToList();
        var twoOfAKind = Cards.GroupBy(x => x).Where(x => x.Count() >= 2).ToList();
        var uniqueTwoOfAKind = twoOfAKind.Where(k => !threeOfAKind.Any(tk => tk.Key == k.Key)).ToList();
        if (threeOfAKind.Count != 0 && uniqueTwoOfAKind.Count != 0)
        {
            cards = threeOfAKind.Concat(uniqueTwoOfAKind).SelectMany(x => x).ToList();
            return HandType.FullHouse;
        }

        // Three of a kind
        if(threeOfAKind.Count != 0)
        {
            cards = threeOfAKind.SelectMany(x => x).ToList();
            return HandType.ThreeOfAKind;
        }

        // Two pairs
        if(twoOfAKind.Count >= 2)
        {
            cards = twoOfAKind.SelectMany(x => x).ToList();
            return HandType.TwoPairs;
        }

        // One pair
        if(twoOfAKind.Count == 1)
        {
            cards = twoOfAKind.SelectMany(x => x).ToList();
            return HandType.OnePair;
        }

        // High card
        cards = [Cards.OrderByDescending(x => x).First()];
        return HandType.HighCard;
    }
}

