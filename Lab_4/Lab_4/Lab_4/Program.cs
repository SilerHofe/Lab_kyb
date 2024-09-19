using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using MyArrayList;

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
        testArray.Remove(14);
        testArray.ArrayPrint();
        testArray.RemoveInd(5);
        testArray.ArrayPrint();
        MyArrayList<int> newArray = testArray.SubList(1, 5);
        newArray.ArrayPrint();
        Console.WriteLine(testArray.Contains(51));
        Console.WriteLine(testArray.Contains(21));
    }
}