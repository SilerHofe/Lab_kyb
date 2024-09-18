using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Generation;
using Sort;
using ZedGraph;

namespace KSAD_Lab3._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void SetPath()
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            pathToArray = "C:/Users/sashs/source/repos/KSAD_Lab3.0/KSAD_Lab3.0/bin/Debug/array.txt";
            pathToTime = "C:/Users/sashs/source/repos/KSAD_Lab3.0/KSAD_Lab3.0/bin/Debug/time.txt";
        }
        string pathToArray;
        string pathToTime;

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        int selectedGroupIndex = -1;
        int selectedArrayTypeIndex = -1;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGroupIndex = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedArrayTypeIndex = comboBox2.SelectedIndex;
        }
        private void SpeedOfSorting(Func<int, int[]> Generate, int size, params Func<int[], int[]>[] SortMethods)
        {
            SetPath();
            double[] speedSum = new double[SortMethods.Length];
            for (int i = 0; i < 20; i++)
            {
                int[] array = Generate(size);
                try
                {
                    StreamWriter sw = File.AppendText(pathToArray);
                    sw.WriteLine("Unsorted array: " + (i + 1).ToString());
                    foreach (int item in array) sw.Write(item.ToString() + " ");
                    sw.Write("\n");

                    int[] sortedArray = null;
                    int index = 0;
                    foreach (Func<int[],int[]> Method in SortMethods)
                    {
                        Stopwatch timer = new Stopwatch();
                        timer.Start();
                        sortedArray = Method(array);
                        timer.Stop();
                        speedSum[index] += timer.ElapsedMilliseconds;
                        index++;
                    }

                    sw.WriteLine("Sorted array: " + (i + 1).ToString());
                    foreach (int item in sortedArray) sw.Write(item.ToString() + " ");
                    sw.Write("\n");
                    sw.Close();


                }
                catch (Exception ex) { Console.WriteLine(ex); };
            }

            try
            {
                StreamWriter sw = File.AppendText(pathToTime);
                for (int i = 0; i < speedSum.Length; i++)
                {
                    if (i == 0)
                    {
                        sw.Write(((double)(speedSum[i] / 20)).ToString());
                        continue;
                    }
                    sw.Write(" " + ((double)(speedSum[i] / 20)).ToString());
                }
                sw.WriteLine();
                sw.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetPath();
            int pow = 4;
            switch (selectedArrayTypeIndex)
            {

                //Рандомные числа
                case 0:
                    switch (selectedGroupIndex)
                    {
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, pow); length *= 10)
                                SpeedOfSorting(Generate.CreateArrMod1000, length, Sorting.BoubleSort, Sorting.CombSort,Sorting.InsertionSort, Sorting.SelectionSort, Sorting.ShakerSort, Sorting.GnomeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, pow + 1); length *= 10)
                                SpeedOfSorting(Generate.CreateArrMod1000, length, Sorting.BitonicSort, Sorting.ShellSort,Sorting.TreeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, pow + 2); length *= 10)
                                SpeedOfSorting(Generate.CreateArrMod1000, length, Sorting.HeapSort, Sorting.QuickSort, Sorting.CountingSort, Sorting.MergeSort, Sorting.RadixSort);
                            break;
                    }
                    break;

                //Разбитые на подмассивы
                case 1:
                    switch (selectedGroupIndex)
                    {
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, pow); length *= 10)
                                SpeedOfSorting(Generate.CreateArrHalfSort, length, Sorting.BoubleSort, Sorting.CombSort, Sorting.InsertionSort, Sorting.SelectionSort, Sorting.ShakerSort, Sorting.GnomeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, pow + 1); length *= 10)
                                SpeedOfSorting(Generate.CreateArrHalfSort, length, Sorting.BitonicSort, Sorting.ShellSort, Sorting.TreeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, pow + 2); length *= 10)
                                SpeedOfSorting(Generate.CreateArrHalfSort, length, Sorting.HeapSort, Sorting.QuickSort, Sorting.CountingSort, Sorting.MergeSort, Sorting.RadixSort);
                            break;
                    }
                    break;

                //Отсортированные с перестановками
                case 2:
                    switch (selectedGroupIndex)
                    {
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, pow); length *= 10)
                                SpeedOfSorting(Generate.ChangeNumberArr, length, Sorting.BoubleSort, Sorting.CombSort, Sorting.InsertionSort, Sorting.SelectionSort, Sorting.ShakerSort, Sorting.GnomeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, pow + 1); length *= 10)
                                SpeedOfSorting(Generate.ChangeNumberArr, length, Sorting.BitonicSort, Sorting.ShellSort, Sorting.TreeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, pow + 2); length *= 10)
                                SpeedOfSorting(Generate.ChangeNumberArr, length, Sorting.HeapSort, Sorting.QuickSort, Sorting.CountingSort, Sorting.MergeSort, Sorting.RadixSort);
                            break;
                    }
                    break;

                //Отсортированные с перестановками и повторами
                case 3:
                    switch (selectedGroupIndex)
                    {
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, pow); length *= 10)
                                SpeedOfSorting(Generate.PrimeArr, length, Sorting.BoubleSort, Sorting.CombSort, Sorting.InsertionSort, Sorting.SelectionSort, Sorting.ShakerSort, Sorting.GnomeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, pow + 1); length *= 10)
                                SpeedOfSorting(Generate.PrimeArr, length, Sorting.BitonicSort, Sorting.ShellSort, Sorting.TreeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, pow + 2); length *= 10)
                                SpeedOfSorting(Generate.PrimeArr, length, Sorting.HeapSort, Sorting.QuickSort, Sorting.CountingSort, Sorting.MergeSort, Sorting.RadixSort);
                            break;
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<List<double>> list = new List<List<double>>();
            SetPath();
            //list.Clear();
            try
            {
                StreamReader sr = new StreamReader(pathToTime);
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] lineArray = line.Split(' ');
                    List<double> times = new List<double>();
                    foreach (string s in lineArray)
                    {
                        times.Add(Convert.ToDouble(s));
                    }
                    list.Add(times);

                    line = sr.ReadLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };

            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Время выполнения";
            pane.Title.Text = "Исследование скорости работы сортировок";

            for (int i = 0; i < list[0].Count(); i++)
            {
                PointPairList pointList = new PointPairList();
                int x = 10;

                for (int j = 0; j < list.Count(); j++)
                {
                    pointList.Add(x, list[j][i]);
                    x *= 10;
                }

                switch (i)
                {
                    case 0:
                        pane.AddCurve("Method: " + i, pointList, Color.Blue, SymbolType.Default);
                        break;
                    case 1:
                        pane.AddCurve("Method: " + i, pointList, Color.Red, SymbolType.Default);
                        break;
                    case 2:
                        pane.AddCurve("Method: " + i, pointList, Color.Black, SymbolType.Default);
                        break;
                    case 3:
                        pane.AddCurve("Method: " + i, pointList, Color.Yellow, SymbolType.Default);
                        break;
                    case 4:
                        pane.AddCurve("Method: " + i, pointList, Color.Green, SymbolType.Default);
                        break;
                    case 5:
                        pane.AddCurve("Method: " + i, pointList, Color.Violet, SymbolType.Default);
                        break;
                }
            }
            zedGraph.AxisChange();
            zedGraph.Invalidate();

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
