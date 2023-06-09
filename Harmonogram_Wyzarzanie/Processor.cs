using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmonogram_Wyzarzanie
{
    public class Processor
    {
        // lista id zadan skonczonych (dla wszystkich procesorow w ramach
        // tworzenia jednego harmonogramu)
        public static List<int> processorListFinishID = new List<int>();

        // id procesora
        public int Id { get; set; } = 0;

        // czas na procesorze
        public int Time { get; set; } = 0;

        // zadania na procesorze
        public List<Job> jobs = new List<Job>();

        // konstruktor procesora
        public Processor(int id)
        {
            Id = id;
        }

        // dodawanie zadania do procesora, uwzgledniajac czy to zadanie jest dodawane
        // do pustej kolejki procesora. Kazdy zadanie na jednym procesorze jest zwiazane
        // z poprzednim i nastepnym, oczywiscie jezeli jest poprzednie lub nastepne
        public void Add_Job(Job job)
        {
            if (jobs.Count > 0)
            {
                job.Poprzednie_Zadanie = jobs.Last();
                jobs.Last().Nastepne_Zadanie = job;
            }

            jobs.Add(job);
            // gdy zadanie jest dodane, jest oznaczone (dodane do listy) jako zakonczone,
            // czyli rozpatrzone w harmonogramie
            processorListFinishID.Add(job.Id);
            // do czasu procesora, dodawany jest czas zadania
            Time += job.Duration;
        }

        // wyswietlanie informacji o procesorze
        public void Wyswietl_dane_procesora()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Processor " + Id + "    " + Wyswietl_czas_procesora());
            Console.WriteLine("---------------------------------------------------------------------------");
            foreach (Job job in jobs)
            {
                job.Wyswietl_dane_zadania();
            }
            Console.WriteLine();
        }

        // wyswietlanie czasu pracy procesora
        public String Wyswietl_czas_procesora()
        {
            return "Czas pracy procesora: " + Time + " s";
        }

    }
}
