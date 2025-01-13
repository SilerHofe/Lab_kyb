using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class MyComporator<T> : Comparer<T> where T : IComparable
    {
        public override int Compare(T x, T y)
        {
            return x.CompareTo(y);
            throw new NotImplementedException();
        }
    }
    public class MyTreeMap<K, V> where K : IComparable
    {
        class Node
        {
            public K key { get; set; }
            public V value { get; set; }
            public Node left { get; set; }
            public Node right { get; set; }
            public Node(K Key, V Value)
            {
                key = Key;
                value = Value;
                left = null;
                right = null;
            }
        }
        MyComporator<K> comparator;
        Node root;
        int size;
        public MyTreeMap()
        {
            comparator = new MyComporator<K>();
            size = 0;
        }
        public MyTreeMap(MyComporator<K> comp)
        {
            comparator = comp;
        }
        public void Clear()
        {
            root = null;
            size = 0;
        }
        public bool containskey(K key)
        {
            Node p = root;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) > 0)
                    p = p.right;
                if (comparator.Compare(key, p.key) < 0)
                    p = p.left;
                else return true;
            }
            return false;
        }
        public bool ContainsValue(V value)
        {
            Node p = root;
            Stack<Node> stack = new Stack<Node>();
            while (p != null || stack.Count > 0)
            {
                while (p != null)
                {
                    if (p.value.Equals(value)) return true;
                    stack.Push(p);
                    p = p.right;
                }
                p = stack.Pop();
                p = p.left;
            }
            return false;
        }
        public List<KeyValuePair<K, V>> EntrySet()
        {
            List<KeyValuePair<K, V>> entries = new List<KeyValuePair<K, V>>();
            Node p = root;
            Stack<Node> stack = new Stack<Node>();
            while (p != null || stack.Count > 0)
            {
                while (p != null)
                {
                    entries.Add(new KeyValuePair<K, V>(p.key, p.value));
                    stack.Push(p);
                    p = p.right;
                }
                p = stack.Pop();
                p = p.left;
            }
            return entries;
        }
        public V Get(K key)
        {
            Node curr = root;
            while (curr != null)
            {
                if (key.CompareTo(curr.key) < 0)
                    curr = curr.left;
                else if (key.CompareTo(curr.key) > 0)
                    curr = curr.right;
                else
                    return curr.value;
            }
            return default(V);
        }
        public bool IsEmpty() => size == 0;
        public K[] keySet()
        {
            K[] set = new K[size];
            Node p = root;
            Stack<Node> stack = new Stack<Node>();
            int ind = 0;
            while (p != null || stack.Count > 0)
            {
                while (p != null)
                {
                    set[ind] = p.key;
                    stack.Push(p);
                    p = p.right;
                    ind++;
                }
                p = stack.Pop();
                p = p.left;
            }
            return set;
        }
        public void Put(K key, V value)
        {
            if (key == null) throw new ArgumentNullException("params key is null");
            if (root == null)
            {
                root = new Node(key, value);
                size = 1;
                return;
            }
            Node node = root;
            while (node != null)
            {

                if (comparator.Compare(key, node.key) < 0)
                {
                    if (node.right != null) node = node.right;
                    else
                    {
                        node.right = new Node(key, value);
                        size++;
                        return;
                    }
                }
                else if (comparator.Compare(key, node.key) == 0)
                {
                    node.value = value;
                    return;
                }
                else
                {
                    if (node.left != null) node = node.left;
                    else
                    {
                        node.left = new Node(key, value);
                        size++;
                        return;
                    }
                }
            }


        }

        public void Remove(K key)
        {
            if (comparator.Compare(key, root.key) == 0 && root.right == null && root.left == null)
            {
                root = null;
                size--;
                return;
            }
            Node high = root;
            Node p = root;
            if (comparator.Compare(key, root.key) < 0)
                p = root.left;
            else if (comparator.Compare(key, root.key) > 0)
                p = root.right;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) == 0)
                {
                    if (p.left == null && p.right == null)
                    {
                        if (comparator.Compare(p.key, high.key) < 0)
                        {
                            size--;
                            high.left = null;
                            return;
                        }
                        else
                        {
                            size--;
                            high.right = null;
                            return;
                        }
                    }
                    else if ((p.left == null && p.right != null) || (p.right == null && p.left != null))
                    {
                        if (p.left != null)
                        {
                            p.value = p.left.value;
                            p.key = p.left.key;
                            p.right = p.left.right;
                            p.left = p.left.left;
                            size--;
                            return;
                        }
                        else if (p.right != null)
                        {
                            p.value = p.right.value;
                            p.key = p.right.key;
                            p.right = p.right.right;
                            p.left = p.right.left;
                            size--;
                            return;
                        }
                    }
                    else if (p.left != null && p.right != null)
                    {
                        Node max = p.left;
                        if (max.right == null)
                            max = p.left;
                        while (max.right != null)
                            max = max.right;
                        Node maxHigh = max;
                        if (maxHigh.left != null)
                        {
                            p.value = max.value;
                            p.key = max.key;
                            maxHigh.value = maxHigh.left.value;
                            maxHigh.key = maxHigh.left.key;
                            maxHigh.left = maxHigh.left.left;
                        }
                        else if (maxHigh.left == null)
                        {
                            p.value = max.value;
                            p.key = max.key;
                            p.left.right = max.left;
                        }
                        size--;
                        return;
                    }
                }
                else if (comparator.Compare(key, p.key) < 0)
                {
                    high = p;
                    p = p.left;
                }
                else if (comparator.Compare(key, p.key) > 0)
                {
                    high = p;
                    p = p.right;
                }
            }
        }
        public int Size() => size;
        public K Firstkey()
        {
            if (root != null)
                return root.key;
            return default(K);
        }
        public K Lastkey()
        {
            Node p = root;
            while (p != null)
            {
                if (p.right == null)
                    return p.key;
                p = p.right;
            }
            return default(K);
        }
        public MyTreeMap<K, V> HeadMap(K end)
        {
            MyTreeMap<K, V> returnTree = new MyTreeMap<K, V>();
            Node p = root;
            Stack<Node> stack = new Stack<Node>();
            while (p != null || stack.Count > 0)
            {
                while (p != null)
                {
                    if (comparator.Compare(p.key, end) < 0)
                        returnTree.Put(p.key, p.value);
                    stack.Push(p);
                    p = p.left;
                }
                if (stack.Count > 0)
                {
                    p = stack.Pop();
                    if (comparator.Compare(p.key, end) >= 0)
                        break;
                    p = p.right;
                }
            }
            return returnTree;
        }
        public MyTreeMap<K, V> SubMap(K start, K end)
        {
            MyTreeMap<K, V> returnTree = new MyTreeMap<K, V>();
            Node p = root;
            Stack<Node> stack = new Stack<Node>();
            while (p != null || stack.Count > 0)
            {
                while (p != null)
                {
                    if (comparator.Compare(p.key, start) >= 0 && comparator.Compare(p.key, end) < 0)
                        returnTree.Put(p.key, p.value);
                    stack.Push(p);
                    p = p.left;
                }
                if (stack.Count > 0)
                {
                    p = stack.Pop();
                    p = p.right;
                }
            }
            return returnTree;
        }
        public MyTreeMap<K, V> TailMap(K start)
        {
            MyTreeMap<K, V> returnTree = new MyTreeMap<K, V>();
            Node p = root;
            Stack<Node> stack = new Stack<Node>();
            while (p != null || stack.Count > 0)
            {
                while (p != null)
                {
                    if (comparator.Compare(p.key, start) >= 0)
                        returnTree.Put(p.key, p.value);
                    stack.Push(p);
                    p = p.left;
                }
                if (stack.Count > 0)
                {
                    p = stack.Pop();
                    p = p.right;
                }
            }
            return returnTree;
        }
        public IEnumerable<KeyValuePair<K, V>> FirstEntry()
        {
            yield return new KeyValuePair<K, V>(root.key, root.value);
        }
        public IEnumerable<KeyValuePair<K, V>> LastEntry()
        {
            Node p = root;
            while (p != null)
            {
                if (p.right == null)
                    yield return new KeyValuePair<K, V>(root.key, root.value);
                p = p.right;
            }
        }
        public IEnumerable<KeyValuePair<K, V>> LowerEntry(K key)
        {
            Node p = root;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) < 0 || comparator.Compare(key, p.key) == 0)
                    p = p.left;
                else
                    yield return new KeyValuePair<K, V>(p.key, p.value);
            }
        }
        public IEnumerable<KeyValuePair<K, V>> FloorEntry(K key)
        {
            Node p = root;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) < 0)
                    p = p.left;
                else
                    yield return new KeyValuePair<K, V>(p.key, p.value);
            }
        }
        public IEnumerable<KeyValuePair<K, V>> HigherEntry(K key)
        {
            Node p = root;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) > 0 || comparator.Compare(key, p.key) == 0)
                    p = p.right;
                else
                    yield return new KeyValuePair<K, V>(p.key, p.value);
            }
        }
        public IEnumerable<KeyValuePair<K, V>> CeilingEntry(K key)
        {
            Node p = root;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) > 0)
                    p = p.right;
                else
                    yield return new KeyValuePair<K, V>(p.key, p.value);
            }
        }
        public K Lowerkey(K key)
        {
            Node p = root;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) < 0 || comparator.Compare(key, p.key) == 0)
                    p = p.left;
                else
                    return p.key;
            }
            return default(K);
        }
        public K Floorkey(K key)
        {
            Node p = root;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) < 0)
                    p = p.left;
                else
                    return p.key;
            }
            return default(K);
        }
        public K Higherkey(K key)
        {
            Node p = root;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) > 0 || comparator.Compare(key, p.key) == 0)
                    p = p.right;
                else
                    return p.key;
            }
            return default(K);
        }
        public K Ceilingkey(K key)
        {
            Node p = root;
            while (p != null)
            {
                if (comparator.Compare(key, p.key) > 0)
                    p = p.right;
                else
                    return p.key;
            }
            return default(K);
        }
        public K PollFirstEntry()
        {
            K key = root.key;
            Remove(root.key);
            return key;
        }
        public K PollLastEntry()
        {
            K key = Lastkey();
            Remove(key);
            return key;
        }
        public void Print()
        {
            if (root == null)
            {
                Console.WriteLine("TreeMap is empty");
                return;
            }
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node node = stack.Pop();
                Console.WriteLine($"key:{node.key} value:{node.value}");
                if (node.right != null) stack.Push(node.right);
                if (node.left != null) stack.Push(node.left);
            }
        }
    }
}
