using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day14;

internal class Platform(string[] platformLines)
{
    public List<string> Lines { get; set; } = [.. platformLines];

    public long Id => Lines.Select(l => (long)l.GetHashCode()).Sum();

    public void Tilt(string direction)
    {
        List<string> DoTilt(List<string> platform){
            var res = new List<string>();
            foreach (var line in platform)
            {
                var newParts = new List<string>();
                var parts = line.Split("#");
                foreach (var part in parts)
                {
                    var numRocks = part.Count(c => c == 'O');
                    newParts.Add(new string('.', part.Length - numRocks) + new string('O', numRocks));
                }

                res.Add(string.Join("#", newParts));
            }

            return res;
        }

        if (direction == "E")
        {
            Lines = DoTilt(Lines);
            return;
        }

        if (direction == "N")
        {
            var rotatedPlatform = Lines.RotateClockwise();
            var res = DoTilt(rotatedPlatform);


            // TODO: Fix Anti-Clockwise rotation
            Lines = res.RotateClockwise().RotateClockwise().RotateClockwise();
            return;
        }

        if (direction == "W")
        {
            var rotatedPlatform = Lines.RotateClockwise().RotateClockwise();
            var res = DoTilt(rotatedPlatform);


            // TODO: Fix Anti-Clockwise rotation
            Lines = res.RotateClockwise().RotateClockwise();
            return;
        }

        if (direction == "S")
        {
            var rotatedPlatform = Lines.RotateClockwise().RotateClockwise().RotateClockwise();
            var res = DoTilt(rotatedPlatform);


            // TODO: Fix Anti-Clockwise rotation
            Lines = res.RotateClockwise();
            return;
        }


    }

    public void Print() => 
        Lines.ForEach(Console.WriteLine);

}
