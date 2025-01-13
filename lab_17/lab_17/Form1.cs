using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLinketList;
using MyArrayList;
using System.Diagnostics;
using ZedGraph;

namespace lab_17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GraphPane pane = zedGraph1.GraphPane;
            pane.XAxis.Title.Text = "Size";
            pane.YAxis.Title.Text = "Count of operation";
            pane.Title.Text = "The ratio between array sizes and count of operation";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            zedGraph1.GraphPane.XAxis.Title.Text = "Size";
            zedGraph1.GraphPane.YAxis.Title.Text = "Count of operation";
            zedGraph1.GraphPane.Title.Text = "The ratio between array sizes and count of operation";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PointPairList array1 = new PointPairList();
            PointPairList array2 = new PointPairList();
            GraphPane pane = zedGraph1.GraphPane;
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    MyArrayList<int> array = new MyArrayList<int>(10);
                    MyLinkedList<int> linkarray = new MyLinkedList<int>();
                    for (int size = 100; size <= 1000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                array.Add(j);
                                linkarray.Add(j);
                            }
                            Random rand = new Random();
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            int tmp = rand.Next(0, size - 1);
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                                array.Get(tmp);
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                                linkarray.Get(tmp);
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        array.Clear();
                        linkarray.Clear();
                    }
                    break;
                case 1:
                    array = new MyArrayList<int>(10);
                    linkarray = new MyLinkedList<int>();
                    for (int size = 100; size <= 1000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                array.Add(j);
                                linkarray.Add(j);
                            }
                            Random rand = new Random();
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int number = rand.Next(0, 100000);
                                int ind = rand.Next(0, array.Size() - 1);
                                array.Set(ind, number);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int number = rand.Next(0, 100000);
                                int ind = rand.Next(0, linkarray.Size() - 1);
                                linkarray.Set(ind, number);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        array.Clear();
                        linkarray.Clear();
                    }
                    break;
                case 2:
                    array = new MyArrayList<int>(10);
                    linkarray = new MyLinkedList<int>();
                    for (int size = 100; size <= 100000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                                array.Add(j);
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                                linkarray.Add(j);
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        array.Clear();
                        linkarray.Clear();
                    }
                    break;
                case 3:
                    array = new MyArrayList<int>(10);
                    linkarray = new MyLinkedList<int>();
                    for (int size = 100; size <= 1000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                array.Add(j);
                                linkarray.Add(j);
                            }
                            Random rand = new Random();
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            int tmp = rand.Next(0, size - 1);
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int number = rand.Next(0, 100000);
                                int ind = rand.Next(0, array.Size() - 1);
                                array.AddInd(ind, number);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int number = rand.Next(0, 100000);
                                int ind = rand.Next(0, linkarray.Size() - 1);
                                linkarray.Add(ind, number);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        array.Clear();
                        linkarray.Clear();
                    }
                    break;
                case 4:
                    array = new MyArrayList<int>(10);
                    linkarray = new MyLinkedList<int>();
                    for (int size = 100; size <= 1000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                array.Add(j);
                                linkarray.Add(j);
                            }
                            Random rand = new Random();
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            int tmp = rand.Next(0, size - 1);
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int ind = rand.Next(0, array.Size() - 1);
                                array.Remove(ind);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int ind = rand.Next(0, linkarray.Size() - 1);
                                linkarray.Remove(ind);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        array.Clear();
                        linkarray.Clear();
                    }
                    break;
            }
            pane.CurveList.Clear();
            pane.AddCurve("Array", array1, Color.Yellow, SymbolType.Default);
            pane.AddCurve("List", array2, Color.Red, SymbolType.Default);
            zedGraph1.AxisChange();
            zedGraph1.Invalidate();
            pane.XAxis.Scale.Min = 100;
            pane.XAxis.Scale.Max = 1000;
            pane.YAxis.Scale.Min = -1;
            pane.XAxis.Scale.Min = 10;
            pane.XAxis.Title.Text = "Size";
            pane.YAxis.Title.Text = "Count of operation";
            pane.Title.Text = "The ratio between array sizes and count of operation";

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void zedGraph1_Load(object sender, EventArgs e)
        {

        }
    }
}
