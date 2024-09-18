using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    public class TreeNode
    {
        public TreeNode(int data)
        {
            Data = data;
        }

        //данные
        public int Data { get; set; }

        //левая ветка дерева
        public TreeNode Left { get; set; }

        //правая ветка дерева
        public TreeNode Right { get; set; }

        //рекурсивное добавление узла в дерево
        public void Insert(TreeNode node)
        {
            if (node.Data < Data)
            {
                if (Left == null)
                {
                    Left = node;
                }
                else
                {
                    Left.Insert(node);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Insert(node);
                }
            }
        }

        //преобразование дерева в отсортированный массив
        public int[] Transform(List<int> elements = null)
        {
            if (elements == null)
            {
                elements = new List<int>();
            }

            if (Left != null)
            {
                Left.Transform(elements);
            }

            elements.Add(Data);

            if (Right != null)
            {
                Right.Transform(elements);
            }

            return elements.ToArray();
        }
    }
    public static class Sorting
    {
        public static int[] TreeSort(int[] array)
        {
            TreeNode root = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++) root.Insert(new TreeNode(array[i]));
            int[] newArray = root.Transform();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = newArray[i];
            }
            return array;
        }
        public static int[] GnomeSort(int[] array)
        {
            int index = 1;
            int nextIndex = index + 1;

            while (index < array.Length)
            {
                if (array[index - 1] < array[index])
                {
                    index = nextIndex;
                    nextIndex++;
                }
                else
                {
                    int tmp = array[index - 1];
                    array[index - 1] = array[index];
                    array[index] = tmp;
                    index--;
                    if (index == 0)
                    {
                        index = nextIndex;
                        nextIndex++;
                    }
                }
            }
            return array;
        }
        //метод возвращающий индекс опорного элемента
        public static void QuickSort(int[] array, int left, int right)
        {

            if (left > right) return;

            int p = array[(left + right) / 2];
            int i = left;
            int j = right;
            while (i <= j)
            {
                while (array[i] < p) i++;
                while (array[j] > p) j--;
                if (i <= j)
                {
                    int tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;
                    i++;
                    j--;
                }
            }
            QuickSort(array, left, j);
            QuickSort(array, i, right);

        }
        public static int[] QuickSort(int[] array)
        {
            QuickSort(array, 0, (array.Length) - 1);
            return array;
        }        //метод для слияния массивов
        public static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            int left = lowIndex;
            int right = middleIndex + 1;
            int[] tempArray = new int[highIndex - lowIndex + 1];
            int index = 0;
            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }
                index++;
            }
            for (int i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }
            for (int i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }
        //сортировка слиянием
        public static int[] MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }

            return array;
        }
        public static int[] CountingSort(int[] array)
        {
            //поиск минимального и максимального значений
            int min = array[0];
            int max = array[0];
            foreach (int element in array)
            {
                if (element > max)
                {
                    max = element;
                }
                else if (element < min)
                {
                    min = element;
                }
            }
            //поправка
            int correctionFactor = min != 0 ? -min : 0;
            max += correctionFactor;
            int[] count = new int[max + 1];
            for (int i = 0; i < array.Length; i++)
            {
                count[array[i] + correctionFactor]++;
            }
            int index = 0;
            for (int i = 0; i < count.Length; i++)
            {
                for (int j = 0; j < count[i]; j++)
                {
                    array[index] = i - correctionFactor;
                    index++;
                }
            }
            return array;
        }
        public static int[] MergeSort(int[] array)
        {
            return MergeSort(array, 0, array.Length - 1);
        }
        public static int[] BoubleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[i] < arr[j])
                    {
                        int tmp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = tmp;
                    }
                }
            }
            return arr;
        }
        static void BoubleSortSpec(List<double> arr)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = 0; j < arr.Count; j++)
                {
                    if (arr[i] < arr[j])
                    {
                        double tmp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = tmp;
                    }
                }
            }
        }
        public static int[] ShakerSort(int[] arr)
        {
            bool swaped = true;
            int startInddex = 0;
            int endInddex = arr.Length;

            while (swaped)
            {
                swaped = false;
                for (int i = startInddex; i < endInddex - 1; i++)
                {
                    if ((arr[i] > arr[i + 1]))
                    {
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        swaped = true;
                    }
                }
                if (!swaped) break;
                --endInddex;
                for (int i = endInddex - 1; i >= startInddex; i--)
                {
                    if ((arr[i] > arr[i + 1]))
                    {
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        swaped = true;
                    }
                }
                ++startInddex;
            }
            return arr;
        }
        static int IndexOfMin(int[] array, int n)
        {
            int result = n;
            for (int i = n; i < array.Length; ++i)
            {
                if (array[i] < array[result])
                {
                    result = i;
                }
            }

            return result;
        }
        public static int[] InsertionSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int key = array[i];
                int j = i;
                while ((j > 0) && (array[j - 1] > key))
                {
                    int tmp = array[j - 1];
                    array[j - 1] = array[j];
                    array[j] = tmp;
                    j--;
                }

                array[j] = key;
            }
            return array;
        }
        public static int[] ShellSort(int[] array)
        {
            //расстояние между элементами, которые сравниваются
            var d = array.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < array.Length; i++)
                {
                    var j = i;
                    while ((j >= d) && (array[j - d] > array[j]))
                    {
                        int tmp = array[j];
                        array[j] = array[j - d];
                        array[j - d] = tmp;
                        j = j - d;
                    }
                }

                d = d / 2;
            }
            return array;
        }
        public static int[] SelectionSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int extremum = array[i];
                int indexOfExtremum = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] > extremum)
                    {
                        indexOfExtremum = j;
                        extremum = array[j];
                    }
                }

                array[indexOfExtremum] = array[i];
                array[i] = extremum;
            }
            return array;

        }
        static int GetNextStep(int s)
        {
            s = s * 1000 / 1247;
            return s > 1 ? s : 1;
        }
        public static int[] CombSort(int[] array)
        {
            int arrayLength = array.Length;
            int currentStep = arrayLength - 1;
            while (currentStep > 1)
            {
                for (int i = 0; i + currentStep < array.Length; i++)
                {
                    if (array[i] > array[i + currentStep])
                    {
                        int tmp = array[i];
                        array[i] = array[i + currentStep];
                        array[i + currentStep] = tmp;
                    }
                }
                currentStep = GetNextStep(currentStep);
            }
            //сортировка пузырьком
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[i] < array[j])
                    {
                        int tmp = array[i];
                        array[i] = array[j];
                        array[j] = tmp;
                    }
                }
            }
            return array;
        }
        static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]} ");
            }
        }
        static void PrintArrayDouble(double[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]} ");
            }
        }
        public static int[] Heapify(int[] array, int size, int index)
        {
            var largestIndex = index;
            var leftChild = 2 * index + 1;
            var rightChild = 2 * index + 2;

            if (leftChild < size && array[leftChild] > array[largestIndex])
            {
                largestIndex = leftChild;
            }

            if (rightChild < size && array[rightChild] > array[largestIndex])
            {
                largestIndex = rightChild;
            }

            if (largestIndex != index)
            {
                var tempVar = array[index];
                array[index] = array[largestIndex];
                array[largestIndex] = tempVar;

                Heapify(array, size, largestIndex);
            }
            return array;
        }
        public static int[] HeapSort(int[] array)
        {
            int size=array.Length;
            if (size <= 1)
                return array;

            for (int i = size / 2 - 1; i >= 0; i--)
            {
                Heapify(array, size, i);
            }

            for (int i = size - 1; i >= 0; i--)
            {
                var tempVar = array[0];
                array[0] = array[i];
                array[i] = tempVar;
                Heapify(array, i, 0);
            }

            return array;
        }
        public static double[] BucketSort(double[] arr)
        {
            int n = arr.Length;
            //1
            List<double>[] buckets = new List<double>[n];
            for (var i = 0; i < n; i++)
            {
                buckets[i] = new List<double>();
            }
            //2
            for (var i = 0; i < n; i++)
            {
                int bi = (int)arr[i];
                buckets[bi].Add(arr[i]);
            }
            //3
            for (int i = 0; i < n; i++)
            {
                BoubleSortSpec(buckets[i]);
            }
            //4
            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    arr[index++] = buckets[i][j];
                }
            }
            return arr;
        }
        public static int[] RadixSort(int[] array)
        {

            int max = array[0];
            for (int i = 1; i < array.Length; i++) if (array[i] > max) max = array[i];

            for (int exp = 1; max / exp > 0; exp *= 10) CountingSort(array);
            return array;
        }
        private static void BitonicMerge(int[] array, int low, int cnt, Direction dir)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;
                for (int i = low; i < low + k; ++i)
                {
                    if ((dir == Direction.ASCENDING && array[i] > array[i + k]) ||
                        (dir == Direction.DESCENDING && array[i] < array[i + k]))
                    {
                        int tmp = array[i];
                        array[i]= array[i + k];
                        array[i + k] = tmp;
                    }
                }
                BitonicMerge(array, low, k, dir);
                BitonicMerge(array, low + k, k, dir);
            }
        }
        private static void BitonicSortPart(int[] array, int low, int cnt, Direction dir)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;
                BitonicSortPart(array, low, k, Direction.ASCENDING);
                BitonicSortPart(array, low + k, k, Direction.DESCENDING);
                BitonicMerge(array, low, cnt, dir);
            }

        }
        public static int[] BitonicSort(int[] array)
        {

            double length = array.Length;
            int powOfTwo = 0;
            while (length > 1)
            {
                length /= 2;
                powOfTwo++;
            }
            if (length != 1) powOfTwo++;

            int[] bitonicArray = new int[(int)Math.Pow(2, powOfTwo)];
            for (int i = 0; i < bitonicArray.Length; i++)
            {
                if (i < array.Length)
                {
                    bitonicArray[i] = array[i];
                    continue;
                }
                bitonicArray[i] = -1;
            }

            BitonicSortPart(bitonicArray, 0, bitonicArray.Length, Direction.ASCENDING);
            return array;
        }
        public enum Direction { ASCENDING, DESCENDING };
    }
}
