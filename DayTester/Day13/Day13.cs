namespace DayTester;

[TestClass]
public class Day13
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day13/TestData.txt");
        AdventOfCode2023.Day13.Program.Part1(values).GetAwaiter().GetResult();
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day13/Data.txt");
        AdventOfCode2023.Day13.Program.Part1(values).GetAwaiter().GetResult();
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day13/TestData.txt");
        AdventOfCode2023.Day13.Program.Part2(values).GetAwaiter().GetResult();
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day13/Data.txt");
        AdventOfCode2023.Day13.Program.Part2(values).GetAwaiter().GetResult();
    }
}
