using System.Reflection.Metadata.Ecma335;
public class MyHashMap<V,K>
{
    private class Node
    {
        public K Key {  get; set; }
        public V Value { get; set; }
        public Node Next { get; set; }
        public Node(K key,V value) 
        {
            Key=key;
            Value=value;
        }
    }

    int size;
    Node[] table;
    double loadFactor;
    public MyHashMap() 
    {
        table = new Node[16];
        size = 16;
        loadFactor = 0.75;
    }
    public MyHashMap(int initialCampacity)
    {
        table = new Node[initialCampacity];
        size = initialCampacity;
        loadFactor = 0.75;
    }
    public MyHashMap(int initialCampacity,double loadFactor)
    {
        table = new Node[initialCampacity];
        size = 0;
        loadFactor = loadFactor;
    }
    private int GetHashCode(K key)
    {
        return Math.Abs(key.GetHashCode()) % size;
    }
    private int GetHashCode(V key)
    {
        return Math.Abs(key.GetHashCode()) % size;
    }

    public void Clear()
    {
        size= 0;
    }
    public bool ContainsKey(K key)
    {
        int ind=GetHashCode(key);
        Node cur = table[ind];
        while (cur != null)
        {
            if (cur.Equals(key)) { return true; }
            cur = cur.Next;
        }
        return false;
    }
    public bool ContainsValue(V value) 
    {
        int ind = GetHashCode(value);
        Node cur = table[ind];
        while (cur != null)
        {
            if (cur.Equals(value)) { return true; }
            cur = cur.Next;
        }
        return false;
    }
    public IEnumerable<KeyValuePair<K,V>> EntrySet()
    {
        for (int i = 0; i < size; i++)
        {
            Node cur = table[i];
            while (cur != null)
            {
                yield return new KeyValuePair<K, V>(cur.Key, cur.Value);
                cur = cur.Next;
            }
        }
    }
    public V Get(K key)
    {
        int ind = GetHashCode(key);
        Node cur = table[ind];
        while (cur != null)
        {
            if (cur.Equals(key))
            {
                return cur.Value;
            }
            cur = cur.Next;
        }
        throw new KeyNotFoundException("Ключ не найден");
    }
    public bool IsEmpty()
    {
        return size == 0;
    }
    public K[] KeySet()
    {
        K[] t=new K[size];
        for(int i=0; i<size; i++)
        {
            Node cur = table[i];
            while(cur != null)
            {
                t[i] = cur.Key;
                cur = cur.Next;
            }
        }
        return t;
    }
    private void HelpPut(K key, V value)
    {
        int index = GetHashCode(key);
        Node step = table[index];
        if (step != null)
        {
            int fl = 1;
            while (step.Next != null)
            {
                if (step.Key.Equals(key))
                {
                    step.Value = value;
                    fl = 0;
                }
                step = step.Next;
            }
            if (step.Key.Equals(key))
            {
                step.Value = value;
                fl = 0;
            }
            if (fl == 1)
            {
                Node tmp = new Node(key, value);
                step.Next = tmp;
                step = tmp;
                size++;
            }
        }
        else
        {
            Node newTmp = new Node(key, value);
            table[index] = newTmp;
            size++;
        }
    }
    private void PutInNew(Node[] array, K key, V value)
    {
        int index = Math.Abs(key.GetHashCode()) % array.Length;
        Node tmp = new Node(key, value);
        if (array[index] != null)
        {
            Node step = array[index];
            while (step.Next != null)
                step = step.Next;
            step.Next = tmp;
        }
        else
            array[index] = tmp;
        size++;
    }
    private void Resize()
    {
        Node[] newArray = new Node[table.Length * 3];
        int prevSize = size;
        size = 0;
        for (int i = 0; i < table.Length; i++)
            if (table[i] != null)
            {
                Node value = table[i];
                while (value != null)
                {
                    int index = Math.Abs(value.Key.GetHashCode()) % newArray.Length;
                    Node NextValue = value.Next;
                    PutInNew(newArray, value.Key, value.Value);
                    value = NextValue;
                }
            }
        table = newArray;
    }
    public void Put(K key, V value)
    {
        double count = (double)(size + 1) / (double)table.Length;
        if (count >= loadFactor)
        {

        }

    }
    public void Remove(K key)
    {
        int ind=GetHashCode(key);
        if (table[ind] == null) return;
        Node cur = table[ind];
        Node prev = null;
        if (table[ind].Key.Equals(key)) 
        {
            table[ind] = table[ind].Next;
            size--;
            return;
        }
        while (cur != null)
        {
            if (cur.Key.Equals(key))
            {
                prev.Next = cur.Next;
                size--;
                return;
            }
            prev = cur;
            cur = cur.Next;
        }

    }
    public int Size()
    {
        return size;
    }
}