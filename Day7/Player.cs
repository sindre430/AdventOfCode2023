namespace AdventOfCode2023.Day7;

internal class Player
{
    public int Id { get; set; }

    public Hand Hand { get; set; }

    public Hand? JokerHand { get; set; }

    public int Bet { get; set; }

    public Player(int id, string playerLine)
    {
        Id = id;
        var split = playerLine.Split(" ");
        Hand = new Hand(split[0]);
        Bet = int.Parse(split[1]);
    }
}
