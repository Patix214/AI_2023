using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmonogram_Wyzarzanie
{
    public class Job : ICloneable
    {
        // referencja do poprzedniego zadania na przypisanym procesorze
        public Job Poprzednie_Zadanie { get; set; }

        // referencja do nastepnego zadania na przypisanym procesorze
        public Job Nastepne_Zadanie { get; set; }

        // id zadania
        public int Id { get; set; }
        // czas wykonania zadania
        public int Duration { get; set; } = -1;
        // zależności
        public List<int> Dependencies { get; set; }

        // kiedy zadanie się zaczeło
        public int Start => Poprzednie_Zadanie != null ? Poprzednie_Zadanie.End : 0;

        // kiedy zadanie się skończyło
        public int End => Start + Duration;

        // konstruktor zadania z id, czasem trwania, i zależnościami
        // (innymi zadaniami, ktore musza sie wykonac przed obecnym)
        public Job(int id, int duration, List<int> dependencies)
        {
            Id = id;
            Duration = duration;
            Dependencies = dependencies;
        }

        // wyswietlanie informacji o zadaniu
        public void Wyswietl_dane_zadania()
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine("Zadanie: " + Id);
            Console.Out.WriteLine("Czas trwania: " + Duration + " s");
            Console.Out.WriteLine("Start: " + Start + " s");
            Console.Out.WriteLine("Koniec: " + End + " s");
            Console.Out.WriteLine();
        }

        // klonowanie zadania
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
