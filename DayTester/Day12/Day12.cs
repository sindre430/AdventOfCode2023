namespace DayTester;

[TestClass]
public class Day12
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day12/TestData.txt");
        AdventOfCode2023.Day12.Program.Part1(values);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day12/Data.txt");
        AdventOfCode2023.Day12.Program.Part1(values);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day12/TestData.txt");
        AdventOfCode2023.Day12.Program.Part2(values, 5);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day12/Data.txt");
        AdventOfCode2023.Day12.Program.Part2(values, 5);
    }
}
