namespace AdventOfCode2023.Day6;

internal class Race
{
    public int Id { get; set; }

    public long TimeInMs { get; set; }

    public long RecordDistanceInMm { get; set; }

    public List<int> GetWinningOptions()
    {
        var winningOptions = new List<int>();
        for(var ms=0; ms<=TimeInMs; ms++)
        {
            var distance = CalculateDistance(ms);
            if(distance > RecordDistanceInMm)
            {
                winningOptions.Add(ms);
            }
        }

        return winningOptions;
    }

    private long CalculateDistance(int pushTimeInMs) =>
        pushTimeInMs * (TimeInMs - pushTimeInMs);
}
