using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace MyLinketList
{
    public class MyLinkedList<T>
    {
        private class MyLLElement<T>
        {
            public T value;
            public MyLLElement<T> next;
            public MyLLElement<T> prev;
            public MyLLElement(T element)
            {
                next = null;
                prev = next;
                value = element;
            }

        }
        private MyLLElement<T> first;
        private MyLLElement<T> last;
        private int size;

        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
        }

        public MyLinkedList(T[] array)
        {
            foreach (T item in array)
                Add(item);
        }

        public MyLinkedList(int capacity)
        {
            first = null;
            last = null;
            size = capacity;
        }

        public void Add(T item)
        {
            MyLLElement<T> element = new MyLLElement<T>(item);
            if (size == 0)
            {
                first = element;
                last = element;
            }
            else
            {
                last.next = element;
                element.prev = last;
                last = element;
            }
            size++;
        }

        public void AddAll(T[] array)
        {
            foreach (T item in array)
                Add(item);
        }

        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }

        public bool Contains(T item)
        {
            MyLLElement<T> step = first;
            while (step != null)
            {
                if (step.value.Equals(item))
                    return true;
                step = step.next;
            }
            return false;
        }

        public bool ContainsAll(T[] array)
        {
            bool[] newArray = new bool[array.Length];
            MyLLElement<T> step = first;
            while (step != null)
            {
                int cnt = 0;
                if (step.Equals(array[cnt]))
                    newArray[cnt] = true;
                cnt++;
                step = step.next;
            }
            for (int i = 0; i < newArray.Length; i++)
                if (!newArray[i])
                    return false;
            return true;
        }

        public void Remove(T item)
        {
            if (Contains(item))
            {
                if (first.value.Equals(item))
                {
                    first = first.next;
                    size--;
                    return;
                }
                MyLLElement<T> step = first;
                while (step != null)
                {
                    if (step.next.value.Equals(item))
                    {
                        step = step.next;
                        size--;
                        return;
                    }
                    else
                        step = step.next;
                }
            }
        }

        public void RemoveAll(T[] array)
        {
            foreach (T item in array)
                Remove(item);
        }

        public void RetainAll(T[] array)
        {
            T[] newArray = new T[array.Length];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                int fl = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    if (Get(i).Equals(array[j]))
                    {
                        fl = 0;
                        break;
                    }
                    else
                        fl = 1;
                }
                if (fl == 1)
                    Remove(Get(i));
            }
        }

        public T[] ToArray()
        {
            T[] newArray = new T[size];
            for (int i = 0; i < size; i++)
                newArray[i] = Get(i);
            return newArray;
        }

        public T[] ToArray(T[] array)
        {
            if (array == null)
                return ToArray();
            else
            {
                T[] newArray = new T[array.Length + size];
                for (int i = 0; i < array.Length; i++)
                    newArray[i] = array[i];
                for (int i = array.Length; i < newArray.Length; i++)
                    newArray[i] = Get(i);
                return newArray;
            }
        }

        public void Add(int index, T item)
        {
            if (index == 0)
            {
                MyLLElement<T> step = new MyLLElement<T>(item);
                step.next = first;
                first.prev = step;
                first = step;
                return;
            }
            else if (index == size - 1)
            {
                MyLLElement<T> step = new MyLLElement<T>(item);
                step.prev = last;
                last.next = step;
                last = step;
                return;
            }
            else
            {
                MyLLElement<T> step = new MyLLElement<T>(item);
                step = first;
                int cnt = 0;
                while (cnt != index)
                {
                    step = step.next;
                    cnt++;
                }
                if (cnt == index)
                {
                    MyLLElement<T> element = new MyLLElement<T>(item);
                    element.next = step;
                    element.prev = step.prev;
                    step.prev.next = element;
                    step.prev = element;
                }
            }
        }

        public void AddAll(int index, T[] array)
        {
            foreach (T item in array)
                Add(index, item);
        }

        public T Get(int index)
        {
            int current = 0;
            if (index >= size)
                throw new IndexOutOfRangeException();
            if (index == size - 1)
                return last.value;
            if (index == 0)
                return first.value;
            MyLLElement<T> step = first;
            while (current != index)
            {
                step = step.next;
                current++;
            }
            return step.value;
        }

        public int IndexOf(T item)
        {
            int i = 0;
            MyLLElement<T> step = new MyLLElement<T>(item);
            step = first;
            while (step != null)
            {
                if (step.value.Equals(item))
                    return i;
                i++;
                step = step.next;
            }
            return -1;
        }

        public int LastIndexOf(T item)
        {
            int i = 0;
            int retI = -1;
            MyLLElement<T> step = new MyLLElement<T>(item);
            step = first;
            while (step != null)
            {
                if (step.value.Equals(item))
                    retI = i;
                i++;
                step = step.next;
            }
            return retI;
        }

        public T Remove(int index)
        {
            T item = Get(index);
            Remove(item);
            return item;
        }

        public void Set(int index, T item)
        {
            MyLLElement<T> step = new MyLLElement<T>(item);
            step = first;
            int ind = 0;
            while (ind != index)
            {
                ind++;
                step = step.next;
            }
            step.value = item;
        }

        public T[] SubList(int fromIndex, int toIndex)
        {
            T[] array = new T[toIndex - fromIndex + 1];
            int index1 = 0;
            int index2 = 0;
            MyLLElement<T> step = new MyLLElement<T>(first.value);
            step = first;
            while (index1 != fromIndex)
            {
                step = step.next;
                index1++;
            }
            while (index1 <= toIndex)
            {
                index1++;
                index2++;
                array[index2] = step.value;
                step = step.next;
            }
            return array;
        }

        public bool Offer(T item)
        {
            Add(item);
            if (Contains(item))
                return true;
            return false;
        }

        public T Peek()
        {
            if (first == null)
                throw new NullReferenceException();
            return first.value;
        }

        public T Poll()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        public T GetFirst()
        {
            if (first == null)
                throw new NullReferenceException();
            return first.value;
        }

        public T GetLast()
        {
            if (first == null)
                throw new NullReferenceException();
            return last.value;
        }

        public bool OfferFirst(T item)
        {
            AddFirst(item);
            if (Contains(item))
                return true;
            return false;
        }

        public bool OfferLast(T item)
        {
            AddLast(item);
            if (Contains(item))
                return true;
            return false;
        }

        public T Pop()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        public T PeekFirst()
        {
            if (size == 0)
                throw new Exception();
            return first.value;
        }

        public T PeekLast()
        {
            if (size == 0)
                throw new Exception();
            return last.value;
        }

        public T PollFirst()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        public T PollLast()
        {
            T item = last.value;
            Remove(item);
            return item;
        }

        public T RemoveLast()
        {
            T item = last.value;
            Remove(item);
            return item;
        }

        public T RemoveFirst()
        {
            T item = first.value;
            Remove(item);
            return item;
        }

        public bool RemoveLastOccurence(T item)
        {
            int index = LastIndexOf(item);
            if (index != -1)
            {
                Remove(index);
                return true;
            }
            return false;
        }

        public bool RemoveFirstOccurence(T item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                Remove(index);
                return true;
            }
            return false;
        }

        public bool IsEmpty() => size == 0;
        public int Size() => size;
        public T Element() => first.value;
        public void AddFirst(T item) => Add(0, item);
        public void AddLast(T item) => Add(size - 1, item);
        public void Push(T item) => AddFirst(item);

        public void Print()
        {
            Console.WriteLine(this);
        }
        public override string ToString()
        {
            string path = "";
            MyLLElement<T> step = new MyLLElement<T>(first.value);
            step = first;
            while (step != null)
            {
                if (step.next != null)
                {
                    path += step.value + ", ";
                    step = step.next;
                }
                else
                {
                    path += step.value;
                    step = step.next;
                }
            }
            return path;
        }
    }

}