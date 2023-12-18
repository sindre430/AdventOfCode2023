namespace DayTester;

[TestClass]
public class Day16
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day16/TestData.txt");
        AdventOfCode2023.Day16.Program.Part1(values);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day16/Data.txt");
        AdventOfCode2023.Day16.Program.Part1(values);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day16/TestData.txt");
        AdventOfCode2023.Day16.Program.Part2(values);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day16/Data.txt");
        AdventOfCode2023.Day16.Program.Part2(values);
    }
}
