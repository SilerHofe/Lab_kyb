using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using MyLib;
using System.Diagnostics;

namespace lab_22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.XAxis.Title.Text = "Size";
            pane.YAxis.Title.Text = "Count of operation";
            pane.Title.Text = "The ratio between array sizes and count of operation";
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PointPairList array1 = new PointPairList();
            PointPairList array2 = new PointPairList();
            GraphPane pane = zedGraphControl1.GraphPane;
            Random rand = new Random();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    MyHashMap<int, int> map = new MyHashMap<int, int>();
                    MyTreeMap<int, int> tree = new MyTreeMap<int, int>();
                    for (int size = 100; size <= 1000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                int n = rand.Next(1, size);
                                map.Put(j, n);
                                tree.Put(j, n);
                            }
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            int tmp = rand.Next(0, size - 1);
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                                map.Get(tmp);
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                                tree.Get(tmp);
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        map.Clear();
                        tree.Clear();
                    }
                    break;
                case 1:
                    map = new MyHashMap<int, int>();
                    tree = new MyTreeMap<int, int>();
                    for (int size = 100; size <= 1000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            int tmp = rand.Next(0, size - 1);
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int n = rand.Next(1, size);
                                map.Put(j, n);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int n = rand.Next(1, size);
                                tree.Put(j, n);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        map.Clear();
                        tree.Clear();
                    }
                    break;
                case 2:
                    map = new MyHashMap<int, int>();
                    tree = new MyTreeMap<int, int>();
                    for (int size = 100; size <= 1000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                int n = rand.Next(1, size);
                                map.Put(j, n);
                                tree.Put(j, n);
                            }
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            int tmp = rand.Next(0, size - 1);
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int ind = rand.Next(0, map.Size() - 1);
                                map.Remove(ind);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int ind = rand.Next(0, tree.Size() - 1);
                                tree.Remove(ind);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        map.Clear();
                        tree.Clear();
                    }
                    break;
            }
            pane.CurveList.Clear();
            pane.AddCurve("HashMap", array1, Color.Blue, SymbolType.Default);
            pane.AddCurve("TreeMap", array2, Color.Red, SymbolType.Plus);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            pane.XAxis.Scale.Min = 100;
            pane.XAxis.Scale.Max = 1000;
            pane.YAxis.Scale.Min = -1;
            pane.XAxis.Scale.Min = 10;
            pane.XAxis.Title.Text = "Size";
            pane.YAxis.Title.Text = "Count of operation";
            pane.Title.Text = "The ratio between array sizes and count of operation";
        }
    }
}
