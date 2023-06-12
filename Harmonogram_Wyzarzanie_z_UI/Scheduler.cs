using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Harmonogram_Wyzarzanie
{
    public class Scheduler
    {
        // zadania, liczba procesorow, kolejnosc zadan
        // do algorytmu symulowanego wyzarzania - temperatura i tempo schladzania

        readonly List<Job> jobs;
        readonly int num_processors;
        List<int> order_of_jobs = new();

        double temperature;
        readonly double coolingRate;

        //konstruktor planisty
        public Scheduler(List<Job> jobs, int numProcessors, double temperature, double coolingRate)
        {
            this.jobs = Schedule.Cloning_jobs(jobs);
            this.jobs.Shuffle();
            this.num_processors = numProcessors;
            this.temperature = temperature;
            this.coolingRate = coolingRate;
        }

        // uruchomienie przydzielania zadan
        public void ScheduleTasks()
        {
            Random random = new();

            // losowanie kolejnosci zadan
            Random_order_of_tasks();

            // tworzenie pierwszego harmonogramu, ktory bedzie pierwszym do porownywania
            Schedule bestSchedule = new(jobs, num_processors, order_of_jobs);
            bestSchedule.Add_jobs_to_processors();
            

            // petla algorytmu symulowanego wyzarzania
            while (temperature > 0.0001) // warunek zakonczenia 
            {
                // generowanie sąsiedniego rozwiązania poprzez zamiane kolejności,
                // zamiana dwoch losowych zadan ze soba
                order_of_jobs = new List<int>(bestSchedule.Order_of_jobs);
                order_of_jobs.Swap(random.Next(0, jobs.Count), random.Next(0, jobs.Count));

                Schedule schedule = new(jobs, num_processors, order_of_jobs);
                schedule.Add_jobs_to_processors();

                // kalkulacja funkcji kosztu dla obecnego harmonogramu i obecnie najlepszego
                int new_cost = schedule.Get_schedule_execution_time();
                int final_cost = bestSchedule.Get_schedule_execution_time();

                // warunek akceptacji gorszego rozwiazania i przypisywanie obecnego rozwiazania
                // jako najlepsze, jesli warunek jest spelniony
                if (new_cost < final_cost || Math.Exp((final_cost - new_cost) / temperature) > random.NextDouble())
                {
                    bestSchedule = schedule;
                }

                // chlodzenie
                temperature *= coolingRate;
            }

            // wyswietlenie najlepszego harmonogramu wyznaczonego przez algorytm
            bestSchedule.Show_full_schedule();
        }

        // losowanie kolejnosci zadan
        void Random_order_of_tasks()
        {
            order_of_jobs.Clear();
            foreach (Job job in jobs) order_of_jobs.Add(jobs.FindIndex(a => a.Equals(job)));
            order_of_jobs.Shuffle();
        }
    }
}
