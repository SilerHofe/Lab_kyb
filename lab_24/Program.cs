using MyLibr;
public class Program
{
    static void Main(string[] args)
    {
        MyTreeSet<int> tree=new MyTreeSet<int>();
        int[] array = {1,2,3,4,5,6,7,8,9,10};
        tree.Add(array);
        int[] array2 = { 2, 1, 12, 18, 76 };
        tree.Print();
        Console.WriteLine("--------------------");
        tree.Remove(10);
        tree.Print();
    }
}