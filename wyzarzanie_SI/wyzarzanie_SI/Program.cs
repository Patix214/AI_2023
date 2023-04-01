// See https://aka.ms/new-console-template for more information
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

delegate double Funkcja(double x);
class Program
{


    //s jest przykładowym argumentem 
    //s* argument dla którego wartość funckji kosztu jest mniejsza bądź równa funkcji kosztu, celu f dla wszystkich elementów, argumentów
    //dziedziny
    //randomowa liczba z zakresu S, np. z rozkładu jednostajnego

    static Random random = new Random();

    static void Main(string[] args)
    {
        double x0 = 4;
        double t0 = 1000;
        double alpha = 0.99;
        int iteracje = 1000000;

        double wynik = Wyzarzanie(Kwadrat, x0, t0, alpha, iteracje);
        Console.WriteLine(wynik);
    }



    public static double Kwadrat(double x)
    {
        //S przedział od [-10 do 10]
        //s*=0 (s*f = 0)
        return x * x;
    }

    public static bool Stopping_Criterion(int i, int i_max)
    {
        if (i < i_max) return true;
        return false;
    }

    public static bool Acceptance_Criterion(double delta, double t)
    {
        Random rand = new Random();
        if (delta < 0 )
        {
            return true;
        }
        return rand.Next()< Math.Exp(-delta / t);
    }

    public static double Losuj(double min, double max)
    {
        Random r = new Random();
        return min + r.NextDouble() * (max - min);
    }

    public static double Wyzarzanie(Funkcja f, double s0, double t0, double t_alpha, int max_iteration)
    {
        //1
        double best_solution = s0;
        double incumbent_solution = s0;

        double s = 0;
        double delta = 0;
        //2
        int i = 0;
        //3
        double t = t0;
        double tk = t0;


        while (Stopping_Criterion(i, max_iteration))
        {
            s = Losuj(Math.Max(-10, incumbent_solution - t), Math.Min(10, incumbent_solution + t));
            delta = f(s) - f(best_solution);

            if (Acceptance_Criterion(delta, t))
            {
                incumbent_solution = s;

                if (f(incumbent_solution) < f(best_solution))
                {
                    best_solution = f(incumbent_solution);
                }
            }

            t *= t_alpha;
            if (t < 0) t = tk;
            i = i + 1;
        }

        return incumbent_solution;
    }


}