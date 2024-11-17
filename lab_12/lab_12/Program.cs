using PriorityQueue;
using System.Diagnostics;

namespace lab_12
{
    public class Request: IComparable<Request>
    {
        public int priority {  get; set; }
        public int number {  get; set; }
        public int step { get; set; }
        public Request(int _priority, int _number,int _step) {
            priority = _priority;
            number = _number;
            step = _step;
        }
        public int CompareTo(Request other)
        {
            return priority.CompareTo(other.priority);
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "C:/Users/sashs/source/repos/lab_12/log.txt";
            MyPriorityQueue<Request> queue = new MyPriorityQueue<Request>();
            Console.WriteLine($"Введите количество шагов");
            int n = Convert.ToInt32(Console.ReadLine());
            int cnt = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < n; i++)
            {
                Random rnd = new Random();
                int num=rnd.Next(1,11);
                for (int j = 0; j < num; j++)
                {
                    int priority = rnd.Next(1, 6);
                    Request example = new Request(priority, j, i);
                    queue.Add(example);
                    sw.WriteLine($"ADD {example.number} {example.priority} {example.step}");
                    cnt++;
                }
            }
            for (int i=0; i < cnt; i++)
            {
                Request tmp = queue.Peek();
                sw.WriteLine($"REMOVE {tmp.number} {tmp.priority} {tmp.step}");
                queue.Remove(tmp);
            }
            stopwatch.Stop();
            sw.WriteLine($"Затраченное время {stopwatch.Elapsed}");
            sw.Close();
        }
    }
}