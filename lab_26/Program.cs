using MyLibr;
using System.Reflection;
using System.Xml.Linq;

namespace Task25
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "input.txt";
            if (path == null) throw new Exception("path is null");
            MyHashSet<string> set = new MyHashSet<string>();
            try
            {
                StreamReader sr = new StreamReader(path);
                string? line = sr.ReadLine();
                while (line != null)
                {
                    string?[] words = line.Split(' ');
                    foreach (string word in words)
                    {
                        if (word == null || word == " ") continue;
                        set.Add(word.ToLower());
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex) { Console.WriteLine("Exeption: " + ex.Message); };
            foreach (string word in set.ToArray()) Console.WriteLine(word);
        }
    }
}