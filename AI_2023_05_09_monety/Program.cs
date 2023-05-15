
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

        bool[] data = new bool[100];
            for (int i = 0; i < data.Length - data.Length / 3; i++)
                data[i] = true;

            for (int i = data.Length - data.Length / 3; i < data.Length; i++)
                data[i] = false; ;

        //P(H)
            Variable<double> mean = Variable.Beta(1, 1);

        //P(E | H)
        for (int i = 0; i < data.Length; i++)
        {
            Variable<bool> x = Variable.Bernoulli(mean);
            x.ObservedValue = data[i];
        }

        var engine = new InferenceEngine();
        Console.WriteLine("mean: " + engine.Infer(mean));
    }
    }

