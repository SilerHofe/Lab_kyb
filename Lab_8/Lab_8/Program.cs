using MyStack;
class program
{
    static void Main(string[] args)
    {
        MyStack<int> stack = new MyStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.StekPrint();
        Console.WriteLine(stack.Peek());
        stack.Pop();
        Console.WriteLine(stack.Empty());
        Console.WriteLine(stack.Search(44));
    }
}