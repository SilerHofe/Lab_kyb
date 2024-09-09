using System.Net.Http.Headers;
using System.Numerics;
namespace KSAD_Lab2
{
    internal class Complex
    {
        private double real, imaginary;
        public double Real { get { return real; } set { real = value; } }
        public double Imaginary { get { return imaginary; } set { imaginary = value; } }
        public Complex(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }
        public static Complex operator +(Complex x, Complex y)
        {
            return new Complex(x.real + y.real, x.imaginary + y.imaginary);
        }
        public static Complex operator -(Complex x, Complex y)
        {
            return new Complex(x.real - y.real, x.imaginary - y.imaginary);
        }
        public static Complex operator *(Complex x, Complex y)
        {
            return new Complex(x.real*y.real-x.imaginary*y.imaginary,x.real*y.imaginary+x.imaginary*y.real);
        }
        public static Complex operator /(Complex x,Complex y)
        {
            double denominator = y.real * y.real + y.imaginary * y.imaginary;
            return new Complex((x.real * y.real + x.imaginary * y.imaginary) / denominator, (x.imaginary*y.real-x.real*y.imaginary)/denominator);

        }
        public static double Abs(Complex x)
        {
            return Math.Sqrt(x.real * x.real + x.imaginary * x.imaginary);
        }
        public static double Argument(Complex x)
        {
            return Math.Atan(x.imaginary/ x.real);
        }

        public static void Print(Complex x)
        {
            if (x.imaginary >= 0) Console.WriteLine($"{x.real}+{x.imaginary}i");
            else Console.WriteLine($"{x.real}-{Math.Abs(x.imaginary)}i");
        }
    }
    class Program
    {
        static void Main()
        {
            bool f = true;
            Complex x = new Complex(0, 0);
            Complex y = new Complex(0, 0);
            while (f)
            {
                Console.WriteLine("Введите 0 для создания комплексного числа");
                Console.WriteLine("Введиет 1 для сложения");
                Console.WriteLine("Введите 2 для вычитания");
                Console.WriteLine("Введите 3 для произведения");
                Console.WriteLine("Введите 4 для деления");
                Console.WriteLine("Введите 5 для вычисления модуля");
                Console.WriteLine("Введите 6 для нахождения аргумента");
                Console.WriteLine("Введите q/Q для выхода");
                char enter;
                enter = Convert.ToChar(Console.ReadLine());
                switch (enter) 
                {
                    case '0':
                        Console.WriteLine("Введите вещественную часть числа");
                        double real1=Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите мнимую часть числа");
                        double imaginary1 = Convert.ToDouble(Console.ReadLine());
                        x = new Complex(real1, imaginary1);

                        continue;
                    case '1':
                        Console.WriteLine("Введите вещественную часть числа");
                        double real2 = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите мнимую часть числа");
                        double imaginary2 = Convert.ToDouble(Console.ReadLine());
                        y = new Complex(real2, imaginary2);
                        Complex sum = x + y;
                        Complex.Print(sum);
                        continue;
                    case '2':
                        Console.WriteLine("Введите вещественную часть числа");
                        real2 = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите мнимую часть числа");
                        imaginary2 = Convert.ToDouble(Console.ReadLine());
                        y = new Complex(real2, imaginary2);
                        Complex dif = x - y;
                        Complex.Print(dif);
                        continue;
                    case '3':
                        Console.WriteLine("Введите вещественную часть числа");
                        real2 = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите мнимую часть числа");
                        imaginary2 = Convert.ToDouble(Console.ReadLine());
                        y = new Complex(real2, imaginary2);
                        Complex mult = x * y;
                        Complex.Print(mult);
                        continue;
                    case '4':
                        Console.WriteLine("Введите вещественную часть числа");
                        real2 = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите мнимую часть числа");
                        imaginary2 = Convert.ToDouble(Console.ReadLine());
                        y = new Complex(real2, imaginary2);
                        Complex del = x / y;
                        Complex.Print(del);
                        continue;
                    case '5':
                        Console.WriteLine("Если хотите найти модуль введенного числа введите 1 если хотитее ввести новое введите 2");
                        char enter1;
                        enter1=Convert.ToChar(Console.ReadLine());
                        switch (enter1)
                        {
                            case '1':
                                double abs = Complex.Abs(x);
                                Console.WriteLine(abs);
                                break;
                            case '2':
                                Console.WriteLine("Введите вещественную часть числа");
                                real2 = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Введите мнимую часть числа");
                                imaginary2 = Convert.ToDouble(Console.ReadLine());
                                y = new Complex(real2, imaginary2);
                                abs = Complex.Abs(y);
                                Console.WriteLine(abs);
                                break;
                        }
                        continue;
                    case '6':
                        Console.WriteLine("Если хотите найти аргумент введенного числа введите 1 если хотите ввести новое введите 2");
                        enter1 = Convert.ToChar(Console.ReadLine());
                        switch (enter1)
                        {
                            case '1':
                                double arg = Complex.Argument(x);
                                Console.WriteLine(arg);
                                break;
                            case '2':
                                Console.WriteLine("Введите вещественную часть числа");
                                real2 = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Введите мнимую часть числа");
                                imaginary2 = Convert.ToDouble(Console.ReadLine());
                                y = new Complex(real2, imaginary2);
                                arg = Complex.Argument(y);
                                Console.WriteLine(arg);
                                break;
                        }
                        continue;
                    case 'q':
                        f=false;
                        break;
                    case 'Q':
                        f = false;
                        break;


                    default: 
                        Console.WriteLine("Неизвестная команда"); 
                        continue;
                }
            }
        }
    }
}
