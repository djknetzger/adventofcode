namespace _01.Carried.Calories
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"input.txt");

            List<int> totalCals = new List<int>();

            var batchTotal = 0;
            foreach (string line in lines)
            {

                if (!string.IsNullOrEmpty(line))
                {
                    batchTotal += int.Parse(line);
                }
                else
                {
                    totalCals.Add(batchTotal);
                    batchTotal = 0;
                }
            }

            Console.WriteLine($"Max Calories: {totalCals.Max()}");

            totalCals.Sort();

            var topThreeCals = totalCals.TakeLast(3).Sum();

            Console.WriteLine($"Sum of top 3 carriers: {topThreeCals}");
        }
    }
}