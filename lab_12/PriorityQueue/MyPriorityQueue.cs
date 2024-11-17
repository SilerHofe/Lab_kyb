using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyArrayList;

namespace PriorityQueue
{
    public class MyPriorityQueue<T> where T : IComparable<T>
    {
        private MyArrayList<T> queue;
        private int size;
        private Comparer<T> comparator;
        public MyPriorityQueue()
        {
            queue = new MyArrayList<T>(11);
            size = 0;
        }
        public MyPriorityQueue(T[] array)
        {
            queue = new MyArrayList<T>(array.Length);
            size = array.Length;
            for (int i = 0; i < array.Length; i++)
                queue.Add(array[i]);
            for (int i = size / 2; i >= 0; i--)
                OrderInHeap(i);
        }
        public MyPriorityQueue(int intialCapacity)
        {
            queue = new MyArrayList<T>(intialCapacity);
            size = 0;
        }
        public MyPriorityQueue(int intialCapacity, Comparer<T> comparator1)
        {

            if (intialCapacity < 0) throw new ArgumentOutOfRangeException();
            queue = new MyArrayList<T>(intialCapacity);
            size = intialCapacity;
            comparator = comparator1;
        }
        public MyPriorityQueue(MyPriorityQueue<T> c)
        {
            T[] mas = new T[c.size];
            for (int i = 0; i < c.size; i++)
            {
                mas[i] = c.queue.Get(i);
            }
            queue = new MyArrayList<T>(c.size);
            size = c.size;
            for (int i = 0; i < c.size; i++)
                queue.Add(mas[i]);
            for (int i = size / 2; i >= 0; i--)
                OrderInHeap(i);
        }
        public void Add(T element)
        {
            queue.Add(element);
            size++;
            for (int i = size / 2; i >= 0; i--)
                OrderInHeap(i);
        }
        public void AddAll(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                this.Add(array[i]);
            }
        }
        public void Clear()
        {
            queue.Clear();
            size = 0;
        }
        public bool Contains(T el) => queue.Contains(el);
        public bool ContainsAll(T[] array) => queue.ContainsAll(array);
        public bool IsEmpty() => queue.IsEmpty();
        public void Remove(params T[] el)
        {
            queue.Remove(el);
            size--;
            for (int i = size / 2; i >= 0; i--)
                OrderInHeap(i);
        }
        public T[] ToArray() => queue.ToArray();
        public T[] ToArray(T[] array) => queue.ToArray(array);
        public T Element() => queue.Get(0);
        public int Size() => queue.Size;
        public bool Offer(T el)
        {
            if (size == queue.Capacity()) return false;
            queue.Add(el);
            for (int i = size / 2; i >= 0; i--)
                OrderInHeap(i);
            return true;
        }
        public T Peek()
        {
            if (size == 0) throw new ArgumentOutOfRangeException();
            else return queue.Get(0);
        }
        public T Poll()
        {
            T a = queue.Get(0);
            queue.Remove(queue.Get(0));
            for (int i = size / 2; i >= 0; i--)
                OrderInHeap(i);
            return a;
        }
        public void OrderInHeap(int i)
        {
            int leftChild;
            int rightChild;
            int parents = i;
            while (true)
            {
                leftChild = 2 * i + 1;
                rightChild = 2 * i + 2;
                if (rightChild < size && queue.Get(rightChild).CompareTo(queue.Get(parents)) > 0)
                    parents = rightChild;
                if (leftChild < size && queue.Get(leftChild).CompareTo(queue.Get(parents)) > 0)
                    parents = leftChild;
                if (parents == i)
                    break;
                Swap(parents, i);
                i = parents;
            }
        }
        private void Swap(int i, int j)
        {
            T temp1 = queue.Get(i);
            T temp2 = queue.Get(j);
            queue.Set(j, temp1);
            queue.Set(i, temp2);
        }
        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{queue.Get(i)} ");
            }
            Console.WriteLine();
        }
    } 
}
