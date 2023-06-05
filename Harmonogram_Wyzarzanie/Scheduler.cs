using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmonogram_Wyzarzanie
{
    public class Scheduler
    {
        // random do tworzenia randomowych liczb
        private Random random = new Random();

        // lista zadań właściwa (wzorzec) i zadania 
        // tymczasowe do pracy na nich
        private List<Job> tasks;
        private List<Job> tasks_temp = new List<Job>();
        private Hashtable tasks_hashtable = new Hashtable();

        // lista zadań oczekujacych
        private List<Job> tasksWait = new List<Job>();

        // kolejność zdań do pracy i ostateczna - najlepsze
        // rozwiązanie
        private List<int> kolejnosc_zadan = new List<int>();
        private List<int> ostateczna_kolejnosc_zadan = new List<int>();

        // liczba procesorow
        private int numProcessors = 0;

        // lista procesorów do pracy i ostateczna - najlepsze
        // rozwiązanie
        private List<Processor> processors_temp;
        private List<Processor> ostateczne_procesory;

        // wartość ostatecznego kosztu - funkcja kosztu
        private int ostateczny_koszt = 0;

        // konstruktor harmonogramu
        public Scheduler(List<Job> tasks, int numProcessors)
        {
            // zadania, losowanie kolejnosci zadan, dla lepszego
            // dzialania algorytmu, liczba procesorow
            // tablica hash, do sprawnego wyszukania elementów w pomieszanej
            // liscie
            this.tasks = tasks;
            this.tasks.Shuffle();

            for (int i = 0; i < tasks.Count; i++)
            {
                tasks_hashtable.Add(tasks[i].Id, i);
            }

            // liczba procesorow
            this.numProcessors = numProcessors;
        }
        // Algorytm symulowanego wyzarzania
        public void ScheduleTasks()
        {
            // ustawienie temperatury
            double temperature = 1000000;

            // predkosc spadku temperatury
            double coolingRate = 0.999;

            // ustawianie kolejnosci zadan - losowo
            int i = 0;
            foreach (Job job in tasks) kolejnosc_zadan.Add(i++);
            kolejnosc_zadan.Shuffle();

            //tworzenie harmonogramu na procesorach processors_temp
            Stworz_harmonogram(tasks, numProcessors);

            // inicjacja ostatecznych zmiennych najlepszego rozwiązania 
            ostateczna_kolejnosc_zadan = new List<int>(kolejnosc_zadan);
            ostateczny_koszt = Czas_wykonania_harmonogramu(processors_temp);
            ostateczne_procesory = processors_temp;

            // petla algorytmu symulowanego wyzarzania
            while (temperature > 0.0001) // warunek zakonczenia 
            {
                // generowanie sąsiedniego rozwiązania poprzez zamiane kolejności,
                // zamiana dwoch losowych zadan ze soba
                Swap(kolejnosc_zadan, random.Next(0, tasks.Count), random.Next(0, tasks.Count));

                // tworzenie harmonogramu na procesorach processors_temp
                Stworz_harmonogram(tasks, numProcessors);

                // kalkulacja funkcji kosztu dla obecnego harmonogramu
                int nowy_koszt = Czas_wykonania_harmonogramu(processors_temp);

                // warunek akceptacji gorszego rozwiazania i przypisywanie obecnego rozwiązania
                // jako najlepsze, jeśli warunek jest spełniony
                if (nowy_koszt < ostateczny_koszt || Math.Exp((ostateczny_koszt - nowy_koszt) / temperature) > random.NextDouble())
                {
                    ostateczna_kolejnosc_zadan = kolejnosc_zadan;
                    ostateczny_koszt = nowy_koszt;
                    ostateczne_procesory = processors_temp;
                }

                // chlodzenie
                temperature *= coolingRate;
            }
        }

        // zamiana dwoch elementow w liscie ze soba
        private static List<int> Swap(List<int> list, int indexA, int indexB)
        {
            int tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
        // wyswietlanie harmonogramu
        private void Wyswietl_harmonogram(List<Processor> processors)
        {
            Console.WriteLine("###########################################################################\n");
            Console.WriteLine("Harmonogram\n");
            // wyswietlanie informacji o kazdym procesorze
            foreach (Processor processor in processors)
            {
                processor.Wyswietl_dane_procesora();
            }
            Console.WriteLine("###########################################################################\n");
        }
        // czas wykonania harmonogramu, czyli czas pracy najdluzej pracujacego procesora
        private int Czas_wykonania_harmonogramu(List<Processor> processors)
        {
            int i = processors[0].Time;
            foreach (Processor processor in processors)
            {
                if (i < processor.Time) i = processor.Time;
            }
            return i;
        }
        // wyswietlanie czasu harmonogramu
        private void Wyswietl_laczny_czas(List<Processor> processors)
        {
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");

            int i = Czas_wykonania_harmonogramu(processors);

            Console.WriteLine("Czas harmonogramu:   " + i + " sekund");
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n");
        }

        // tworzenie nowych procesorow
        private List<Processor> Nowe_procesory(int numProcessors)
        {
            Processor.processorListFinishID = new List<int>();
            List<Processor> processors = new List<Processor>(numProcessors);

            for (int i = 0; i < numProcessors; i++)
            {
                // inicjacja procesorow i dodanie ich do listy
                Processor processor = new Processor(i);
                processors.Add(processor);
            }

            return processors;
        }

        // dodanie zadan do procesorow, wedlug kolejnosci, zaleznosci, jezeli
        // nie mozna dodac obecnie, zadanie idzie na kolejke czekania
        private void Dodaj_Zadania_Do_Procesorow(List<Processor> processors_t)
        {
            Job job1;
            Processor processor;

            // pierwsze przejscie po zadaniach, ktore mozna dodac
            // (mozliwosc zakleszczenia przy zlych danych)
            foreach (int i in kolejnosc_zadan)
            {
                processor = GetProcessorWithDependencies(processors_t, tasks_temp[i]);
                if (processor == null) tasksWait.Add(tasks_temp[i]);
                else processor.Add_Job(tasks_temp[i]);
            }

            // przejscie po kolejce czekania, dopoki wszystkie zadania, nie beda dodane
            // do wykonania przez procesory (mozliwosc zakleszczenia przy zlych danych)
            while (tasksWait.Count > 0)
            {
                List<Job> tasksWait1 = new List<Job>();

                foreach (Job job in tasksWait)
                {
                    processor = GetProcessorWithDependencies(processors_t, job);
                    if (processor == null) tasksWait1.Add(job);
                    else processor.Add_Job(job);
                }

                // czyszczenie kolejki czekania i tworzenie nowej z pozostalymi zadaniami
                // do przydzielenia do procesorow
                tasksWait.Clear();
                tasksWait.AddRange(tasksWait1);
            }
        }

        // klonowanie zadan do nowej listy (listy zadan do opracowania harmonogramu)
        private List<Job> Klonowanie_zadan(List<Job> tasks)
        {
            List<Job> list = new List<Job>();

            foreach (Job job in tasks)
            {
                Job job_copy = (Job)job.Clone();
                list.Add(job_copy);
            }
            return list;
        }

        // metoda dajaca procesor z listy, ktory ma najmniejszy czas wykonywania
        private Processor GetProcessorWithMinLoad(List<Processor> processors)
        {
            int minLoad = int.MaxValue;
            Processor best_processor = processors[0];

            foreach (Processor processor in processors)
            {
                if (processor.Time < minLoad)
                {
                    minLoad = processor.Time;
                    best_processor = processor;
                }
            }

            return best_processor;
        }

        // metoda dajaca procesor, ktory moze obecnie miec przypisane zadanie,
        // ze wzgledu na czas wykonywanych zadan na procesorze jak i zaleznosci,
        // miedzy innymi zadaniami
        private Processor GetProcessorWithDependencies(List<Processor> processors, Job job)
        {
            // lista procesorow, ktore moga miec przypisane zadanie ze wzgledu na
            // wykonane zadania zalezne i ich czas zakonczenia

            List<Processor> processors_depend = new List<Processor>();
            int time = 0;

            foreach (int dependency in job.Dependencies)
            {
                // sprawdzanie, czy kazdy z zadan zaleznych jest juz wykonany,
                // w przeciwnym razie zwraca null i zadanie trafia do kolejki oczekujacej 
                if (!Processor.processorListFinishID.Contains(dependency)) return null;

                // uzyskanie czasu najpozniej wykonanego zadania zaleznego, aby
                // mozna bylo wykonac zadanie (tablica haszujaca, aby mozna bylo
                // wydobyc dobre indeksy zadan z pomieszanej tablicy)
                int t = tasks_temp[(int)tasks_hashtable[dependency]].End;
                if (t > time) time = t;
            }

            // porownywanie czasu i tworzenie listy procesorow, do ktorych moze byc
            // przypisane zadanie (jezeli nie ma zaleznosci, lista procesorow bedzie
            // zawierac wszystkie mozliwe procesory)
            foreach (Processor p in processors)
            {
                if (p.Time == time) return p;
                if (p.Time > time)
                {
                    processors_depend.Add(p);
                }
            }

            // zwracanie wzglednie najlepszego procesora dla listy dostepnych procesorow
            return GetProcessorWithMinLoad(processors_depend);
        }
        // wyswietlanie harmonogramu ostatecznego rozwiazania, razem z czasem
        // wykonywania harmonogramu
        public void Wyswietl_harmonogram()
        {
            Wyswietl_harmonogram(ostateczne_procesory);
            Wyswietl_laczny_czas(ostateczne_procesory);
        }

        // tworzenie harmonogramu
        public void Stworz_harmonogram(List<Job> tasks, int numProcessors)
        {
            // tworzenie roboczych zadan i procesorow, dodawanie
            // zadan do procesorow na podstawie kolejnosi zadan
            tasks_temp = Klonowanie_zadan(tasks);
            processors_temp = Nowe_procesory(numProcessors);
            Dodaj_Zadania_Do_Procesorow(processors_temp);
        }
    }

    // rozszerzenie do obiektu list, aby mozna bylo przetasowac elementy listy
    public static class ListExtensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }



    }
}
