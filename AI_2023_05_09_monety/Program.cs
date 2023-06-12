
using Microsoft.ML.Probabilistic.Models;
using System;

class Program
    {
        static void Main(string[] args)
        {

        //var rng = new Random();     
        //E
        //true - orzeł
        //false - reszka
        //inicjacja danych - w tym przypadku 2/3 danych to orły, a 1/3 to reszki
        bool[] data = new bool[100];
            for (int i = 0; i < data.Length - data.Length / 3; i++)
                data[i] = true;

            for (int i = data.Length - data.Length / 3; i < data.Length; i++)
                data[i] = false; ;

        //P(H) - prawdopodobieństwo a priori (prawdopodobieństwo hipotezy, przed obserwacją nowych dowodów
            Variable<double> mean = Variable.Beta(1, 1);

        //P(E | H) - prawdopodobieństwo obserwacji przy założeniu hipotezy
        for (int i = 0; i < data.Length; i++)
        {
            Variable<bool> x = Variable.Bernoulli(mean);
            x.ObservedValue = data[i];
        }

        var engine = new InferenceEngine();
            
        //P(H | E) - prawdopodobieństwo a posteriori (prawdopodobieństwo hipotezy w świetle zaobserwowanych dowodów)
        Console.WriteLine("Inferred values: " + engine.Infer(mean));
    }
    }

