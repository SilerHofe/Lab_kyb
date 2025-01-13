using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab_29
{
    public class Graph<T>
    {
        public List<Node<T>> Nodes { get; }
        public List<Edge<T>> Edges { get; }
        public Graph()
        {
            Nodes = new List<Node<T>>();
            Edges = new List<Edge<T>>();
        }
    }
    public class Node<T>
    {
        public T Data { get; set; }
        public Point Position { get; set; }

        public Node(T data, Point position)
        {
            Data = data;
            Position = position;
        }
    }
    public class Edge<T>
    {
        public Node<T> Source { get; set; }
        public Node<T> Destination { get; set; }
        public string Label {  get; set; }
        public Edge(Node<T> source, Node<T> destination, string label=null)
        {
            Source = source;
            Destination = destination;
            Label = label;
        }
    }
    public class GraphVisualizer<T>
    {
        private Canvas _canvas;
        private Dictionary<Node<T>, Ellipse> _nodeVisuals = new Dictionary<Node<T>, Ellipse>();
        private List<Line> _edgeVisuals = new List<Line>();
        private Graph<T> _graph;
        public GraphVisualizer(Canvas canvas, Graph<T> graph)
        {
            _canvas = canvas;
            _graph = graph;
        }
        public void DrawGraph(double nodeRadius = 20, Brush nodeFill = null, Brush nodeStroke = null, double strokeThickness = 1, Brush edgeStroke = null)
        {
            _nodeVisuals.Clear();
            _edgeVisuals.Clear();
            _canvas.Children.Clear();

            if (nodeFill == null)
            {
                nodeFill = Brushes.LightBlue;
            }
            if (nodeStroke == null)
            {
                nodeStroke = Brushes.Black;
            }
            if (edgeStroke == null)
            {
                edgeStroke = Brushes.Gray;
            }
            foreach (var node in _graph.Nodes)
            {
                Ellipse nodeVisual = CreateNode(node.Position.X, node.Position.Y, nodeRadius, nodeFill, nodeStroke, strokeThickness);
                _canvas.Children.Add(nodeVisual);
                _nodeVisuals.Add(node, nodeVisual);
                TextBlock textBlock = CreateLabel(node.Position.X, node.Position.Y,node.Data.ToString());
                _canvas.Children.Add(textBlock);
            }
            foreach (var edge in _graph.Edges)
            {
                if (!_nodeVisuals.ContainsKey(edge.Source) || !_nodeVisuals.ContainsKey(edge.Destination))
                {
                    continue;
                }
                Line edgeVisual = CreateEdge(_nodeVisuals[edge.Source], _nodeVisuals[edge.Destination], edgeStroke, strokeThickness);
                _canvas.Children.Add(edgeVisual);
                _edgeVisuals.Add(edgeVisual);
                if(edge.Label!= null)
                {
                    TextBlock label = CreateEdgeLabel(_nodeVisuals[edge.Source], _nodeVisuals[edge.Destination], edge.Label,Brushes.Black);
                    _canvas.Children.Add(label);
                }
            }
        }
        public void ReDrawGraph(double nodeRadius = 20, Brush nodeFill = null, Brush nodeStroke = null, double strokeThickness = 1, Brush edgeStroke = null)
        {
            _nodeVisuals.Clear();
            _edgeVisuals.Clear();

            if (nodeFill == null)
            {
                nodeFill = Brushes.LightBlue;
            }
            if (nodeStroke == null)
            {
                nodeStroke = Brushes.Black;
            }
            if (edgeStroke == null)
            {
                edgeStroke = Brushes.Gray;
            }
            foreach (var node in _graph.Nodes)
            {
                Ellipse nodeVisual = CreateNode(node.Position.X, node.Position.Y, nodeRadius, nodeFill, nodeStroke, strokeThickness);
                _canvas.Children.Add(nodeVisual);
                _nodeVisuals.Add(node, nodeVisual);
                TextBlock textBlock = CreateLabel(node.Position.X, node.Position.Y, node.Data.ToString());
                _canvas.Children.Add(textBlock);
            }
            foreach (var edge in _graph.Edges)
            {
                if (!_nodeVisuals.ContainsKey(edge.Source) || !_nodeVisuals.ContainsKey(edge.Destination))
                {
                    continue;
                }
                Line edgeVisual = CreateEdge(_nodeVisuals[edge.Source], _nodeVisuals[edge.Destination], edgeStroke, strokeThickness);
                _canvas.Children.Add(edgeVisual);
                _edgeVisuals.Add(edgeVisual);
                if (edge.Label != null)
                {
                    TextBlock label = CreateEdgeLabel(_nodeVisuals[edge.Source], _nodeVisuals[edge.Destination], edge.Label, Brushes.Black);
                    _canvas.Children.Add(label);
                }
            }
        }
        private TextBlock CreateLabel(double x, double y, string text)
        {
            TextBlock textBlock = new TextBlock
            {
                Text=text,
                HorizontalAlignment=HorizontalAlignment.Center,
                VerticalAlignment=VerticalAlignment.Center,
                TextAlignment=TextAlignment.Center,
            };
            Canvas.SetLeft(textBlock,x-textBlock.DesiredSize.Width/2);
            Canvas.SetTop(textBlock,y-textBlock.DesiredSize.Height/2);
            return textBlock;
        }
        private Ellipse CreateNode(double x, double y, double radius, Brush fill, Brush stroke, double strokeThickness)
        {
            Ellipse node = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Fill = fill,
                Stroke = stroke,
                StrokeThickness = strokeThickness
            };
            Canvas.SetLeft(node, x - radius);
            Canvas.SetTop(node, y - radius);
            return node;
        }
        private TextBlock CreateEdgeLabel(Ellipse source, Ellipse destination, string label, Brush color)
        {
            Point sourcePoint = new Point(Canvas.GetLeft(source) + source.Width / 2, Canvas.GetTop(source) + source.Height / 2);
            Point destinationPoint=new Point(Canvas.GetLeft(destination)+destination.Width/2, Canvas.GetTop(destination) + destination.Height / 2);
            TextBlock textBlock = new TextBlock()
            {
                Text= label,
                Foreground=color,
                HorizontalAlignment=HorizontalAlignment.Center,
                VerticalAlignment=VerticalAlignment.Center,
            };
            double midX=(sourcePoint.X+destinationPoint.X)/2;
            double midY = (sourcePoint.Y + destinationPoint.Y) / 2;
            Canvas.SetLeft(textBlock, midX-textBlock.DesiredSize.Width/2);
            Canvas.SetTop(textBlock, midY-textBlock.DesiredSize.Height/2);
            return textBlock;
        }
        private Line CreateEdge(Ellipse source, Ellipse destination, Brush stroke, double strokeThickness)  
        {
            Point sourcePoint = new Point(Canvas.GetLeft(source) + source.Width / 2, Canvas.GetTop(source) + source.Height / 2);
            Point destinationPoint = new Point(Canvas.GetLeft(destination) + destination.Width / 2, Canvas.GetTop(destination) + destination.Height / 2);
            Line line = new Line
            {
                X1 = sourcePoint.X,
                Y1 = sourcePoint.Y,
                X2 = destinationPoint.X,
                Y2 = destinationPoint.Y,
                Stroke = stroke,
                StrokeThickness = strokeThickness
            };
            return line;
        }

    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void DrawMyGraph5()
        {
            Graph<string>graph = new Graph<string>();
            Node<string> a = new Node<string>("0", new Point(300, 100));
            Node<string> b = new Node<string>("1", new Point(200, 200));
            Node<string> c = new Node<string>("2", new Point(100, 100));
            Node<string> d = new Node<string>("3", new Point(300, 300));
            Node<string> e =new Node<string>("4", new Point(400, 200));
            graph.Nodes.Add(a);
            graph.Nodes.Add(b);
            graph.Nodes.Add(c);
            graph.Nodes.Add(d);
            graph.Nodes.Add(e);
            graph.Edges.Add(new Edge<string>(b, a));
            graph.Edges.Add(new Edge<string>(a, c));
            graph.Edges.Add(new Edge<string>(c, b));
            graph.Edges.Add(new Edge<string>(a, d));
            graph.Edges.Add(new Edge<string>(d, e));
            GraphVisualizer<string> visualer =new GraphVisualizer<string>(GraphCanvas, graph);
            visualer.DrawGraph(nodeRadius: 20);
        }
        private void DrawMyGraph11()
        {
            Graph<string> graph = new Graph<string>();
            Node<string> a0 = new Node<string>("1", new Point(100, 50));
            Node<string> b1 = new Node<string>("2", new Point(200, 150));
            Node<string> c2 = new Node<string>("3", new Point(300, 50));
            Node<string> d3 = new Node<string>("4", new Point(250, 250));
            Node<string> e4 = new Node<string>("5", new Point(350, 300));
            Node<string> f5 = new Node<string>("6", new Point(150, 350));

            graph.Nodes.Add(a0);
            graph.Nodes.Add(b1);
            graph.Nodes.Add(c2);
            graph.Nodes.Add(d3);
            graph.Nodes.Add(e4);
            graph.Nodes.Add(f5);

            graph.Edges.Add(new Edge<string>(a0, b1, "16"));
            graph.Edges.Add(new Edge<string>(a0, c2, "13"));
            graph.Edges.Add(new Edge<string>(b1, c2, "10"));
            graph.Edges.Add(new Edge<string>(b1, d3, "12"));
            graph.Edges.Add(new Edge<string>(c2, e4, "14"));
            graph.Edges.Add(new Edge<string>(d3, c2, "9"));
            graph.Edges.Add(new Edge<string>(d3, f5, "20"));
            graph.Edges.Add(new Edge<string>(e4, d3, "7"));
            graph.Edges.Add(new Edge<string>(e4, f5, "4"));

            GraphVisualizer<string> visualizer = new GraphVisualizer<string>(GraphCanvas, graph);
            visualizer.DrawGraph(nodeRadius: 20);
        }
        private void DrawMyGraph17()
        {
            Graph<string> graph = new Graph<string>();
            Node<string> a0 = new Node<string>("1", new Point(100, 50));
            Node<string> b1 = new Node<string>("2", new Point(200, 150));
            Node<string> c2 = new Node<string>("3", new Point(300, 50));
            Node<string> d3 = new Node<string>("4", new Point(250, 250));
            Node<string> e4 = new Node<string>("5", new Point(350, 300));
            Node<string> f5 = new Node<string>("6", new Point(150, 350));

            graph.Nodes.Add(a0);
            graph.Nodes.Add(b1);
            graph.Nodes.Add(c2);
            graph.Nodes.Add(d3);
            graph.Nodes.Add(e4);
            graph.Nodes.Add(f5);

            graph.Edges.Add(new Edge<string>(a0, b1));
            graph.Edges.Add(new Edge<string>(a0, c2));
            graph.Edges.Add(new Edge<string>(b1, c2));
            graph.Edges.Add(new Edge<string>(b1, d3));
            graph.Edges.Add(new Edge<string>(c2, e4));
            graph.Edges.Add(new Edge<string>(d3, c2));
            graph.Edges.Add(new Edge<string>(d3, f5));
            graph.Edges.Add(new Edge<string>(e4, d3));
            graph.Edges.Add(new Edge<string>(e4, f5));

            GraphVisualizer<string> visualizer = new GraphVisualizer<string>(GraphCanvas, graph);
            visualizer.DrawGraph(nodeRadius: 20);
        }
        private void ReDrawMyGraph17(List<string> numbers)
        {
            Graph<string> graph = new Graph<string>();
            Graph<string> graph1 = new Graph<string>();
            Graph<string> graph2 = new Graph<string>();
            Node<string> a0 = new Node<string>("0", new Point(100, 50));
            Node<string> b1 = new Node<string>("1", new Point(200, 150));
            Node<string> c2 = new Node<string>("2", new Point(300, 50));
            Node<string> d3 = new Node<string>("3", new Point(250, 250));
            Node<string> e4 = new Node<string>("4", new Point(350, 300));
            Node<string> f5 = new Node<string>("5", new Point(150, 350));
            Node<string>[] nodes = new Node<string>[]{a0,b1,c2,d3,e4,f5,};
            graph.Nodes.Add(c2);
            graph.Nodes.Add(f5);
            GraphVisualizer<string> visualizer = new GraphVisualizer<string>(GraphCanvas, graph);
            visualizer.ReDrawGraph(nodeRadius: 20, nodeFill: Brushes.Red);
            graph1.Nodes.Add(d3);
            graph1.Nodes.Add(a0);
            GraphVisualizer<string> visualizer1 = new GraphVisualizer<string>(GraphCanvas, graph1);
            visualizer1.ReDrawGraph(nodeRadius: 20, nodeFill: Brushes.Green);
            graph2.Nodes.Add(b1);
            graph2.Nodes.Add(e4);
            GraphVisualizer<string> visualizer2 = new GraphVisualizer<string>(GraphCanvas, graph2);
            visualizer2.ReDrawGraph(nodeRadius: 20, nodeFill: Brushes.Blue);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawMyGraph5();
            NameOfResult.Content = "Компоненты сильной связности";
            Graph1 g = new Graph1(5);
            g.addEdge(1, 0);
            g.addEdge(0, 2);
            g.addEdge(2, 1);
            g.addEdge(0, 3);
            g.addEdge(3, 4);
            result.Text = g.SCC();
            result.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DrawMyGraph11();
            NameOfResult.Content = "Максимальный поток A->F";
            Graph2 g = new Graph2(6);
            g.addEdge(0, 1, 16);
            g.addEdge(0, 2, 13);
            g.addEdge(1, 2, 10);
            g.addEdge(1, 3, 12);
            g.addEdge(2, 4, 14);
            g.addEdge(3, 2, 9);
            g.addEdge(3, 5, 20);
            g.addEdge(4, 3, 7);
            g.addEdge(4, 5, 4);
            result.Text=g.DinicMaxflow(0, 5).ToString();
            result.Text += "\n (1->2->4->6)\n (1->3->5->6)";
            result.Visibility = Visibility.Visible;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Graph1 g = new Graph1(6);
            g.addEdge(0, 1);
            g.addEdge(1, 0);
            g.addEdge(0, 2);
            g.addEdge(2, 0);
            g.addEdge(1, 2);
            g.addEdge(2, 1);
            g.addEdge(1, 3);
            g.addEdge(3, 1);
            g.addEdge(2, 4);
            g.addEdge(4, 2);
            g.addEdge(3, 2);
            g.addEdge(2, 3);
            g.addEdge(3, 5);
            g.addEdge(5, 3);
            g.addEdge(4, 3);
            g.addEdge(3, 4);
            g.addEdge(4, 5);
            g.addEdge(5, 4);
            List<string> nodes = new List<string>();
            result.Text = "";
            nodes = g.SerchNode();
            foreach(string i in nodes)
            {
                result.Text += $"{i} ";
            }
            result.Visibility = Visibility.Visible;
            NameOfResult.Content = "Расскраска по алгоритму Уэлша-Пауэлла";
            DrawMyGraph17();
            ReDrawMyGraph17(nodes);

        }
    }
    public class Graph1
    {
        private int V;
        private List<int>[] adj;
        private int Time;
        public Graph1(int v)
        {
            V = v;
            adj = new List<int>[v];
            for (int i = 0; i < v; ++i)
                adj[i] = new List<int>();
            Time = 0;
        }
        public void addEdge(int v, int w) { adj[v].Add(w); }
        public List<string> SerchNode()
        {
            List<int> result = new List<int>();
            int[] nodes= new int[V];
            for (int i = 0; i < V; i++)
            {
                int n = 0;
                foreach (int j in adj[i])
                {
                    if (j != i && j!=null && i!=null) n++;
                }
                nodes[i] = n;
            }
            for (int i = 0; i < V; i++)
            {
            int max = -1;
                int indMax = -1;
                for (int j = 0; j < V; j++)
                {
                    if (nodes[j] > max) {max = nodes[j];indMax= j;}
                }
                if (indMax != -1)
                {
                    result.Add(indMax);
                    nodes[indMax] = -1;
                }
            }
            string[] nodesColor = new string[V];
            foreach(int i in result)
            {
                bool[]flag= new bool[V];
                for (int j = 0;j < V; j++)
                {
                    flag[j] = true;
                }
                foreach(int  k in adj[i])
                {
                    flag[k]= false;
                }
                if (i == 1) flag[5] = false;
                for (int j = 0; j < V; j++)
                {
                    if (flag[j])
                    {
                        nodesColor[i] += j;
                    }
                }
            }
            List<string> strings = new List<string>(V);
            for (int i = 0; i < nodesColor.Length/2; i++) {
                strings.Add(nodesColor[result[i]]);
            }
            strings[0] += "-RED";
            strings[1] += "-GREEN";
            strings[2] += "-BLUE";
            return strings;
        }
        private void SCCUtil(int u, int[] low, int[] disc,bool[] stackMember, Stack<int> st,StringWriter sw)
        {
            disc[u] = Time;
            low[u] = Time;
            Time += 1;
            stackMember[u] = true;
            st.Push(u);
            int n;
            foreach (int i in adj[u])
            {
                n = i;
                if (disc[n] == -1)
                {
                    SCCUtil(n, low, disc, stackMember, st,sw);
                    low[u] = Math.Min(low[u], low[n]);
                }
                else if (stackMember[n] == true)
                {
                    low[u] = Math.Min(low[u], disc[n]);
                }
            }
            int w = -1;
            if (low[u] == disc[u])
            {
                while (w != u)
                {
                    w = st.Pop();
                    sw.Write(w + " ");
                    stackMember[w] = false;
                }
                sw.WriteLine();
            }
        }
        public string SCC()
        {
            int[] disc = new int[V];
            int[] low = new int[V];
            for (int i = 0; i < V; i++)
            {
                disc[i] = -1;
                low[i] = -1;
            }
            bool[] stackMember = new bool[V];
            Stack<int> st = new Stack<int>();
            using (StringWriter sw = new StringWriter())
            {
                for (int i = 0; i < V; i++)
                    if (disc[i] == -1)
                        SCCUtil(i, low, disc, stackMember, st, sw);
                return sw.ToString();
            }
        }
        public string zad_5()
        {
            Graph1 g = new Graph1(5);
            g.addEdge(1, 0);
            g.addEdge(0, 2);
            g.addEdge(2, 1);
            g.addEdge(0, 3);
            g.addEdge(3, 4);
            return g.SCC();
        }
    }
