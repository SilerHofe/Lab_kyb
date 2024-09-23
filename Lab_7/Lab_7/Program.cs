using MyVector;
class Program
{
    static string path = "C:/Users/sashs/source/repos/Lab_7/Lab_7/input.txt";
    static string path1 = "C:/Users/sashs/source/repos/Lab_7/Lab_7/output.txt";
    static StreamReader input=new StreamReader(path);
    static StreamWriter output=new StreamWriter(path1);
    static MyVector<string> GetIp()
    {
        string line = input.ReadLine();
        if (line == null) Console.WriteLine("Пустая строка");
        MyVector<string> ip = new MyVector<string>(0,1);
        while(line != null)
        {
            string[]ipArray=line.Split(' ');
            foreach(string adres in ipArray)
            {
                bool isIp=true;
                int[]ipblock =adres.Split(".").Select(x=>Convert.ToInt32(x)).ToArray();
                foreach(int pice in ipblock)
                {
                    if(pice<0 || pice>255)isIp=false;
                }
                if (isIp && ipblock.Length == 4) ip.Add(adres);
            }
            line = input.ReadLine();
        }
        input.Close();
        return ip;
    }
    static void DeleteCopy(MyVector<string> ip)
    {
        for (int i = 0;i<ip.elementCount;i++)
        {
            for (int j=i+1;j<ip.elementCount;j++)
            {
                if (ip.Element(i) == ip.Element(j)) ip.Remove(ip.Element(j));
            }
        }
    }
    static void IpToFile(MyVector<string> ip)
    {
        DeleteCopy(ip);
        for (int i=0;i<ip.elementCount;i++)
        {
            string addres = ip.Get(i);
            output.WriteLine(addres);
        }
        output.Close();
    }
    static void Main(string[] args)
    {
        MyVector<string> ip = new MyVector<string>();
        ip = GetIp();
        IpToFile(ip);
    }
}