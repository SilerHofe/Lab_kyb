using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayList
{
    public class MyArrayList<T>
    {
        private T[] elementData;
        private int size;
        public MyArrayList()
        {

            size = 0;
        }
        public MyArrayList(T[] array)
        {
            elementData = new T[array.Length];
            size = array.Length;
            for (int i = 0; i < size; i++)
            {
                elementData[i] = array[i];
            }
        }
        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = 0;
        }
        public void Add(T element)
        {
            if (size == elementData.Length)
            {
                Resize();
            }

            elementData[size] = element;
            size++;
        }
        public int Capacity() => elementData.Length;
        public void Add(T[] array)
        {
            foreach (T element in array)
            {
                Add(element);
            }
        }
        private void Resize()
        {
            int newCapacity = (int)(elementData.Length * 1.5) + 1;
            T[] newArray = new T[newCapacity];
            for (int i = 0; i < size; i++)
            {
                newArray[i] = elementData[i];
            }
            elementData = newArray;
        }
        public int Size => size;
        public void Clear()
        {

            size = 0;
        }
        public bool Contains(T element)
        {
            foreach (T item in elementData)
            {
                if (item.Equals(element)) return true;
            }
            return false;
        }
        public bool ContainsAll(T[] array)
        {
            bool[] b = new bool[array.Length];
            int index = 0;
            foreach (T item in array)
            {
                foreach (T element in elementData)
                {
                    if (item.Equals(element))
                    {
                        b[index] = true;
                        if (index != b.Length) index++;
                    }
                }
            }
            foreach (bool element in b)
            {
                if (!element) return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public T Remove(int index)
        {
            if ((index < 0) || (index >= size)) throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int i = index; i < size - 1; i++)
            {
                elementData[i] = elementData[i + 1];
            }
            size--;
            return element;
        }
        public T[] ToArray()
        {
            T[] newArray = new T[size];
            for (int i = 0; i < size; i++)
            {
                newArray[i] = elementData[i];
            }
            return newArray;
        }
        public T Get(int index)
        {
            if ((index < 0) || (index >= size)) throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            return element;
        }
        public int IndexOf(T element)
        {
            for (int i = 0; i < size; i++)
            {
                if (element.Equals(elementData[i])) return i;
            }
            return -1;
        }
        public int LastIndexOf(T element)
        {
            int index = -1;
            for (int i = 0; i < size; i++)
            {
                if (element.Equals(elementData[i])) index = i;
            }
            return index;
        }
        public void Remove(params T[] element)
        {
            foreach (T obj in element)
            {
                int i = 0;
                while (i < elementData.Length)
                {
                    if (obj.Equals(elementData[i]))
                    {
                        for (int j = i; j < elementData.Length - 1; j++)
                        {
                            elementData[j] = elementData[j + 1];
                        }
                        size--;

                    }
                    i++;
                }
            }
        }
        public void Set(int index, T element)
        {
            if ((index < 0) || (index >= size)) throw new ArgumentOutOfRangeException("index");
            elementData[index] = element;


        }
        public T[] ToArray(T[] array)
        {
            if (array == null)
                array = new T[size];
            for (int i = 0; i < size; i++)
                array[i] = elementData[i];
            return array;
        }
        public void Retain(params object[] array)
        {
            T[] newArray = new T[size];
            int newSize = 0;
            for (int i = 0; i < size; i++)
            {
                foreach (object element in array)
                {
                    if (element.Equals(elementData[i])) ;
                    {
                        newArray[newSize++] = elementData[i];
                    }
                }
            }
            size = newSize;
            elementData = newArray;
        }
        public void AddAll(int index, params T[] element)
        {
            if (index > size)
            {
                Add(element);
                return;
            }
            else
            {
                T[] newArray = new T[size + element.Length];
                for (int i = 0; i < index; i++)
                {
                    newArray[i] = elementData[i];
                }
                for (int i = index; i < index + element.Length; i++)
                {
                    newArray[i] = element[i];
                }
                for (int i = index + element.Length; i < size; i++)
                {
                    newArray[i] = elementData[i];
                }
                elementData = newArray;
                size = newArray.Length;
            }
        }

        public MyArrayList<T> SubList(int start, int end)
        {

            MyArrayList<T> newArray = new MyArrayList<T>(end - start);
            for (int i = start; i <= end; i++)
            {
                newArray.Add(elementData[i]);
            }
            return newArray;
        }
    }
}