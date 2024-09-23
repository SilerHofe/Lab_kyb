using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVector
{
    public class MyVector<T>
    {
        public int elementCount { get; set; }
        T[] elementData { get; set; }
        int capacityIncrement {  get; set; }
        public MyVector()
        {
            elementData = null;
            elementCount = 10;
            capacityIncrement = 0;
        }
        public MyVector(T[] array)
        {
            elementData = new T[(int)(array.Length * 1.5)];
            for (int i = 0; i < array.Length; i++)
                elementData[i] = array[i];
            elementCount = array.Length;
            capacityIncrement = 0;
        }
        public MyVector(int initialCapacity,int initialCampacityIncrement)
        {
            elementData = new T[initialCapacity];
            elementCount=initialCapacity;
            capacityIncrement=initialCampacityIncrement;
        }

        public void Add(T element)
        {
            if (elementCount == elementData.Length)
            {
                T[] ar = null;
                if (capacityIncrement != 0) ar = new T[elementCount + capacityIncrement];
                else ar = new T[elementCount * 2];
                for (int i = 0; i < elementCount; i++) ar[i] = elementData[i];
                elementData = ar;
            }
            elementData[elementCount] = element;
            elementCount++;
        }
        public void AddAll(T[] array)
        {
            foreach (T item in array) Add(item);
        }
        public void Clear()
        {
            elementData = null;
            elementCount = 0;
        }
        public bool Contains(T element)
        {
            for (int i = 0; i < elementCount; i++) if (elementData[i].Equals(element)) return true;
            return false;
        }
        public bool ContainsAll(T[] array)
        {
            foreach (T item in array)
            {
                for (int i = 0; i < elementCount; i++) if (!(elementData[i].Equals(item))) return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            return elementCount == 0;
        }
        public void Remove(T item)
        {
            if (Contains(item))
            {
                int f = 0;
                for (int i = 0; i < elementCount; i++)
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
                elementCount--;
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
            T[] newArr = new T[elementCount];
            int newelementCount = 0;
            foreach (T item in array)
            {
                for (int i = 0; i < elementCount; i++)
                {
                    if (item.Equals(elementData[i]))
                    {
                        newArr[newelementCount] = elementData[i];
                        newelementCount++;
                    }
                }
            }
            elementData = newArr;
            elementCount = newelementCount;
        }
        public int Size()
        {
            return elementCount;
        }
        public object[] ToArray()
        {
            object[] array = new object[elementCount];
            for (int i = 0; i < elementCount; i++)
            {
                array[i] = elementData[i];
            }
            return array;
        }
        public void ToArray(T[] array)
        {
            if (array == null) ToArray();
            for (int i = 0; i < array.Length && i < elementCount; i++) array[i] = (T)elementData[i];
        }
        public void AddInd(int index, T element)
        {
            if (index >= elementCount) { Add(element); return; }
            T[] ar = new T[elementCount + 1];
            for (int i = 0, j = 0; i <= elementCount; i++, j++)
            {
                if (i == index)
                {
                    ar[i] = element;
                    i++;
                }
                ar[i] = elementData[j];
            }
            elementData = ar;
            elementCount++;
        }
        public void AddAllInd(int index, T[] array)
        {
            int i = index;
            foreach (T element in array) { AddInd(i, element); i++; }
        }
        public T Get(int index)
        {
            if (index < 0 || index >= elementCount)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return elementData[index];
        }
        public int IndexOf(object obj)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (obj.Equals(elementData[i])) return i;
            }
            return -1;
        }
        public int LastIndexOff(object obj)
        {
            int ind = -1;
            for (int i = 0; ind < elementCount; i++) { if (obj.Equals(elementData[i])) ind = i; }
            return ind;
        }
        public T RemoveInd(int index)
        {
            if ((index < 0 || index >= elementCount)) throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            Remove(element);
            return element;
        }
        public void Set(int index, T element)
        {
            if ((index < 0 || index >= elementCount)) throw new ArgumentOutOfRangeException("index");
            if (element == null) throw new ArgumentNullException();
            elementData[index] = element;
        }
        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if ((fromIndex < 0 || fromIndex >= elementCount)) throw new ArgumentOutOfRangeException("fromIndex");
            if (toIndex < 0 || toIndex >= elementCount) throw new ArgumentOutOfRangeException("toIndex");
            MyVector<T> list = new MyVector<T>(toIndex - fromIndex,10);
            for (int i = 0; i < list.elementCount; i++)
            {
                list.Set(i, elementData[fromIndex + i]);
            }
            return list;
        }
        public T FirstElement() => elementData[0];
        public T LastElemnet() => elementData[elementCount - 1];
        public void RemoveElementAt(int index) => RemoveInd(index);
        public void RemoveRange(int begin,int end)
        {
            if ((begin < 0 || begin >= elementCount)) throw new ArgumentOutOfRangeException("begin");
            if (end < 0 || end >= elementCount) throw new ArgumentOutOfRangeException("end");
            for (int i = begin; i < end; i++) RemoveInd(i);
        }

        public void VectorPrint()
        {
            for (int i = 0; i < elementCount; i++)
            {
                Console.Write($"{elementData[i]} ");
            }
            Console.WriteLine();
        }

    }
}