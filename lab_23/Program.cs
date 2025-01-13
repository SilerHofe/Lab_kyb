using MyLibr;

public class Project
{
    static void Main(string[] args)
    {
        MyHashSet<string> hashSet = new MyHashSet<string>();
        string[] array = { "Pampilus", "Bi-2", "Nautilus"};
        hashSet.AddAll(array);
        hashSet.Remove("Bi-2");
        
        Console.Write(hashSet);
    }
}