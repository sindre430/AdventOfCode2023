namespace DayTester;

[TestClass]
public class Day2
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day2/TestData.txt");
        AdventOfCode2023.Day2.Program.Part1(values, 12, 13, 14);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day2/Data.txt");
        AdventOfCode2023.Day2.Program.Part1(values, 12, 13, 14);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day2/TestData.txt");
        AdventOfCode2023.Day2.Program.Part2(values);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day2/Data.txt");
        AdventOfCode2023.Day2.Program.Part2(values);
    }
}
