namespace DayTester;

[TestClass]
public class Day10
{
    [TestMethod]
    public void Part1RunWithTestData()
    {
        var values = File.ReadAllLines("./Day10/TestData.txt");
        AdventOfCode2023.Day10.Program.Part1(values);
    }

    [TestMethod]
    public void Part1Run()
    {
        var values = File.ReadAllLines("./Day10/Data.txt");
        AdventOfCode2023.Day10.Program.Part1(values);
    }

    [TestMethod]
    public void Part2RunWithTestData()
    {
        var values = File.ReadAllLines("./Day10/TestDataPart2.txt");
        AdventOfCode2023.Day10.Program.Part2(values);
    }

    [TestMethod]
    public void Part2Run()
    {
        var values = File.ReadAllLines("./Day10/Data.txt");
        AdventOfCode2023.Day10.Program.Part2(values);
    }
}
