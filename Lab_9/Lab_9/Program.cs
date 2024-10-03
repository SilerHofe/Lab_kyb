using MyStack;
using RPN;

namespace Lab_9
{
    class Program
    {
        static void Main(string[] args)
        {
            RPN.RPN rPN = new RPN.RPN();
            while (true)
            {
                Console.WriteLine("Введите выражение: ");
                Console.WriteLine(RPN.RPN.Calculate(Console.ReadLine()));
            }
        }
    }
}