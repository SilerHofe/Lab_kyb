using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayList
{
    public class MyArrayList<T>
    {
        public int size { get; set; }
        T[] elementData { get; set; }
        public MyArrayList()
        {
            elementData = null;
            size = 0;
        }
        public MyArrayList(T[] array)
        {
            elementData = new T[(int)(array.Length * 1.5)];
            for (int i = 0; i < array.Length; i++)
                elementData[i] = array[i];
            size = array.Length;
        }
        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = capacity;
        }

        public void Add(T element)
        {
            if (size == elementData.Length)
            {
                T[] ar = new T[(int)(elementData.Length * 1.5) + 1];
                for (int i = 0; i < size; i++) ar[i] = elementData[i];
                elementData = ar;
            }
            elementData[size] = element;
            size++;
        }
        public void AddAll(T[] array)
        {
            foreach (T item in array) Add(item);
        }
        public void Clear()
        {
            elementData = null;
            size = 0;
        }
        public bool Contains(T element)
        {
            for (int i = 0; i < size; i++) if (elementData[i].Equals(element)) return true;
            return false;
        }
        public bool ContainsAll(T[] array)
        { 
            foreach (T item in array)
            {
                for (int i = 0; i < size; i++) if (!(elementData[i].Equals(item))) return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            return size== 0;
        }
        public void Remove(T item)
        {
            if (Contains(item))
            {
                int f = 0;
                for (int i = 0; i < size; i++)
                {
                    if (f == 0)
                    {
                        if (elementData[i].Equals(item))
                        {
                            elementData[i] = elementData[i + 1];
                            f = 1;
                        }
                    }
                    else if (f == 1)
                    {
                        elementData[i] = elementData[i + 1];
                    }
                }
                size--;
            }
        }
        public void RemoveAll(T[] array)
        {
            foreach (T item in array)
            {
                Remove(item);
            }
        }
        public void RetainAll(T[] array)
        {
            T[] newArr = new T[size];
            int newSize = 0;
            foreach (T item in array)
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
            elementData = newArr;
            size = newSize;
        }
        public int Size()
        {
            return size;
        }
        public object[] ToArray()
        {
            object[] array = new object[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = elementData[i];
            }
            return array;
        }
        public void ToArray(T[] array)
        {
            if(array == null) throw new ArgumentNullException("array");
            for (int i = 0; i < array.Length && i < size; i++) array[i] = (T)elementData[i];
        }
        public void AddInd(int index, T element)
        {
            if (index >= size) { Add(element); return; }
            T[] ar = new T[size + 1];
            for (int i = 0, j = 0; i <= size; i++, j++)
            {
                if (i == index)
                {
                    ar[i] = element;
                    i++;
                }
                ar[i] = elementData[j];
            }
            elementData = ar;
            size++;
        }
        public void AddAllInd(int index, T[] array)
        {
            int i = index;
            foreach (T element in array) { AddInd(i, element); i++; }
        }
        public T Get(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return elementData[index];
        }
        public int IndexOf(object obj)
        {
            for (int i = 0; i < size; i++)
            {
                if (obj.Equals(elementData[i])) return i;
            }
            return -1;
        }
        public int LastIndexOff(object obj)
        {
            int ind = -1;
            for (int i = 0; ind < size; i++) { if (obj.Equals(elementData[i])) ind = i; }
            return ind;
        }
        public T RemoveInd(int index)
        {
            if ((index < 0 || index >= size)) throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            Remove(element);
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
            MyArrayList<T> list = new MyArrayList<T>(toIndex - fromIndex);
            for (int i = 0; i < list.size; i++)
            {
                list.Set(i, elementData[fromIndex + i]);
            }
            return list;
        }

        public void ArrayPrint()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{elementData[i]} ");
            }
            Console.WriteLine();
        }

    }
}
