using MyArrayList;
using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Text;
namespace Task5
{
    public class Program
    {
        static string path = "C:/Users/sashs/source/repos/Lab_5/Lab_5/text.txt";
        static StreamReader sr = new StreamReader(path);
        static bool IsHere(object i, char[] arr)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j] == Convert.ToChar(i)) return true;
            }
            return false;
        }
        static MyArrayList<string> GetTeg()
        {
            bool isOpen = false;
            bool isClose = false;
            string? line = sr.ReadLine();
            if (line == null) { Console.WriteLine("Строчка пуста"); }
            var arrayTeg = new MyArrayList<string>(0);
            string teg = "";
            char[] alf = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 
                'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M' };
            char[] num = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            isOpen = false;
            teg = "";
            for(int i=0;i<line.Length;i++)
            {
                    if (line[i] == '<' && line[i + 1] != null)
                    {
                    if (!isOpen) { isOpen = true; }
                    else { isOpen = false; teg = ""; continue; }
                    }
                    if (line[i] == '>' && isOpen) { isOpen = false; teg += line[i]; isClose = true; }
                    if (isOpen && (line[i] == '<' || line[i] == '>' || line[i] == '/' || IsHere(line[i],alf) || IsHere(line[i],num))) teg += line[i];
                    if (isClose) { arrayTeg.Add(teg); teg = ""; isClose = false; }
            }
            return arrayTeg;
        }
        static string SlashDelete(string element)
        {
                if (element[1] == '/')
                {
                    element = $"<{element.Substring(2)}";
                }
            return element;
        }
        static MyArrayList<string> DeleteDuplicate(MyArrayList<string> arrayTeg)
        {
            MyArrayList<string> tegDublicate = new MyArrayList<string>(0);
            MyArrayList<string> uniqueTeg = new MyArrayList<string>(0);
            string lowerCaseTeg;
            string dublicate;
            for (int i = 0; i < arrayTeg.Size(); i++)
            {
                lowerCaseTeg = arrayTeg.Element(i);
                dublicate = lowerCaseTeg.ToLower();
                tegDublicate.Add(dublicate);
            }

            for (int i = 0; i < tegDublicate.Size(); i++)
            {
                tegDublicate.Set(i, SlashDelete(tegDublicate.Element(i)));
            }
            for (int i = 0; i < tegDublicate.Size(); i++)
            {
                for (int j = i + 1; j < tegDublicate.Size(); j++)
                {

                    if (SlashDelete(tegDublicate.Element(i)) == SlashDelete(tegDublicate.Element(j)))
                    {
                        tegDublicate.Remove(tegDublicate.Element(j));
                    }
                }
            }

            for (int i = 0; i < tegDublicate.Size(); i++)
            {
                uniqueTeg.Add(tegDublicate.Element(i));
            }
            return uniqueTeg;
        }
        static void Main(string[] args)
        {
            var uniqueTeg = new MyArrayList<string>(0);
            var teg = new MyArrayList<string>(0);
            teg = GetTeg();
            teg.ArrayPrint();
            uniqueTeg=DeleteDuplicate(teg);
            uniqueTeg.ArrayPrint();
        }
    }
}