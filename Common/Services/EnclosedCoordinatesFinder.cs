namespace Common.Services;

public static class EnclosedCoordinatesFinder
{
    // Check if a coordinate (x, y) is within the bounds of the grid
    private static bool IsInsideGrid(int x, int y, int maxX, int maxY)
    {
        return (x >= 0 && x <= maxX && y >= 0 && y <= maxY);
    }

    // Perform Flood Fill algorithm to find enclosed coordinates within the loop
    public static List<(int, int)> GetEnclosedCoordinates(List<(int, int)> loopCoordinates)
    {
        List<(int, int)> enclosedCoordinates = [];

        // Find the bounding box of the loop
        int minX = int.MaxValue,
            maxX = int.MinValue,
            minY = int.MaxValue,
            maxY = int.MinValue;
        foreach (var (x, y) in loopCoordinates)
        {
            minX = Math.Min(minX, x);
            maxX = Math.Max(maxX, x);
            minY = Math.Min(minY, y);
            maxY = Math.Max(maxY, y);
        }

        // Create a grid to mark visited points
        bool[,] visited = new bool[maxX + 1, maxY + 1];

        // Perform Flood Fill from a starting point within the bounding box
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if (!visited[x, y] && IsPointInPolygon(x, y, loopCoordinates))
                {
                    FloodFill(x, y, loopCoordinates, visited, enclosedCoordinates);
                }
            }
        }

        return enclosedCoordinates;
    }

    // Check if a point (x, y) is inside a polygon defined by a list of vertices
    private static bool IsPointInPolygon(int x, int y, List<(int, int)> vertices)
    {
        bool isInside = false;

        for (int i = 0, j = vertices.Count - 1; i < vertices.Count; j = i++)
        {
            if (((vertices[i].Item2 > y) != (vertices[j].Item2 > y)) &&
                (x < (vertices[j].Item1 - vertices[i].Item1) * (y - vertices[i].Item2) / (double)(vertices[j].Item2 - vertices[i].Item2) + vertices[i].Item1))
            {
                isInside = !isInside;
            }
        }

        return isInside;
    }

    // Flood Fill algorithm to mark enclosed coordinates
    private static void FloodFill(int x, int y, List<(int, int)> loopCoordinates, bool[,] visited, List<(int, int)> enclosedCoordinates)
    {
        int maxX = visited.GetLength(0) - 1;
        int maxY = visited.GetLength(1) - 1;

        if (!IsInsideGrid(x, y, maxX, maxY) || visited[x, y] || !IsPointInPolygon(x, y, loopCoordinates))
        {
            return;
        }

        visited[x, y] = true;
        enclosedCoordinates.Add((x, y));

        FloodFill(x + 1, y, loopCoordinates, visited, enclosedCoordinates);
        FloodFill(x - 1, y, loopCoordinates, visited, enclosedCoordinates);
        FloodFill(x, y + 1, loopCoordinates, visited, enclosedCoordinates);
        FloodFill(x, y - 1, loopCoordinates, visited, enclosedCoordinates);
    }
}
