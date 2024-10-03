using MyStack;

namespace Lab_9
{
    class RPN
    {
        static public double Calculate(string input)
        {
            string outPut = GetString(input);
            double result = Counting(outPut);
            return result;
        }
        static private string GetString(string input)
        {
            string outPut = string.Empty;
            MyStack<string> operStack = new MyStack<string>();
            bool funcFlag = false;
            string inFunc = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimetr(input[i])) continue;
                if (char.IsDigit(input[i]))
                {
                    while (!IsDelimetr(input[i]) && !IsOperator(input[i]))
                    {
                        outPut += input[i];
                        i++;
                        if (i == input.Length) { i--; break; }
                    }
                    outPut += " ";
                    
                }

                if (IsOperator(input[i]) && !funcFlag)
                {
                    if (input[i] == '(') operStack.Push(input[i].ToString());
                    else if (input[i] == ')')
                    {
                        string s = operStack.Pop();
                        while (s != "(")
                        {
                            outPut += s.ToString() + " ";
                            if (operStack.Count() > -1) s = operStack.Pop();
                            else break;
                        }
                    }
                    else
                    {
                        if (operStack.Count() > -1)
                        {
                            if (GetPriority(input[i].ToString()) <= GetPriority(operStack.Peek().ToString())) outPut += operStack.Pop().ToString() + ' ';
                        }
                        operStack.Push(input[i].ToString());
                    }
                }
                if (char.IsLetter(input[i]) || funcFlag)
                {
                    if (!funcFlag)
                    {
                        while (input[i] != '(')
                        {
                            inFunc += input[i];
                            i++;
                        }
                        funcFlag = true;
                    }
                    if (IsFunc(inFunc))
                    {
                        if (input[i] == '(') operStack.Push(input[i].ToString());
                        else if (input[i] == ')')
                        {
                            string s = operStack.Pop();
                            while (s != "(")
                            {
                                outPut += s.ToString() + " ";
                                if (operStack.Count() > -1) s = operStack.Pop();
                                else break;
                            }
                            if (operStack.Search("(") == -1)
                            {
                                funcFlag = false;
                                operStack.Push(inFunc);
                            }
                        }
                        else
                        {
                            if (operStack.Count() > -1)
                            {
                                if (GetPriority(input[i].ToString()) <= GetPriority(operStack.Peek().ToString())) outPut += operStack.Pop().ToString() + ' ';
                            }
                            operStack.Push(input[i].ToString());
                        } 
                        
                    }
                }
            }
                while (operStack.Count() > -1)
                {
                    outPut += operStack.Pop() + " ";
                }
        return outPut;
        } 
        static private double Counting(string input)
        {
            double result = 0;
            MyStack<double> tmp= new MyStack<double>();
            for (int i=0;i<input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    string a = string.Empty;
                    while (!IsDelimetr(input[i]) && !IsOperator(input[i]))
                    {
                        a += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    tmp.Push(double.Parse(a));
                    i--;
                }
                if (IsOperator(input[i]))
                {
                    double a=tmp.Pop();
                    double b=tmp.Pop();
                    switch (input[i])
                    {
                        case '+':result = b + a; break;
                        case '-':result = b-a; break;
                        case '*':result = b*a; break;
                        case'/':result = b/a; break;
                        case '^':result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                    }
                    tmp.Push(result);
                }
                if (char.IsLetter(input[i]))
                {
                    string inFunc=string.Empty;
                    while (input[i] != ' ')
                    {
                        inFunc += input[i];  
                        i++;
                        if (i+1 == input.Length) break;
                    }
                    i--;
                    if (IsFunc(inFunc))
                    {
                        double a = tmp.Pop();
                        double b = default(double);
                        switch (inFunc)
                        {
                            case "sqrt":result = Math.Sqrt(a); break;
                            case "abs":result = Math.Abs(a); break;
                            case "sin":result=Math.Sin(a); break;
                            case "cos":result=Math.Cos(a); break;
                            case "tg":result = Math.Tan(a); break;
                            case "ln":result = Math.Log(a); break;
                            case "log":result = Math.Log10(a); break;
                            case "min": { b = tmp.Pop(); result = a < b ? a : b; break; }
                            case "max": { b = tmp.Pop(); result = a > b ? a : b; break; }
                            case "mod": { b = tmp.Pop(); result = a % b; break; }
                            case "div": { b = tmp.Pop(); result = Math.Floor(a / b); break; }
                            case "exp":result = Math.Exp(a);break;
                            case "floor":result = Math.Floor(a); break;
                        }
                        tmp.Push(result);
                    }
                }
            }
            return tmp.Peek();
        }
        static private bool IsDelimetr(char c)
        {
            if((" =".IndexOf(c)!=-1)) return true;
            return false;
        }
        static private bool IsOperator(char c)
        {
            if("+-/*^()".IndexOf(c)!=-1) return true;
            return false;
        }
        static private bool IsFunc(string c)
        {
            string[] func = { "sqrt", "abs", "sin", "cos", "tg", "ln", "log", "min", "max", "mod", "div", "exp", "fool" };
            for (int i = 0; i < func.Length; i++)
            {
                if (c == func[i]) return true;
            }
            return false;
        }
        static private byte GetPriority(string c)
        {
            switch (c)
            {
                case "(":return 0;
                case ")":return 1;
                case "+":return 2;
                case "-":return 3;
                case "*":return 4;
                case "/":return 4;
                case "^":
                case "sqrt":
                case "abs":
                case "sin":
                case "cos":
                case "tg":
                case "ln":
                case "log":
                case "min":
                case "max":
                case "mod":
                case "div":
                case "exp":
                case "fool":return 5;

                default: return 6;
            }
        }

    }
    class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите выражение: ");
                Console.WriteLine(RPN.Calculate(Console.ReadLine()));
            }
        }
    }
}