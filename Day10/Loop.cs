namespace AdventOfCode2023.Day10;

internal class Loop(Pipe startPipe)
{
    public Pipe StartPipe { get; set; } = startPipe;
    public List<Pipe> Pipes { get; set; } = GetAllPipesInOrder(startPipe);

    public List<Pipe> GetAllCornerPipes() =>
        GetAllPipesInOrder(StartPipe).Where(p => !new[] { '-', '|' }.Contains(p.Value))
            .ToList();

    private static List<Pipe> GetAllPipesInOrder(Pipe startPipe)
    {
        var curPipe = startPipe;
        var pipes = new List<Pipe>();
        while (!pipes.Any(p => p.Position == curPipe.Position))
        {
            pipes.Add(curPipe);
            curPipe = curPipe.NextPipe ??
                throw new Exception("The pipe does not have a next pipe, hence there is no loop to be formed");
        }

        return pipes;
    }
}
