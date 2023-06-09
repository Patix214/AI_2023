

namespace Harmonogram_Wyzarzanie
{
    class Program
    {
        static void Main(string[] args)
        {
            // Definicja zadań
            // Job(Id_zadania, czas_wykonania_zadania,lista powiazan {id_zadań,
            // które muszą zostać wykonane przed zaczęciem zadania})

            List<Job> tasks = new List<Job>()
        {
            new Job(0, 211, new List<int>() { 2 }),
            new Job(1, 115, new List<int>() { 3 }),
            new Job(2, 101, new List<int>() { 1 }),
            new Job(3, 322, new List<int>() { }),
            new Job(4, 135, new List<int>() { 2 }),
            new Job(5, 18, new List<int>() { 4 }),
            new Job(6, 112, new List<int>() { 0, 3 }),
            new Job(7, 228, new List<int>() { 2, 5 }),
            new Job(8, 22, new List<int>() { 1, 3 }),
            new Job(9, 137, new List<int>() {  }),
            new Job(10, 341, new List<int>() { 5, 8, 9 }),
            new Job(11, 19, new List<int>() { 2, 4 }),
            new Job(12, 124, new List<int>() { 0, }),
            new Job(13, 26, new List<int>() {  9 }),
            new Job(14, 121, new List<int>() { 5, }),
            new Job(15, 29, new List<int>() { 3, 4,}),
            new Job(16, 113, new List<int>() { 1, 2 }),
            new Job(17, 27, new List<int>() { 0, 3, }),
            new Job(18, 510, new List<int>() { 2, 4, }),
            new Job(19, 16, new List<int>() { 1}),
            new Job(20, 647, new List<int>(){11}),
            new Job(21, 37, new List<int>() { 5, }),
            new Job(22, 490, new List<int>() { 5, 12 }),
            new Job(23, 48, new List<int>() { }),
            new Job(24, 256, new List<int>() { 2, 4 }),
            new Job(25, 327, new List<int>() { 2, 4 }),
            new Job(26, 125, new List<int>()),
            new Job(27, 69, new List<int>()),
            new Job(28, 42, new List<int>()),
            new Job(29, 371, new List<int>()),
            new Job(30, 231, new List<int>()),
            new Job(31, 154, new List<int>() { 1, 5, }),
            new Job(32, 17, new List<int>()),
            new Job(33, 164, new List<int>()),
            new Job(34, 782, new List<int>(){ 0, 3 }),
            new Job(35, 621, new List<int>()),
            new Job(36, 139, new List<int>(){ 0, 3 }),
            new Job(37, 575, new List<int>()),
            new Job(38, 117, new List<int>(){ 0, 3 }),
            new Job(39, 2185, new List<int>() {  14, 15, 16 }),
            new Job(40, 216, new List<int>()),
            new Job(41, 149, new List<int>()),
            new Job(42, 478, new List<int>()),
            new Job(43, 135, new List<int>()),
            new Job(44, 360, new List<int>()),
            new Job(45, 229, new List<int>()),
            new Job(46, 192, new List<int>(){ 0, 2, 3 }),
            new Job(47, 144, new List<int>()),
            new Job(48, 168, new List<int>()),
            new Job(49, 127, new List<int>()),
            new Job(50, 191, new List<int>(){  14, 15, 16 }),
            new Job(51, 143, new List<int>(){  15, 16 }),
            new Job(52, 166, new List<int>(){ 1, 5, 7, 9 }),
            new Job(53, 118, new List<int>()),
            new Job(54, 138, new List<int>()),
            new Job(55, 181, new List<int>() { 0, 2, 3 }),
            new Job(56, 113, new List<int>())
        };

            // liczba procesorów
            int numProcessors = 12;

            // Harmonogram, konstruktor (zadania, liczba dostępnych procesorów)
            Scheduler scheduler = new Scheduler(tasks, numProcessors);

            // Uloz harmonogram, za pomocą symulowanego wyżażania
            scheduler.ScheduleTasks();

            // wyswietl harmonogram
            scheduler.Wyswietl_harmonogram();
        }
    }
}
