string path = "C:/Users/sashs/source/repos/ConsoleApp3/test.txt";
string? line;
int n;
double MatrixMult(int[][] Array, int[] Vector,int n)
{
    int[] Vector_Tmp=new int[n];
    for(int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            Vector_Tmp[i] += Array[j][i]*Vector[j];
    double Result = 0.0;
    for (int i = 0; i < n; i++) Result += Vector_Tmp[i] * Vector[i];
    return Result;
}
bool SimetryCheck(int[][] Array,int n)
{
    for(int i = 0; i < n; i++)
        for(int j = 0; j < n; j++)
        if (Array[i][j] != Array[j][i]) return false;
    return true;
}
try
{
    StreamReader sr = new StreamReader(path);
    line = sr.ReadLine();
    n =Convert.ToInt32(line);
    
    Console.WriteLine(n);
    int[][] Array = new int[n][];
    for (int i = 0; i < n; i++)
    {
        line = sr.ReadLine();
        Array[i] = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    }
    for (int i = 0; i < n; i++)
    {
        for(int j = 0; j < n; j++)
        {
            Console.Write($"{Array[i][j]}\t");
        }
        Console.WriteLine();
    }
    int[] Vector=new int[n];
    line = sr.ReadLine();
    Vector=line.Split(' ').Select(x=>Convert.ToInt32(x)).ToArray();
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine($"{Vector[i]}\t");
    }
    
    sr.Close();
    if (SimetryCheck(Array, n))
    {
        double Result = MatrixMult(Array, Vector, n);
        Console.WriteLine(Math.Sqrt(Result));
    }else Console.WriteLine("Matrix not simetry!");
} catch (Exception e)
{
    Console.Write("Exception: "+e.Message);
}
