using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace lab_16
{
    public class Node<T>
    {
        public T data { get; set; }
        public Node<T> prev { get; set; }
        public Node<T> next { get; set; }
        public Node(T Data)
        {
            data = Data;
            prev = null;
            next = null;
        }
    }
    public class MyLinketList<T>
    {
        Node<T> first;
        Node<T> last;
        int size;
        public MyLinketList()
        {
            first = null;
            last = null;
            size = 0;
        }
        public MyLinketList(T[] a)
        {
            for (int i=0;i<a.Length; i++)
            {
                Node<T> node = new Node<T>(a[i]);
                if (last == null)
                {
                    last = node;
                    first = node;
                }
                else
                {
                    node.prev = last;
                    last.next= node;
                    last = node;
                }
            }
            size= a.Length;
        }
        public void Add(T e)
        {
            Node<T> node= new Node<T>(e);
            if (last == null)
            {
                last = node;
                first = node;
            }
            else
            {
                node.prev= last;
                last.next = node;
                last = node;
            }
            size++;
        }
        public void AddAll(T[] a)
        {
            for (int i=0;i< a.Length;i++) Add(a[i]);
        }
        public void Clear()
        {
            last = null;
            first = null;
            size = 0;
        }
        public bool Contains(T e)
        {
            Node<T> p= first;
            while (p != null)
            {
                if(p.data.Equals(e)) return true;
                p = p.next;
            }return false;
        }
        public bool ContainsAll(T[] a)
        {
            for (int i=0;i< a.Length; i++)
            {
                if (!Contains(a[i])) return false;
            }return true;
        }
        public bool IsEmpty() => size == 0;
        public void Remove(T e)
        {
            if (Contains(e))
            {
                if (first.data.Equals(e))
                {
                    first = first.next;
                    size--;
                    return;
                }
                if (last.data.Equals(e))
                {
                    last.prev.next = null;
                    last = last.prev;
                    size--;
                    return;
                }
                Node<T> p = first;
                while (p.next != null)
                {
                    if (p.data.Equals(e))
                    {
                        p.prev.next = p.next;
                        p.next.prev = p.prev;
                        size--;
                        return;
                    }
                    else p = p.next;
                }
            }
        }
        public void RemoveAll(T[] a)
        {
            foreach(T e in a)
            {
                Remove(e);
            }
        }
        public int Size() => size;
        public void RetainAll(T[] a)
        {
            if (ContainsAll(a))
            {
                Clear();
                AddAll(a);
            }else
            {
                throw new InvalidOperationException("Эллементы не содержатся в списке");
            }
        }
        public T Get(int ind)
        {
            int curInd = 0;
            if (ind < 0) throw new ArgumentOutOfRangeException();
            if(ind >= size) throw new ArgumentOutOfRangeException();
            if (ind == size - 1) return last.data;
            if (ind==0)return first.data;
            Node<T> p = first;
            while (curInd != ind)
            {
                p= p.next;
                curInd++;
            }return p.data;
        }
        public T[] ToArray()
        {
            T[] newA= new T[size];
            for (int i = 0; i < size; i++)
            {
                newA[i]= Get(i);
            }return newA;
        }
        public T[] ToArray(T[]? a)
        {
            if (a == null) return ToArray();
            else
            {
                T[]newA= new T[a.Length+size];
                for (int i = 0; i < a.Length; i++)
                {
                    newA[i] = a[i];
                }
                for(int i = a.Length; i < newA.Length; i++)
                {
                    newA[i]= Get(i);
                }return newA;
            }
        }
        public T Element() => first.data;
        public T Peek()
        {
            if (first == null)
                return default(T);
            return first.data;
        }
        public T Poll()
        {
            T obj = first.data;
            Remove(first.data);
            return obj;
        }
        public T GetFirst()
        {
            if (first == null)
                throw new IndexOutOfRangeException();
            return first.data;
        }
        public T GetLast()
        {
            if (last == null)
                throw new IndexOutOfRangeException();
            return last.data;

        }
        public T PeekFirst()
        {
            if (size == 0)
                return default(T);
            return first.data;
        }
        public T PeekLast()
        {
            if (size == 0)
                return default(T);
            return first.data;
        }
        public T PollFirst()
        {
            T obj = first.data;
            Remove(first.data);
            return obj;
        }
        public T PollLast()
        {
            T obj = last.data;
            Remove(last.data);
            return obj;
        }
        public T RemoveFirst()
        {
            T obj = first.data;
            Remove(first.data);
            return obj;
        }
        public T RemoveLast()
        {
            T obj = last.data;
            Remove(last.data);
            return obj;
        }
        public T Pop()
        {
            T obj = first.data;
            Remove(first.data);
            return obj;
        }
        public bool Offer(T obj)
        {
            Add(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public void Add(int index, T obj)
        {
            if (index == 0)
            {
                Node<T> step = new Node<T>(obj);
                step.next = first;
                first.prev = step;
                first = step;
                return;
            }
            else if (index == size - 1)
            {
                Node<T> step = new Node<T>(obj);
                step.prev = last;
                last.next = step;
                last = step;
                return;
            }
            else
            {
                int tind = 0;
                Node<T> step = new Node<T>(obj);
                step = first;
                while (tind != index)
                {
                    step = step.next;
                    tind++;
                }
                if (tind == index)
                {
                    Node<T> el = new Node<T>(obj);
                    el.next = step;
                    el.prev = step.prev;
                    step.prev.next = el;
                    step.prev = el;
                }
            }
        }
        public void AddAll(int index, T[] a)
        {
            for (int i = a.Length - 1; i >= 0; i--)
                Add(index, a[i]);
        }
        public int IndexOf(T o)
        {
            Node<T> step = new Node<T>(o);
            step = first;
            int i = 0;
            while (step != null)
            {
                if (step.data.Equals(o))
                    return i;
                i++;
                step = step.next;
            }
            return -1;
        }
        public int LastIndexOf(T obj)
        {
            Node<T> step = new Node<T>(obj);
            step = first;
            int retInd = -1;
            int ind = 0;
            while (step != null)
            {
                if (step.data.Equals(obj)) retInd = ind;
                ind++;
                step = step.next;
            }
            return retInd;
        }
        public T Remove(int index)
        {
            T obj = Get(index);
            Remove(obj);
            return obj;
        }
        public void Set(int index, T obj)
        {
            Node<T> step = new Node<T>(obj);
            step = first;
            int ind = 0;
            while (ind != index)
            {
                ind++;
                step = step.next;
            }
            step.data = obj;
        }
        public T[] SubList(int fromIndex, int toIndex)
        {
            T[] a = new T[toIndex - fromIndex + 1];
            Node<T> step = new Node<T>(first.data);
            step = first;
            int ind1 = 0;
            while (ind1 != fromIndex)
            {
                step = step.next;
                ind1++;
            }
            int ind2 = 0;
            while (ind1 <= toIndex)
            {
                ind2++;
                ind1++;
                a[ind2] = step.data;
                step = step.next;
            }
            return a;

        }
        public void AddFirst(T obj)
        {
            Add(0, obj);
        }
        public void AddLast(T obj)
        {
            Add(size - 1, obj);
        }
        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public bool OfferLast(T obj)
        {
            AddLast(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public void Push(T obj)
        {
            AddFirst(obj);
        }
        public bool RemoveLastOccurrence(T obj)
        {
            int ind = LastIndexOf(obj);
            if (ind != -1)
            {
                Remove(ind);
                return true;
            }
            return false;
        }
        public bool RemoveFirstOccurrence(T obj)
        {
            int index = IndexOf(obj);
            if (index != -1)
            {
                Remove(index);
                return true;
            }
            return false;
        }
        public void Print()
        {
            Node<T> step = new Node<T>(first.data);
            step = first;
            while (step != null)
            {
                Console.WriteLine($"{step.data}");
                step = step.next;
            }
        }
    }
    static class Project
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 5 };
            MyLinketList<int> list = new MyLinketList<int>(arr);
            list.Print();
            int[] ar = new int[] { 3, 4 };
            list.RemoveAll(ar);
            Console.WriteLine("-----------");
            list.Print();

        }
    }
}