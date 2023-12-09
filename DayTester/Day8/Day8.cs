namespace DayTester;

[TestClass]
public class Day8
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day8/TestData.txt");
        AdventOfCode2023.Day8.Program.Part1(values);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day8/Data.txt");
        AdventOfCode2023.Day8.Program.Part1(values);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day8/TestDataPart2.txt");
        AdventOfCode2023.Day8.Program.Part2(values);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day8/Data.txt");
        AdventOfCode2023.Day8.Program.Part2(values);
    }
}
