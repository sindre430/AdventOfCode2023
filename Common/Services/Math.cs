using System.Numerics;

namespace AdventOfCode2023.Common.Services;

public static class Math
{
    public static BigInteger LCM(BigInteger a, BigInteger b)
    {
        return a * b / BigInteger.GreatestCommonDivisor(a, b);
    }

    public static T[,] RotateMatrixCounterClockwise<T>(this T[,] oldMatrix)
    {
        T[,] newMatrix = new T[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
        int newColumn, newRow = 0;
        for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
        {
            newColumn = 0;
            for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
            {
                newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                newColumn++;
            }
            newRow++;
        }
        return newMatrix;
    }

    public static T[,] RotateMatrixClockwise<T>(this T[,] oldMatrix)
    {
        T[,] newMatrix = new T[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
        int newColumn, newRow = 0;
        for (int oldColumn = 0; oldColumn < oldMatrix.GetLength(1); oldColumn++)
        {
            newColumn = 0;
            for (int oldRow = oldMatrix.GetLength(0) - 1; oldRow >= 0; oldRow--)
            {
                newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                newColumn++;
            }
            newRow++;
        }
        return newMatrix;
    }

    public static T[,] ConvertToMultiDimensionalArray<T>(this T[][] arr)
    {
        int rows = arr.Length;
        int cols = arr[0].Length; // Assuming all inner arrays have the same length

        T[,] multiDimensionalArray = new T[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                multiDimensionalArray[i, j] = arr[i][j];
            }
        }

        return multiDimensionalArray;
    }

    public static T[][] ConvertToJaggedArray<T>(this T[,] arr)
    {
        int rows = arr.GetLength(0);
        int cols = arr.GetLength(1);

        T[][] jaggedArray = new T[rows][];

        for (int i = 0; i < rows; i++)
        {
            jaggedArray[i] = new T[cols];
            for (int j = 0; j < cols; j++)
            {
                jaggedArray[i][j] = arr[i, j];
            }
        }

        return jaggedArray;
    }
}
