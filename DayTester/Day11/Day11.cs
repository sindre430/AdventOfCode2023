namespace DayTester;

[TestClass]
public class Day11
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day11/TestData.txt");
        AdventOfCode2023.Day11.Program.Part1(values);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day11/Data.txt");
        AdventOfCode2023.Day11.Program.Part1(values);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day11/TestData.txt");
        AdventOfCode2023.Day11.Program.Part2(values, 100);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day11/Data.txt");
        AdventOfCode2023.Day11.Program.Part2(values, 1000000);
    }
}
