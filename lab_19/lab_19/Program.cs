using Hash;
using System.Text.RegularExpressions;

public class Lab19
{
    static void Main(string[] args)
    {
        MyHashMap<string, int> teg = new MyHashMap<string, int>();
        string path = "input.txt";
        StreamReader sr = new StreamReader(path);
        string? line = sr.ReadLine();
        string pattern = @"(?<=</?)([a-zA-Z][\w-]*)(?=/?>)";

        if (line == null) Console.WriteLine("Строчка пуста");
        while (line != null)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                if (!teg.ContainsKey(match.Value.ToLower()))
                    teg.Put(match.Value.ToLower(), 1);
                else
                    teg.Put(match.Value.ToLower(), teg.Get(match.Value.ToLower()) + 1);
            }

            line = sr.ReadLine();
        }
        sr.Close();
        IEnumerable<KeyValuePair<string, int>> pairs = teg.EntrySet();
        foreach (var pair in pairs)
            Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value}");
        int n =14;
        Console.WriteLine($"1){((n - 1) % 9) + 1}\t2){((n - 1) % 3) + 10}\t3){((n - 1) % 6) + 13}");
    }
}