namespace AdventOfCode2023.Day4;

internal class CardCopy : Card
{
    public string Lineage { get; set; }

    public CardCopy(Card parentCard, Card cardToCopy) : 
        base(cardToCopy)
    {
        Lineage = parentCard is CardCopy cardCopy ?
            $"{cardCopy.Lineage}.{Number}" :
            $"{parentCard.Number}.{Number}";
    }
}
