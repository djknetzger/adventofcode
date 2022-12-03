namespace _02.Rock.Paper.Scissors
{
    internal class Program
    {
        static Dictionary<string, ThrowType> OppThrowTypes = new Dictionary<string, ThrowType> {
            { "A", ThrowType.Rock },
            { "B", ThrowType.Paper },
            { "C", ThrowType.Scissors }
        };

        static Dictionary<string, Throw> MyThrows = new Dictionary<string, Throw>
        {
            {
                "X",
                new Throw {
                    Type = ThrowType.Rock,
                    PointValue = 1,
                    BeatenThrow = ThrowType.Scissors
                }
            },
            {
                "Y",
                new Throw {
                    Type = ThrowType.Paper,
                    PointValue = 2,
                    BeatenThrow = ThrowType.Rock
                }
            },
            {
                "Z",
                new Throw {
                    Type = ThrowType.Scissors,
                    PointValue = 3,
                    BeatenThrow = ThrowType.Paper
                }
            }
        };

        static Dictionary<string, Dictionary<ThrowType, Throw>> ResultMap = new Dictionary<string, Dictionary<ThrowType, Throw>>
        {
            {
                "X", // Lose
                new Dictionary<ThrowType, Throw>
                {
                    { ThrowType.Rock, MyThrows.GetValueOrDefault("Z") },
                    { ThrowType.Paper, MyThrows.GetValueOrDefault("X") },
                    { ThrowType.Scissors, MyThrows.GetValueOrDefault("Y") }
                }
            },
            {
                "Y", // Draw
                new Dictionary<ThrowType, Throw>
                {
                    { ThrowType.Rock, MyThrows.GetValueOrDefault("X") },
                    { ThrowType.Paper, MyThrows.GetValueOrDefault("Y") },
                    { ThrowType.Scissors, MyThrows.GetValueOrDefault("Z") }
                }
            },
            {
                "Z", // Win
                new Dictionary<ThrowType, Throw>
                {
                    { ThrowType.Rock, MyThrows.GetValueOrDefault("Y") },
                    { ThrowType.Paper, MyThrows.GetValueOrDefault("Z") },
                    { ThrowType.Scissors, MyThrows.GetValueOrDefault("X") }
                }
            }
        };

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"input.txt");

            var partOnePoints = GetPartOnePoints(lines);

            Console.WriteLine($"Total Points (Part 1): {partOnePoints}");

            var partTwoPoints = GetPartTwoPoints(lines);

            Console.WriteLine($"Total Points (Part 1): {partTwoPoints}");
        }

        #region Part One

        static int GetPartOnePoints(string[] lines)
        {
            var pairs = lines.Select(line => ConvertLineToObjectsPartOne(line)).ToList();

            var totalPoints = 0;
            foreach (var pair in pairs)
            {
                totalPoints += pair.Value.PointsAwarded(pair.Key);
            }

            return totalPoints;
        }

        static KeyValuePair<ThrowType, Throw> ConvertLineToObjectsPartOne(string line)
        {
            var splitLine = line.Split(' ');

            var first = splitLine[0];
            var second = splitLine[1];

            var oppThrowType = OppThrowTypes.GetValueOrDefault(first);
            var myThrow = MyThrows.GetValueOrDefault(second);

            return new KeyValuePair<ThrowType, Throw>(oppThrowType, myThrow);
        }

        #endregion

        #region Part Two

        static int GetPartTwoPoints(string[] lines)
        {
            var pairs = lines.Select(line => ConvertLineToObjectsPartTwo(line)).ToList();

            var totalPoints = 0;
            foreach (var pair in pairs)
            {
                totalPoints += pair.Value.PointsAwarded(pair.Key);
            }

            return totalPoints;
        }

        static KeyValuePair<ThrowType, Throw> ConvertLineToObjectsPartTwo(string line)
        {
            var splitLine = line.Split(' ');

            var first = splitLine[0];
            var second = splitLine[1];

            var oppThrowType = OppThrowTypes.GetValueOrDefault(first);
            var myThrow = ResultMap.GetValueOrDefault(second).GetValueOrDefault(oppThrowType);

            return new KeyValuePair<ThrowType, Throw>(oppThrowType, myThrow);
        }

        #endregion

        internal class Throw
        {
            internal ThrowType Type { get; set; }

            internal int PointValue { get; set; }

            internal ThrowType BeatenThrow { get; set; }

            public int PointsAwarded(ThrowType throwType)
            {
                if (throwType == BeatenThrow)
                {
                    return PointValue + 6;
                }
                else if (throwType == Type) 
                {
                    return PointValue + 3;
                }
                else
                {
                    return PointValue;
                }
            }
        }

        internal enum ThrowType {
            Rock,
            Paper,
            Scissors
        }
    }
}