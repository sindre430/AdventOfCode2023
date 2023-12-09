using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day9;

internal class Sequence(List<int> numbers)
{
    public List<int> Numbers { get; set; } = numbers;
    public Sequence? Child { get; set; } = numbers.All(n => n == 0) ? null :
            new Sequence(GenerateChildSequence(numbers));

    public Sequence(string rawLine) : 
        this(rawLine.GetAllNumbers()) { }

    public int PredictNextValue()
    {
        var childValue = Child?.PredictNextValue() ?? 0;
        return Numbers.Last() + childValue;
    }

    public int PredictPrevValue()
    {
        var childValue = Child?.PredictPrevValue() ?? 0;
        return Numbers.First() - childValue;
    }

    public static List<int> GenerateChildSequence(List<int> numbers)
    {
        var res = new List<int>();

        for (var i=0; i<numbers.Count-1; i++)
        {
            res.Add(numbers[i+1] - numbers[i]);
        }

        return res;
    }
}
