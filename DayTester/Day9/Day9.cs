namespace DayTester;

[TestClass]
public class Day9
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day9/TestData.txt");
        AdventOfCode2023.Day9.Program.Part1(values);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day9/Data.txt");
        AdventOfCode2023.Day9.Program.Part1(values);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day9/TestData.txt");
        AdventOfCode2023.Day9.Program.Part2(values);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day9/Data.txt");
        AdventOfCode2023.Day9.Program.Part2(values);
    }
}
