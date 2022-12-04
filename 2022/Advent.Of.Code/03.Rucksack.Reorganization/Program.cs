namespace _03.Rucksack.Reorganization
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"input.txt");

            var priorityPoints = TotalPriorityOfMispackedRucks(lines);

            Console.WriteLine($"Total Priority Points (Part One): {priorityPoints}");

            var badgeItemPoints = TotalPriorityOfBadgeItems(lines);

            Console.WriteLine($"Total Badge Item Priority Points (Part Two): {badgeItemPoints}");
        }

        internal static int TotalPriorityOfMispackedRucks(string[] lines)
        {
            var priorityPoints = 0;
            foreach (var line in lines)
            {
                var firstHalf = line.Take(line.Length / 2);
                var secondHalf = line.Skip(line.Length / 2);

                var intersections = firstHalf.Intersect(secondHalf);

                priorityPoints += GetIntersectionPriority(intersections.First());
            }

            return priorityPoints;
        }

        internal static int TotalPriorityOfBadgeItems(string[] lines)
        {
            var priorityPoints = 0;

            var index = 0;
            var triples = lines
                .GroupBy(x => index++ / 3)
                .Select(x => x.ToList())
                .ToList();

            foreach (var triple in triples)
            {
                var firstIntersections = triple[0].Intersect(triple[1]);
                var finalIntersections = firstIntersections.Intersect(triple[2]);

                priorityPoints += GetIntersectionPriority(finalIntersections.First());
            }

            return priorityPoints;
        }

        internal static int GetIntersectionPriority(char intersection)
        {
            var asciiValue = Convert.ToInt32(intersection);

            return char.IsUpper(intersection) ? asciiValue - 38 : asciiValue - 96;
        }
    }
}