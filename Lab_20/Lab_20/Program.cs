using Hash;
using System.Net.Security;
using System.Text.RegularExpressions;

public class Definition
{   
    enum Type
    {
        Int,
        Double,
        Float
    }
    static void Main(string[] args)
    {
        try
        {
            MyHashMap<string, string> definition = new MyHashMap<string, string>();
            string path = "input.txt";
            string pattern = @"(double|int|float) \S* ?(?:=) ?(\S)+?(?=;)";
            StreamReader sr = new StreamReader(path);
            string? line = sr.ReadLine();
            while (line != null)
            {
                MatchCollection matches = Regex.Matches(line, pattern);
                foreach (Match match in matches)
                {
                    string[] part = match.Value.Split(' ');
                    string type = part[0].Trim();
                    string value = part[3].Trim();
                    string name = part[1].Trim();
                    string typeValue = type + " " + value;
                    if (definition.ContainsKey(name))
                        Console.WriteLine("Repeat" + " " + $"{type} {name} = {value}");
                    else
                        definition.Put(name, typeValue);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            var pairs = definition.EntrySet();
            foreach (var pair in pairs)
                Console.WriteLine($"{pair.Key} : {pair.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exeption: " + ex.Message);
        }
    }
}