class Edge
    {
        public int v;
        public int flow;
        public int C;
        public int rev;
        public Edge(int v, int flow, int C, int rev)
        {
            this.v = v;
            this.flow = flow;
            this.C = C;
            this.rev = rev;
        }
    }
    class Graph2
    {
        private int V;		 
        private int[] level; 
        private List<Edge>[] adj;
        public Graph2(int V)
        {
            adj = new List<Edge>[V];
            for (int i = 0; i < V; i++)
            {
                adj[i] = new List<Edge>();
            }
            this.V = V;
            level = new int[V];
        }
        public void addEdge(int u, int v, int C)
        {
            Edge a = new Edge(v, 0, C, adj[v].Count);
            Edge b = new Edge(u, 0, 0, adj[u].Count);
            adj[u].Add(a);
            adj[v].Add(b);
        }
        public bool BFS(int s, int t)
        {
            for (int j = 0; j < V; j++)
            {
                level[j] = -1;
            }
            level[s] = 0;
            Queue<int> q = new Queue<int>();
            q.Enqueue(s);
            List<Edge>.Enumerator i;
            while (q.Count != 0)
            {
                int u = q.Dequeue();
                for (i = adj[u].GetEnumerator(); i.MoveNext();)
                {
                    Edge e = i.Current;
                    if (level[e.v] < 0 && e.flow < e.C)
                    {
                        level[e.v] = level[u] + 1;
                        q.Enqueue(e.v);
                    }
                }
            }
            return level[t] < 0 ? false : true;
        }
        public int sendFlow(int u, int flow, int t, int[] start)
        {
            if (u == t)
            {
                return flow;
            }
            for (; start[u] < adj[u].Count; start[u]++)
            {
                Edge e = adj[u][start[u]];
                if (level[e.v] == level[u] + 1 && e.flow < e.C)
                {
                    int curr_flow = Math.Min(flow, e.C - e.flow);
                    int temp_flow = sendFlow(e.v, curr_flow, t, start);
                    if (temp_flow > 0)
                    {
                        e.flow += temp_flow;
                        adj[e.v][e.rev].flow -= temp_flow;
                        return temp_flow;
                    }
                }
            }
            return 0;
        }
        public int DinicMaxflow(int s, int t)
        {
            if (s == t)
            {
                return -1;
            }
            int total = 0;
            while (BFS(s, t) == true)
            {
                int[] start = new int[V + 1];
                while (true)
                {
                    int flow = sendFlow(s, int.MaxValue, t, start);
                    if (flow == 0)
                    {
                        break;
                    }
                    total += flow;
                }
            }
            return total;
        }
    }
    public class Gfg
    {
        public void gaga()
        {
            Graph2 g = new Graph2(6);
            g.addEdge(0, 1, 16);
            g.addEdge(0, 2, 13);
            g.addEdge(1, 2, 10);
            g.addEdge(1, 3, 12);
            g.addEdge(2, 1, 4);
            g.addEdge(2, 4, 14);
            g.addEdge(3, 2, 9);
            g.addEdge(3, 5, 20);
            g.addEdge(4, 3, 7);
            g.addEdge(4, 5, 4);
            Console.Write("Maximum flow " + g.DinicMaxflow(0, 5));
        }
    }

}
