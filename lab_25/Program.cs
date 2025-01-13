using System.Reflection;
using System.Xml.Linq;
using MyLibr;
namespace lab_25
{
    class Program
    {
        static public void Sorting(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int indexOfmin = i;
                string? minString = array[i];
                for (int j = i + 1; j < array.Length; j++)
                {
                    string? current = array[j];
                    bool ifChange = false;
                    for (int k = 0; k < current.Length && k < minString.Length; k++)
                    {
                        if (current[k] == ' ' && minString[k] != ' ')
                        {
                            minString = current;
                            indexOfmin = j;
                            ifChange = true;
                            break;
                        }
                        if (current[k] != ' ' && minString[k] == ' ')
                        {
                            ifChange = true;
                            break;
                        }
                    }
                    if (!ifChange)
                    {
                        minString = minString.Length < current.Length ? minString : current;
                        indexOfmin = minString.Length < current.Length ? indexOfmin : j;
                    }
                }
                array[indexOfmin] = array[i];
                array[i] = minString;
            }
        }

        static void Main()
        {
            string path = "input.txt";
            if (path == null) throw new Exception("path is null");
            MyHashSet<string> stringSet = new MyHashSet<string>();
            try
            {
                StreamReader sr = new StreamReader(path);
                string? line = sr.ReadLine();
                while (line != null)
                {
                    stringSet.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex) { Console.WriteLine("Exeption: " + ex.Message); };
            string[] stringArray = stringSet.ToArray();
            Sorting(stringArray);
            foreach (string str in stringArray) Console.WriteLine(str);
        }
    }
}