using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyVector;

namespace MyStack
{
    public class MyStack<T> : MyVector<T>
    {
        private MyVector<T> stack;
        private int top;
        public MyStack()
        {
            top = -1;
            stack = new MyVector<T>();
        }
        public void Push(T element)
        {
            top++;
            stack.Add(element);
        }
        public T Pop()
        {
            T tmp = stack.Element(top);
            stack.Remove(stack.LastElemnet());
            top--;
            return tmp;
        }
        public T Peek()
        {
            if (top == -1) throw new Exception("Стэк пуст");
            return stack.Get(top);
        }
        public bool Empty()
        {
            return stack.IsEmpty();
        }
        public int Search(T element)
        {
            for (int i = top - 1, j = 1; i >= 0; i--, j++)
            {
                if (stack.Element(i).Equals(element)) return j;
            }
            return -1;
        }
        public void StekPrint()
        {
            for (var i = top; i >= 0; i--)
            {
                Console.Write($"{stack.Element(i)} ");
            }
            Console.WriteLine();
        }
    }
}
