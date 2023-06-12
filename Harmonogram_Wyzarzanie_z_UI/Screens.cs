using System;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Harmonogram_Wyzarzanie
{
    public class Screens
    {
        static bool czy_sa_dane = false;
        public static void Create_new_data_screen()
        {
            Console.Clear();
            if(czy_sa_dane == true) 
            {
                while (true)
                {
                    Console.WriteLine("Istnieja już jakies dane w programie. Aby stworzyc nowe trzeba je usunac.\n" +
                        "Czy chcesz kontynuowac?");
                    Console.WriteLine("Wpisz 1 jezeli tak, 2 jezeli nie");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Data_management.Create_data_screen();
                            return;
                        case "2":
                            return;
                        default:
                            Console.WriteLine("Nieprawidlowy wybor. Sprobuj ponownie.");
                            break;
                    }
                }
            }
            Data_management.Create_data_screen();
        }

        public static void Display_current_data_screen()
        {
            Console.Clear();
            if (Data_management.jobs.Count == 0)
            {
                Console.WriteLine("Brak danych.");
            }
            else Data_management.Display_current_data();
            Console.WriteLine("Nacisnij klawisz, aby kontynuowac");
            Console.ReadLine();
        }

        public static void Load_data_screen()
        {
            
            string filePath;

            Console.Clear();

            Console.WriteLine("Dostepne pliki");
            string folderPath = @".\\pliki\\";

            try
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }

                if (files.Length == 0) 
                {
                    Console.WriteLine("Nie ma zadnych plikow!");
                    Console.WriteLine("Nacisnij klawisz, aby kontynuowac");
                    Console.ReadLine();
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
            }
            while (true)
            {
                Console.WriteLine("Podaj nazwe pliku z rozszerzeniem");
                filePath = Console.ReadLine();

                if (File.Exists(folderPath + filePath))
                {
                    Console.Clear();
                    Data_management.Load_data_from_file(folderPath + filePath);
                    Console.WriteLine("Wczytano plik");
                    Console.WriteLine("Nacisnij klawisz, aby kontynuowac");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje");
                    Console.WriteLine("Nacisnij klawisz, aby kontynuowac");
                    Console.ReadLine();
                    Load_data_screen();
                }
            }

            
        }

        public static void Create_schedule_screen()
        {
            Console.Clear();
            if (Data_management.jobs.Count == 0)
            {
                
                Console.WriteLine("Brak danych.");
            }
            else
            {
                double temperature, cooling_rate;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Podaj temperature i tempo schladzania np. 1000 i 0.95");

                    string input = Console.ReadLine();

                    string[] split = input.Split(' ');

                    if(split.Count() != 2 )
                    {
                        Console.WriteLine("Nieprawidlowa ilosc argumentow");
                        Console.ReadLine();
                        continue;
                    }

                    bool var1 = double.TryParse(split[0], out temperature);
                    bool var2 = double.TryParse(split[1].Replace('.', ','), out cooling_rate);

                    if(var1 == var2 == true)
                    {
                        Data_management.Create_schedule(temperature, cooling_rate);
                    }
                    else
                    {
                        Console.WriteLine("Bledne argumenty");
                        Console.WriteLine("Nacisnij klawisz, aby kontynuowac");
                        Console.ReadLine();
                        continue;
                    }
                    break;
                }
            }
            Console.WriteLine("Nacisnij klawisz, aby kontynuowac");
            Console.ReadLine();


            

        }






    }
}
