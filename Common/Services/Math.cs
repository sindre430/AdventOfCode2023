using System.Numerics;

namespace AdventOfCode2023.Common.Services;

public static class Math
{
    public static BigInteger LCM(BigInteger a, BigInteger b)
    {
        return a * b / BigInteger.GreatestCommonDivisor(a, b);
    }
}
