
namespace Harmonogram_Wyzarzanie
{
    class Program
    {
        // Definicja zadań
        // Job(Id_zadania, czas_wykonania_zadania,lista powiazan {id_zadań,
        // które muszą zostać wykonane przed wykonaniem zadania})

        static readonly List<Job> jobs = new()
        {
            new Job(0, 211, new List<int>() ),
            new Job(1, 115, new List<int>() { 3 }),
            new Job(2, 101, new List<int>() { 1 }),
            new Job(3, 322, new List<int>() { 0 }),
            new Job(4, 135, new List<int>() { 2 }),
            new Job(5, 18, new List<int>() { 4 }),
            new Job(6, 112, new List<int>() { 0, 3 }),
            new Job(7, 228, new List<int>() { 2, 5 }),
            new Job(8, 22, new List<int>() { 1, 3 }),
            new Job(9, 137, new List<int>(){ 0 }),
            new Job(10, 341, new List<int>() { 5, 8, 9 }),
            new Job(11, 19, new List<int>() { 2, 4 }),
            new Job(12, 124, new List<int>()),
            new Job(13, 26, new List<int>() { 9 }),
            new Job(14, 121, new List<int>() { 5 }),
            new Job(15, 29, new List<int>() { 3, 4 }),
            new Job(16, 113, new List<int>() { 1, 2 }),
            new Job(17, 27, new List<int>() { 0, 3 }),
            new Job(18, 510, new List<int>() { 2, 4 }),
            new Job(19, 16, new List<int>() { 1 }),
            new Job(20, 647, new List<int>() { 11 }),
            new Job(21, 37, new List<int>() { 5 }),
            new Job(22, 490, new List<int>() { 5, 12 }),
            new Job(23, 48, new List<int>()),
            new Job(24, 256, new List<int>() { 2, 4 }),
            new Job(25, 327, new List<int>() { 2, 4 }),
            new Job(26, 125, new List<int>(){ 0 }),
            new Job(27, 69, new List<int>() { 0 }),
            new Job(28, 42, new List<int>() { 0 }),
            new Job(29, 371, new List<int>() { 0 }),
            new Job(30, 231, new List<int>() { 0 }),
            new Job(31, 154, new List<int>() { 1, 5 }),
            new Job(32, 17, new List<int>() { 0 }),
            new Job(33, 164, new List<int>(){ 0 }),
            new Job(34, 782, new List<int>() { 0, 3 }),
            new Job(35, 621, new List<int>() { 0 }),
            new Job(36, 139, new List<int>() { 0, 3 }),
            new Job(37, 575, new List<int>()),
            new Job(38, 117, new List<int>() { 0, 3 }),
            new Job(39, 2185, new List<int>() { 14, 15, 16 }),
            new Job(40, 216, new List<int>() { 0 }),
            new Job(41, 149, new List<int>() { 0 }),
            new Job(42, 478, new List<int>() { 0 }),
            new Job(43, 135, new List<int>() { 0 }),
            new Job(44, 360, new List<int>()),
            new Job(45, 229, new List<int>() { 0 }),
            new Job(46, 192, new List<int>() { 0, 2, 3 }),
            new Job(47, 144, new List<int>() { 0 }),
            new Job(48, 168, new List<int>() { 0 }),
            new Job(49, 127, new List<int>()),
            new Job(50, 191, new List<int>() { 14, 15, 16 }),
            new Job(51, 143, new List<int>() { 15, 16 }),
            new Job(52, 166, new List<int>() { 1, 5, 7, 9 }),
            new Job(53, 118, new List<int>() { 0 }),
            new Job(54, 138, new List<int>() { 0 }),
            new Job(55, 181, new List<int>() { 0, 2, 3 }),
            new Job(56, 113, new List<int>() { 0 }),
            new Job(57, 211, new List<int>() { 2 }),
            new Job(58, 115, new List<int>() { 3 }),
            new Job(59, 101, new List<int>() { 1 }),
            new Job(60, 322, new List<int>()),
            new Job(61, 135, new List<int>() { 2 }),
            new Job(62, 18, new List<int>() { 4 }),
            new Job(63, 112, new List<int>() { 0, 3 }),
            new Job(64, 228, new List<int>() { 2, 5 }),
            new Job(65, 22, new List<int>() { 1, 3 }),
            new Job(66, 137, new List<int>() { 0 }),
            new Job(67, 341, new List<int>() { 5, 8, 9 }),
            new Job(68, 19, new List<int>() { 2, 4 }),
            new Job(69, 124, new List<int>() { 0 }),
            new Job(70, 26, new List<int>() { 9 }),
            new Job(71, 121, new List<int>() { 5 }),
            new Job(72, 29, new List<int>() { 3, 4 }),
            new Job(73, 113, new List<int>() { 1, 2 }),
            new Job(74, 27, new List<int>() { 0, 3 }),
            new Job(75, 510, new List<int>() { 12, 4 }),
            new Job(76, 16, new List<int>() { 1 }),
            new Job(77, 647, new List<int>() { 11 }),
            new Job(78, 37, new List<int>() { 5 }),
            new Job(79, 490, new List<int>() { 5, 12 }),
            new Job(80, 48, new List<int>() { 0 }),
            new Job(81, 256, new List<int>() { 2, 4 }),
            new Job(82, 327, new List<int>() { 2, 4 }),
            new Job(83, 125, new List<int>() { 0 }),
            new Job(84, 69, new List<int>() { 0 }),
            new Job(85, 42, new List<int>() { 0 }),
            new Job(86, 371, new List<int>() { 0 }),
            new Job(87, 231, new List<int>() { 0 }),
            new Job(88, 154, new List<int>() { 15, 5, 21, 23, 55, 77 }),
            new Job(89, 17, new List<int>() { 0 }),
            new Job(90, 164, new List<int>() { 0 }),
            new Job(91, 782, new List<int>() { 0, 3, 4, 31, 22, 23,56 }),
            new Job(92, 621, new List<int>() { 0 }),
            new Job(93, 139, new List<int>() { 0, 3 , 1, 4, 51, 67 }),
            new Job(94, 575, new List<int>() { 0 }),
            new Job(95, 117, new List<int>() { 0, 3 }),
            new Job(96, 2185, new List<int>() { 14, 15, 16 }),
            new Job(97, 216, new List<int>() { 0 }),
            new Job(98, 149, new List<int>() { 0 }),
            new Job(99, 478, new List<int>() { 0 }),
            new Job(100, 135, new List<int>() { 0 })
        };

        static readonly int num_processors = 7; // liczba procesorów
        static void Main()
        {
            // zmienne do algorytmu symulowanego wyzarzania

            // temperatura
            int temperature = 100;

            // tempo schladzania
            double cooling_rate = 0.999;

            // Harmonogram, konstruktor (zadania, liczba dostępnych procesorów)
            Scheduler scheduler = new(jobs, num_processors, temperature, cooling_rate);

            // Uloz harmonogram, za pomocą algorytmu symulowanego wyżażania
            scheduler.ScheduleTasks();
        }
    }

    // klasa do roszerzen dzialan na listach
    public static class ListExtensions
    {
        private static readonly Random rng = new();

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

        // zamiana dwoch elementow w liscie ze soba
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}