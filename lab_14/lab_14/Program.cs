using Program;
using System;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace Program
{
    public class MyArrayDeque<T>
    {
        T[] elements;
        int head;
        int tail;

        public MyArrayDeque()
        {

            head = 0;
            tail = 15;
        }
        public MyArrayDeque(T[] a)
        {
            int n = a.Length;
            elements = new T[n];

            head = 0;
            tail = n;
            for (int i = 0; i < n; i++) elements[i] = a[i];
        }
        public MyArrayDeque(int numElements)
        {

            head = 0;
            tail = numElements;
        }
        public void Add(T e)
        {
            if (tail == elements.Length)
            {
                T[] array = new T[(tail * 2) + 1];
                for (int i = 0; i < tail; i++) array[i] = elements[i];
                elements = array;
                elements[tail++] = e;
            }
            else
            {
                elements[tail++] = e;
            }
        }
        public void AddAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Add(a[i]);
        }
        public void Clear(T e)
        {
            tail = 0;
        }
        public bool Contains(object o)
        {

            for (int i = 0; i < tail; i++)
            {
                foreach (T t in elements)
                {
                    if (Equals(t, o)) return true;
                }
            }
            return false;
        }
        public bool ContainsAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (Contains(a[i])) return true;
            }
            return false;
        }
        public bool IsEmpty(T e)
        {
            return tail == 0;
        }
        private int FindIndex(object o)
        {
            for (int i = 0; i < tail; i++)
            {
                if (o.Equals(elements[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public void Remove(T o)
        {
            if (Contains(o))
            {
                int k = FindIndex(o);
                int tmp = tail - 1;
                T[] array = new T[tmp];
                for (int i = 0; i < k; i++) array[i] = elements[i];
                for (int i = k; i < tmp; i++) array[i] = elements[i+1];
                elements = array;
                tail--;
            }

        }
        public void RemoveAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Remove(a[i]);

        }
        public void RetainAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) elements[i] = a[i];
            tail = a.Length;
        }
        public int Size()
        {
            return tail;
        }
        public void ToArray()
        {
            T[] array = new T[tail];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = elements[i];
            }
        }
        public void ToArray(T[] a)
        {
            for (int i = 0; i < tail; i++)
            {
                Add(a[i]);
            }
            tail += a.Length;
            T[] array = new T[tail];
            for (int i = 0; i < array.Length; i++) array[i] = elements[i];
        }
        public T Element()
        {
            return elements[head];
        }
        public bool Offer(T obj)
        {
            Add(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public T Peek()
        {
            if (tail == 0) return default(T);
            else return elements[0];
        }
        public T Poll()
        {
            if (tail == 0) return default(T);
            else
            {
                T q = elements[0];
                Remove(elements[0]);
                return q;
            }
        }
        public void AddFirst(T obj)
        {
            T[] array = new T[tail + 1];
            array[0] = obj;
            for (int i = 1; i <= tail; i++) array[i] = elements[i];
            elements = array;
            tail += 1;

        }
        public void AddLast(T obj)
        {
            Add(obj);
        }
        public T GetFirst()
        {
            return elements[head];
        }
        public T GetLast()
        {
            return elements[tail - 1];
        }
        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public bool OfferLast(T obj)
        {
            AddLast(obj);
            if (Contains(obj)) return true;
            else return false;
        }
        public T Pop()
        {
            T element = elements[head];
            Remove(element);
            return element;
        }
        public void Push(T obj)
        {
            T[] array = new T[tail + 1];
            array[0] = obj;
            for (int i = 1; i <= tail; i++) array[i] = elements[i];
            elements = array;
            tail += 1;
        }
        public T PeekFirst()
        {
            if (tail == 0) return default(T);
            else return elements[head];
        }
        public T PeekLast()
        {
            if (tail == 0) return default(T);
            else return elements[tail - 1];
        }
        public T PollFirst()
        {
            if (tail == 0) return default(T);
            else
            {
                T q = elements[head];
                Remove(elements[head]);
                return q;
            }
        }
        public T PollLast()
        {
            if (tail == 0) return default(T);
            else
            {
                T q = elements[tail - 1];
                Remove(elements[tail - 1]);
                return q;
            }
        }
        public T RemoveLast()
        {
            T element = elements[tail - 1];
            Remove(elements[tail - 1]);
            return element;
        }

        public T RemoveFirst()
        {
            T element = elements[head];
            Remove(elements[head]);
            return element;
        }
        public bool RemoveLastOccurrence(object obj)
        {
            int index = -1;
            for (int i = 0; i < tail; i++) if (obj.Equals(elements[i])) index = i;
            if (index > -1)
            {
                T[] array = new T[tail--];
                for (int i = 0; i < index; i++) array[i] = elements[i];
                for (int i = index + 1; i < tail--; i++) array[i] = elements[i];
                elements = array;
                tail--;
                return true;
            }
            else return false;
        }
        public bool RemoveFirstOccurrence(object obj)
        {
            int index = -1;
            for (int i = 0; i < tail; i++) if (obj.Equals(elements[i])) { index = i; break; }
            if (index > -1)
            {
                T[] array = new T[tail--];
                for (int i = 0; i < index; i++) array[i] = elements[i];
                for (int i = index + 1; i < tail--; i++) array[i] = elements[i];
                elements = array;
                tail--;
                return true;
            }
            else return false;
        }
        public void Print()
        {
            for (int i = 0;i<tail;i++)
            {
                Console.Write($"{elements[i]} ");
            }
            Console.WriteLine();
        }
        public override string ToString()
        {
            string s = "";
            foreach (T t in elements)
            {
                s += t + " ";
            }
            return s;
        }

    }
    public class Programm
    {
        public static void Main(string[] args)
        {
            int[] array = { 1, 2, 4, 7, 1, 8, 6 };
            MyArrayDeque<int> list = new MyArrayDeque<int>(array);
            list.Add(2);
            list.Print();
            list.Remove(4);
            list.Print();
        }
    }
}
