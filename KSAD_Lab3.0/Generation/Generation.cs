using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation
{
    public class Generate
    {
        public static int[] CreateArrMod1000(int size)
        {
            int[] array = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(0, 1000);
            }
            return array;
        }
    public static int[] CreateArrHalfSort(int totalSize)
        {
            Random random = new Random();
            int i = 0;
            int maxSubarraySize = random.Next(10, totalSize);
            int[] fullArray = new int[totalSize];
            int currentIndex = 0;
            while (totalSize > 0)
            {
                // Генерация случайного размера подмассива
                int subarraySize = random.Next(1, Math.Min(totalSize, maxSubarraySize) + 1);
                int[] subarray = GenerateRandomArray(subarraySize);
                Array.Sort(subarray); // Сортировка подмассива

                // Добавление отсортированного подмассива в полный массив
                for (int j = 0; j < subarray.Length; j++)
                {
                    fullArray[currentIndex++] = subarray[j];
                }

                totalSize -= subarraySize; // Уменьшение оставшегося объема
            }
            return fullArray;
        }
        public static int[] ChangeNumberArr(int size)
        {
            Random random = new Random();
            int newLength = random.Next(2, size);
            if (newLength < 2) newLength = 2;
            int[] array = new int[size];
            int countOfArray = 0;

            int i = 0;
            while (i < size)
            {
                int exp = random.Next(0, 1000);
                int elementBase = 0;
                countOfArray++;

                while (i < size && i < newLength * countOfArray)
                {
                    elementBase++;
                    array[i] = elementBase * exp;
                    i++;
                }
            }

            return array;
        }
        public static int[] PrimeArr(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++) array[i] = i;

            Random random = new Random();
            int countOfSwap = random.Next(0, size / 3);
            for (int i = 0; i < countOfSwap; i++)
            {
                int firstIndex = random.Next(0, array.Length - 1);
                int secondIndex = random.Next(0, array.Length - 1);
                int temp = array[firstIndex];
                array[firstIndex] = array[secondIndex];
                array[secondIndex] = temp;
            }
            Random r = new Random();
            int indexOfRepeat = r.Next(0, size - 1);
            int countOfRepeat = r.Next(0, size / 3);

            while (countOfRepeat > 0)
            {
                int randomIndex = r.Next(0, array.Length - 1);
                if (array[randomIndex] != array[indexOfRepeat])
                {
                    array[randomIndex] = array[indexOfRepeat];
                    countOfRepeat--;
                }

            }
            return array;
        }
        private static int[] GenerateRandomArray(int n)
        {
            int[] array = new int[n];
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = r.Next(1, 1000);
            }
            return array;
        }
        private static double[] GenerateRandomArrayDouble(int n)
        {
            double[] array = new double[n];
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = (double)r.Next(1, 100) / 100;
            }
            return array;
        }

    }
}
