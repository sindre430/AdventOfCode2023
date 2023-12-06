namespace DayTester;

[TestClass]
public class Day6
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day6/Part1TestData.txt");
        AdventOfCode2023.Day6.Program.Part1(values);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day6/Part1Data.txt");
        AdventOfCode2023.Day6.Program.Part1(values);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day6/Part2TestData.txt");
        AdventOfCode2023.Day6.Program.Part2(values);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day6/Part2Data.txt");
        AdventOfCode2023.Day6.Program.Part2(values);
    }
}
