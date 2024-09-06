string path = "C:/Users/sashs/source/repos/ConsoleApp3/test.txt";
string? line;
int n=0;
int[][] array = null;
int[] vector = null;

double MatrixMult(int[][] array, int[] vector,int n)
{
    int[] vector_tmp=new int[n];
    for(int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            vector_tmp[i] += array[j][i]*vector[j];
    double result = 0.0;
    for (int i = 0; i < n; i++) result += vector_tmp[i] * vector[i];
    return result;
}
bool SimetryCheck(int[][] array,int n)
{
    for(int i = 0; i < n; i++)
        for(int j = i; j < n; j++)
        if (array[i][j] != array[j][i]) return false;
    return true;
}
try
{
    StreamReader sr = new StreamReader(path);
    line = sr.ReadLine();
    n =Convert.ToInt32(line);
    array = new int[n][];
    vector = new int[n];
    for (int i = 0; i < n; i++)
    {
        line = sr.ReadLine();
        array[i] = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    }
    line = sr.ReadLine();
    vector=line.Split(' ').Select(x=>Convert.ToInt32(x)).ToArray();
    
    sr.Close();
} catch (Exception e)
{
    Console.Write("Exception: "+e.Message);
}
Console.WriteLine(n);
for (int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {
        Console.Write($"{array[i][j]}\t");
    }
    Console.WriteLine();
}
for (int i = 0; i < n; i++)
{
    Console.WriteLine($"{vector[i]}\t");
}
if (SimetryCheck(array, n))
{
    double result = MatrixMult(array, vector, n);
    Console.WriteLine(Math.Sqrt(result));
}else Console.WriteLine("Matrix not simetry!");
