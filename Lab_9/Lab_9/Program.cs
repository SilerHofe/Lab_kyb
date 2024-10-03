using MyStack;
using RPN;

namespace Lab_9
{
    class Program
    {
        static void Main(string[] args)
        {
            rpn rpn = new rpn();
            while (true)
            {
                Console.WriteLine("Введите выражение: ");
                Console.WriteLine(rpn.Calculate(Console.ReadLine()));
            }
        }
    }
}