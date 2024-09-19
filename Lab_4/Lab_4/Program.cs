using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

class MyArrayList<T>
{
    public int size { get; set; }
    T[] elementData { get; set; }
    public MyArrayList()
    {
        elementData = null;
        size = 0;
    }
    public MyArrayList(T[] ar)
    {
        elementData = new T[(int)(ar.Length * 1.5)];
        for (int i = 0; i < ar.Length; i++)
            elementData[i] = ar[i];
        size = ar.Length;
    }
    public MyArrayList(int capacity)
    {
        elementData = new T[capacity];
        size = capacity;
    }

    public void Add(T e)
    {
        if (size == elementData.Length)
        {
            T[] ar = new T[(int)(elementData.Length*1.5)+1];
            for (int i=0; i < size; i++) ar[i]= elementData[i];
            elementData = ar;
        }
        elementData[size] = e;
        size++;
    }
    public void AddAll(T[] ar)
    {
        foreach (T item in ar) Add(item);
    }
    public void Clear()
    {
        elementData = null;
        size = 0;
    }
    public bool Contains(T e)
    {
        bool f= false;
        for (int i=0; i < size; i++) if (elementData[i].Equals(e)) f= true;
        return f;
    }
    public bool ContainsALL(T[] ar)
    {
        bool f = true;
        foreach(T item in ar)
        {
            for (int i = 0; i < size; i++) { if (elementData[i].Equals(item)) continue; else f = false; }
        }
        return f;
    }
    public bool IsEmpty()
    {
        if (size == 0) return true;
        return false;
    }
    public void Remove(T item)
    {
        if (Contains(item))
        {
            int f = 0;
            for (int i=0;i<size; i++)
            {
                if (f == 0)
                {
                    if (elementData[i].Equals(item))
                    {
                        elementData[i] = elementData[i + 1];
                        f = 1;
                    }
                } else if (f == 1)
                {
                    elementData[i] = elementData[i+1];
                }
            }size--;
        }
    }
    public void RemoveAll(T[] ar)
    {
        foreach(T item in ar)
        {
            Remove(item);
        }
    }
    public void RetainAll(T[] ar)
    {
        T[]newArr= new T[size];
        int newSize = 0;
        foreach(T item in ar)
        {
            for (int i = 0; i < size; i++)
            {
                if (item.Equals(elementData[i]))
                {
                    newArr[newSize] = elementData[i];
                    newSize++;
                }
            }
        }
        elementData= newArr;
        size = newSize;
    }
    public int Size()
    {
        return size;
    }
    public object[] ToArray()
    {
        object[] array= new object[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = elementData[i];
        }
        return array;
    }
    public void ToArray(T[] a)
    {
        for (int i = 0; i < a.Length && i < size; i++) a[i] = (T)elementData[i];
    }
    public void AddInd(int index, T element)
    {
        if (index >= size) {Add(element); return; }
        T[] ar = new T[size+1];
        for(int i = 0,j=0;i <= size; i++,j++)
        {
            if (i == index) 
            {
                ar[i] = element;
                i++;
            }
            ar[i]= elementData[j];
        }
        elementData= ar;
        size++;
    }
    public void AddAllInd(int  index, T[] a)
    {
        int i = index;
        foreach (T element in a) { AddInd(i, element);i++; }
    }
    public T Get(int index)
    {
        if(index<0|| index >= size)
        {
            throw new ArgumentOutOfRangeException("index");
        }
        return elementData[index];
    }
    public int IndexOf(object o)
    {
        for(int i = 0; i < size; i++)
        {
            if (o.Equals(elementData[i])) return i;
        }return -1;
    }
    public int LastIndexOff(object o)
    {
        int ind = -1;
        for (int i = 0; ind < size; i++) { if (o.Equals(elementData[i])) ind = i; }
        return ind;
    }
    public T Remove(int index)
    {
        if((index<0|| index >= size)) throw new ArgumentOutOfRangeException("index");
        T element = elementData[index];
        for (int i = 0; i < size; ++i) { elementData[i] = elementData[i+1]; }
        size--;
        return element;
    }
    public void Set(int index, T element)
    {
        if ((index < 0 || index >= size)) throw new ArgumentOutOfRangeException("index");
        if (element == null) throw new ArgumentNullException();
        elementData[index] = element;
    }
    public MyArrayList<T> SubList(int fromIndex, int toIndex)
    {
        if ((fromIndex < 0 || fromIndex >= size)) throw new ArgumentOutOfRangeException("fromIndex");
        if (toIndex < 0 || toIndex >= size) throw new ArgumentOutOfRangeException("toIndex");
        MyArrayList<T> list = new MyArrayList<T>(toIndex-fromIndex);
        for (int i = 0; i < list.size; i++)
        {
            list.Set(i, elementData[fromIndex+i]);
        }
        return list;
    }

    public void ArrayPrint()
    {
        for (int i=0;i<size; i++)
        {
            Console.Write($"{elementData[i]} ");
        }Console.WriteLine();
    }

}
class Program
{
    static void rand(int[] a)
    {
        Random random = new Random();
        for (int i=0;i<a.Length;i++)
        {
            a[i]= random.Next(0,100);
        }
    }
    static void Main(string[] args)
    {
        int[] array = new int[] { 1, 2, 3, 4, 5,6,7,8 };
        MyArrayList<int> testArray = new MyArrayList<int>(array);
        testArray.ArrayPrint();
        int[] array2 = new int[] { 13, 14, 15, 16 };
        testArray.AddAllInd(3,array2);
        testArray.ArrayPrint();
        testArray.Add(51);
        testArray.ArrayPrint();
        MyArrayList<int> r = testArray.SubList(1, 5);
        r.ArrayPrint();
        Console.WriteLine(testArray.Contains(51));
        Console.WriteLine(testArray.Contains(21));
    }
}