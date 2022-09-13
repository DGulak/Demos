using System.Diagnostics;
using System.Text;

namespace DemoConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var test = new List<int>()
            {
                1,3,2,3,4,1
            };

            var tr = BitPatternHS(test);
    
            Bench(10_000_000, 0, 100);

            Bench(10_000_000, 0, 1000);

            Bench(10_000_000, 0, 10000);
        }

        private static List<string> BitPatternHS(List<int> nums)
        {
            var occursEarlier = new char[nums.Count];
            var occursLater = new char[nums.Count];

            HashSet<int> back = new HashSet<int>();
            HashSet<int> front = new HashSet<int>();
            
            int revI = 0;
            for (int i = 0; i < nums.Count; i++)
            {
                revI = nums.Count - 1 - i;

                var num = nums[i];
                var revNum = nums[revI];

                if(!back.Add(num))
                    occursEarlier[i] = '1';
                else
                    occursEarlier[i] = '0';

                if (!front.Add(revNum))
                    occursLater[revI] = '1';
                else
                    occursLater[revI] = '0';

            }

            return new List<string>()
            {
                String.Join(string.Empty, occursEarlier),
                String.Join(string.Empty, occursLater)
            };
        }

        private static List<string> BitPattern(List<int> nums)
        {
            StringBuilder occursEarlier = new StringBuilder();
            StringBuilder occursLater = new StringBuilder();

            for(int i = 0; i < nums.Count; i++)
            {
                int currentNum = nums[i];

                if(nums.IndexOf(currentNum) < i)
                    occursEarlier.Append("1");
                else
                    occursEarlier.Append("0");
                if (nums.LastIndexOf(currentNum) > i)
                    occursLater.Append("1");
                else
                    occursLater.Append("0");
            }

            return new List<string>()
            {
                occursEarlier.ToString(),
                occursLater.ToString()
            };
        }

        private static void Bench(long count, int min, int max)
        {
            var nums = GetRandomList(count, min, max);
            Stopwatch sw = new Stopwatch();

            sw.Start();
            var res = BitPatternHS(nums);
            sw.Stop();
            Console.WriteLine($"HashSet Method: Sequence count: {nums.Count}, time elapsed: {sw.ElapsedMilliseconds} ");

            sw.Restart();
            res = BitPattern(nums);
            sw.Stop();
            Console.WriteLine($"base Method: Sequence count: {nums.Count}, time elapsed: {sw.ElapsedMilliseconds} ");

            Console.WriteLine();
        }

        private static List<int> GetRandomList(long count, int min, int max)
        {
            var res = new List<int>();
            var random = new Random();

            for(int i = 0; i < count; i++)
            {
                res.Add(random.Next(min, max));
            }

            return res;
        }

    }
}



