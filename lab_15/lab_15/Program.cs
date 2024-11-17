using MyArrayDeque;

namespace lab_15
{
    public class Programm
    {
        public static int CountOfC(string line)
        {
            int count = 0;
            string[] numbers =line.Split(' ');
            foreach (string number in numbers)
            {
                if (int.Parse(number) > 9 && int.Parse(number) < 100) count++;
            }
            return count;
        }
        public static int CountOfS(string line)
        {
            int count = 0;
            foreach (char symb in line)
            {
                if(symb == ' ')count++;
            }
            return count;
        }
        public static void Main(string[] args)
        {
            MyArrayDeque<string> deque = new MyArrayDeque<string>(0);
            string path1 = "input.txt";
            string path2 = "sorted.txt";
            try
            {
                StreamReader sr = new StreamReader(path1);
                StreamWriter sw = new StreamWriter(path2);
                string? line = sr.ReadLine();
                if (line != null) deque.Add(line);
                while (line != null)
                {
                    line=sr.ReadLine();
                    if (line != null)
                    {
                        if (CountOfC(line) > CountOfC(deque.GetFirst())) deque.AddLast(line);
                        else deque.AddFirst(line);
                    }
                }
                sr.Close();
                for (int i = 0; i < deque.Size(); i++)
                {
                    sw.WriteLine(deque.Get(i));
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            int n;
            n = Convert.ToInt16(Console.ReadLine());
            for (int i=0;i<deque.Size();i++)
            {
                string tmp=deque.Get(i);
                if (CountOfS(deque.Get(i)) > n) deque.Remove(tmp);
            }
            for (int i = 0; i < deque.Size(); i++)
            {
                Console.WriteLine(deque.Get(i));
            }

        }
    }
}