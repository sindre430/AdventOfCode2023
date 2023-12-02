namespace DayTester;

[TestClass]
public class Day1
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day1/TestDataPart1.txt");
        AdventOfCode2023.Day1.Program.Part1(values);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day1/Data.txt");
        AdventOfCode2023.Day1.Program.Part1(values);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day1/TestDataPart2.txt");
        AdventOfCode2023.Day1.Program.Part2(values);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day1/Data.txt");
        AdventOfCode2023.Day1.Program.Part2(values);
    }
}