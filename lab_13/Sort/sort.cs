using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    public static class UniversalSorts<T> where T : IComparable<T>
    {
        public class TreeNode<T> where T : IComparable<T>
        {
            public T value { get; set; }
            public TreeNode(T key)
            {
                value = key;
            }
            public TreeNode<T> right { get; set; }
            public TreeNode<T> left { get; set; }

            public void InsertNode(TreeNode<T> root)
            {
                if (root.value.CompareTo(value) < 0)
                {
                    if (left == null) left = root;
                    else left.InsertNode(root);
                }
                else
                {
                    if (right == null) right = root;
                    else right.InsertNode(root);
                }
            }

            public T[] TransformToArray(List<T> elements = null)
            {
                if (elements == null) elements = new List<T>();
                if (left != null) left.TransformToArray(elements);
                elements.Add(value);
                if (right != null) right.TransformToArray(elements);
                return elements.ToArray();
            }

        }
        // support method

        // converting subarray to a heap (HeapSort)
        private static void ConvertingToHeap(T[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && array[left].CompareTo(array[largest]) > 0)
                largest = left;
            if (right < n && array[right].CompareTo(array[largest]) > 0)
                largest = right;
            if (largest != i)
            {
                T temp = array[i];
                array[i] = array[largest];
                array[largest] = temp;
                ConvertingToHeap(array, n, largest);
            }
        }

        // for separation array (QuickSort)
        private static int Separation(T[] array, int start, int stop, bool swap = false)
        {
            int left = start;
            T support = array[left];
            int index = 0;
            if ((start + 1).CompareTo(stop) >= 0)
                return left;
            for (int i = start + 1; i < stop; i++)
            {
                if ((!swap && array[i].CompareTo(support) < 0) || (swap && array[i].CompareTo(support) > 0))
                {
                    T temp = array[i];
                    array[i] = array[left];
                    array[left] = temp;
                    left++;
                }
                if (array[i].CompareTo(support) == 0)
                    index = i;
                else if (array[left].CompareTo(support) == 0)
                    index = left;
            }
            array[index] = array[left];
            array[left] = support;
            return left;
        }
        // second part of quick sort (QuickSort)
        private static void SecondPart(T[] helpArray, int left, int right, bool swap = false)
        {
            if (left < right)
            {
                int dot = Separation(helpArray, left, right, swap);
                SecondPart(helpArray, left, dot, swap);
                SecondPart(helpArray, dot + 1, right, swap);
            }
        }

        // for sort bitonic sequence (BitonicSort)
        private static void BitonicSequenceSort(T[] array, int low, int count, bool upward)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++)
                    if (upward ? array[i].CompareTo(array[i + k]) > 0 : array[i].CompareTo(array[i + k]) < 0)
                    {
                        T temp = array[i];
                        array[i] = array[i + k];
                        array[i + k] = temp;
                    }
                BitonicSequenceSort(array, low, k, upward);
                BitonicSequenceSort(array, low + k, k, upward);
            }
        }
        // for create bitonic sequence (BitonicSort)
        private static void BitonicSequenceCreate(T[] array, int low, int count, bool upward)
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSequenceCreate(array, low, k, true);
                BitonicSequenceCreate(array, low + k, k, false);
                BitonicSequenceSort(array, low, count, upward);
            }
        }

        // for merge two subarrays (MergeSort)
        private static void MergeSubarrays(T[] array, int left, int mid, int right, bool swap)
        {
            int countNums1 = mid - left + 1;
            int countNums2 = right - mid;
            T[] leftArray = new T[countNums1];
            T[] rightArray = new T[countNums2];
            for (int i = 0; i < countNums1; i++)
                leftArray[i] = array[left + i];
            for (int i = 0; i < countNums2; i++)
                rightArray[i] = array[mid + i + 1];
            int j = 0, z = 0;
            while (j < countNums1 && z < countNums2)
            {
                if ((!swap && leftArray[j].CompareTo(rightArray[z]) < 0) || (swap && leftArray[j].CompareTo(rightArray[z]) > 0))
                {
                    array[left] = leftArray[j];
                    j++;
                }
                else
                {
                    array[left] = rightArray[z];
                    z++;
                }
                left++;
            }
            while (j < countNums1)
            {
                array[left] = leftArray[j];
                j++;
                left++;
            }
            while (z < countNums2)
            {
                array[left] = rightArray[z];
                z++;
                left++;
            }
        }
        // second part of merge sort (MergeSort)
        private static void Merge(T[] helpArray, int left, int right, bool swap = false)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                Merge(helpArray, left, mid, swap);
                Merge(helpArray, mid + 1, right, swap);
                MergeSubarrays(helpArray, left, mid, right, swap);
            }
        }

        // main sorts
        public static T[] BubbleSort(T[] array, bool swap = false)
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (!swap)
                    {
                        if (array[j].CompareTo(array[j + 1]) > 0)
                        {
                            T temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                        continue;
                    }
                    if (array[j].CompareTo(array[j + 1]) < 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            return array;
        }
        public static T[] ShakerSort(T[] array, bool swap = false)
        {
            int left = 0;
            int right = array.Length;
            bool isSwap = true;
            while (isSwap)
            {
                isSwap = false;
                for (int i = left; i < right - 1; i++)
                    if ((swap && array[i].CompareTo(array[i + 1]) < 0) || (!swap && array[i].CompareTo(array[i + 1]) > 0))
                    {
                        T temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        isSwap = true;
                    }
                --right;
                if (!isSwap) break;
                for (int i = right - 1; i >= left; i--)
                    if ((swap && array[i].CompareTo(array[i + 1]) < 0) || (!swap && array[i].CompareTo(array[i + 1]) > 0))
                    {
                        T temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        isSwap = true;
                    }
                ++left;
            }
            return array;
        }
        public static T[] CombSort(T[] array, bool swap = false)
        {
            double k = 1.2473309;
            int countNums = array.Length;
            bool isSwap = true;
            while (countNums != 1 || isSwap)
            {
                countNums = (int)(countNums / k);
                if (countNums < 1) countNums = 1;
                isSwap = false;
                for (int i = 0; i + countNums < array.Length; i++)
                    if ((!swap && array[i].CompareTo(array[i + countNums]) > 0) || (swap && array[i].CompareTo(array[i + countNums]) < 0))
                    {
                        T temp = array[i];
                        array[i] = array[i + countNums];
                        array[i + countNums] = temp;
                        isSwap = true;
                    }
            }
            return array;
        }
        public static T[] InsertionSort(T[] array, bool swap = false)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int swapIndex = i;
                while (swapIndex > 0 && ((!swap && array[swapIndex].CompareTo(array[swapIndex - 1]) < 0) || (swap && array[swapIndex].CompareTo(array[swapIndex - 1]) > 0)))
                {
                    T temp = array[swapIndex];
                    array[swapIndex] = array[swapIndex - 1];
                    array[swapIndex - 1] = temp;
                    swapIndex--;
                }
            }
            return array;
        }
        public static T[] ShellSort(T[] array, bool swap = false)
        {
            int countNums = array.Length;
            for (int i = countNums / 2; i > 0; i /= 2)
                for (int j = i; j < countNums; j++)
                {
                    T temp = array[j];
                    int z;
                    for (z = j; z >= i && ((!swap && array[z - i].CompareTo(temp) > 0) || (swap && array[z - i].CompareTo(temp) < 0)); z -= i)
                        array[z] = array[z - i];
                    array[z] = temp;
                }
            return array;
        }
        public static T[] TreeSort(T[] array, bool swap = false)
        {
            TreeNode<T> root = new TreeNode<T>(array[0]);
            for (int i = 1; i < array.Length; i++) root.InsertNode(new TreeNode<T>(array[i]));
            T[] supportArray = root.TransformToArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (swap)
                {
                    array[i] = supportArray[array.Length - 1 - i];
                    continue;
                }
                array[i] = supportArray[i];
            }
            return array;
        }
        public static T[] GnomeSort(T[] array, bool swap = false)
        {
            int left = 0;
            int right = array.Length;
            while (left < right)
            {
                if (left == 0) left++;
                if ((!swap && array[left - 1].CompareTo(array[left]) <= 0) || (swap && array[left - 1].CompareTo(array[left]) >= 0))
                    left++;
                else
                {
                    T temp = array[left - 1];
                    array[left - 1] = array[left];
                    array[left] = temp;
                    left--;
                }
            }
            return array;
        }
        public static T[] SelectionSort(T[] array, bool swap = false)
        {
            int countNums = array.Length;
            for (int i = 0; i < countNums; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < countNums; j++)
                    if ((!swap && array[j].CompareTo(array[minIndex]) < 0) || (swap && array[j].CompareTo(array[minIndex]) > 0))
                        minIndex = j;
                if (minIndex != i)
                {
                    T temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
            return array;
        }
        public static T[] HeapSort(T[] array, bool swap = false)
        {
            int countNums = array.Length;
            for (int i = countNums / 2 - 1; i >= 0; i--)
                ConvertingToHeap(array, countNums, i);
            for (int i = countNums - 1; i >= 0; i--)
            {
                T temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                ConvertingToHeap(array, i, 0);
            }
            if (swap)
            {
                for (int i = 0; i < countNums / 2; i++)
                {
                    T temp = (T)array[i];
                    array[i] = array[countNums - i - 1];
                    array[countNums - i - 1] = temp;
                }
            }
            return array;
        }
        public static T[] QuickSort(T[] array, bool swap = false)
        {
            SecondPart(array, 0, array.Length, swap);
            return array;
        }
        public static T[] MergeSort(T[] array, bool swap = false)
        {
            Merge(array, 0, array.Length - 1, swap);
            return array;
        }
        public static T[] BitonicSort(T[] array, bool swap = false)
        {
            BitonicSequenceCreate(array, 0, array.Length, !swap);
            return array;
        }
    }
    public static class Generate
    {
        public static int[] GenerateArray(int size)
        {
            int[] array = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
                array[i] = random.Next(0, 1000);
            return array;
        }
        public static int[] GenerateSubArrays(int size)
        {
            Random random = new Random();
            int module = random.Next(0, size);
            int newSize = random.Next(2, size) % module;
            if (newSize < 2) newSize = 2;
            int[] array = new int[size];
            int countArray = 0, i = 0;
            while (i < size)
            {
                int baseElement = 0;
                int exp = random.Next(0, 1000);
                countArray++;
                while (i < size && i < countArray * newSize)
                {
                    baseElement++;
                    array[i] = baseElement * exp;
                    i++;
                }
            }
            return array;
        }
        public static int[] GenerateBySwap(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++) array[i] = i;
            Random random = new Random();
            int countSwaps = random.Next(0, size / 3);
            for (int i = 0; i < countSwaps; i++)
            {
                int first = random.Next(0, array.Length - 1);
                int second = random.Next(0, array.Length - 1);
                int temp = array[first];
                array[first] = array[second];
                array[second] = temp;
            }
            return array;
        }
        public static int[] GenerateSwapAndRepeat(int size)
        {
            int[] array = GenerateBySwap(size);
            Random random = new Random();
            int repeatIndex = random.Next(0, array.Length - 1);
            int repeatCount = random.Next(0, array.Length / 3);
            while (repeatCount > 0)
            {
                int randomIndex = random.Next(0, array.Length - 1);
                if (array[randomIndex] != array[repeatIndex])
                {
                    array[randomIndex] = array[repeatIndex];
                    repeatCount--;
                }
            }
            return array;
        }
    }
}