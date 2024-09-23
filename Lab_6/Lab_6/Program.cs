using System;
using MyVector;
namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vector = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            MyVector<int> testVector = new MyVector<int>(vector);
            testVector.VectorPrint();
            int[] vector2 = new int[] { 13, 14, 15, 16 };
            testVector.AddAllInd(3, vector2);
            testVector.VectorPrint();
            testVector.Add(51);
            testVector.VectorPrint();
            testVector.Remove(14);
            testVector.VectorPrint();
            testVector.RemoveInd(5);
            testVector.VectorPrint();
            MyVector<int> newVector = testVector.SubList(1, 5);
            newVector.VectorPrint();
            Console.WriteLine(testVector.Contains(51));
            Console.WriteLine(testVector.Contains(21));
        }
    }
}