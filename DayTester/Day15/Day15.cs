namespace DayTester;

[TestClass]
public class Day15
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day15/TestData.txt");
        AdventOfCode2023.Day15.Program.Part1(values);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day15/Data.txt");
        AdventOfCode2023.Day15.Program.Part1(values);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day15/TestData.txt");
        AdventOfCode2023.Day15.Program.Part2(values);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day15/Data.txt");
        AdventOfCode2023.Day15.Program.Part2(values);
    }
}
