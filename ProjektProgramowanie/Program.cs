using ProjektProgramowanie;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        BazaDanych bazaDanych = new BazaDanych();
        List<Pojazd> pojazdy = bazaDanych.WczytajDane();

        while (true)
        {
            Console.WriteLine("Wybierz operację:");
            Console.WriteLine("1. Dodaj samochód");
            Console.WriteLine("2. Dodaj motocykl");
            Console.WriteLine("3. Dodaj pojazd elektryczny");
            Console.WriteLine("4. Wyświetl pojazdy");
            Console.WriteLine("5. Zapisz dane");
            Console.WriteLine("6. Wyjście");

            int wybor;
            if (int.TryParse(Console.ReadLine(), out wybor))
            {
                switch (wybor)
                {
                    case 1:
                        bazaDanych.DodajSamochod(pojazdy);
                        break;
                    case 2:
                        bazaDanych.DodajMotocykl(pojazdy);
                        break;
                    case 3:
                        bazaDanych.DodajPojazdElektryczny(pojazdy);
                        break;
                    case 4:
                        Console.WriteLine("Pojazdy w bazie danych:");
                        bazaDanych.WyswietlPojazdy(pojazdy);
                        break;
                    case 5:
                        bazaDanych.ZapiszPojazdy(pojazdy);
                        Console.WriteLine("Dane zostały zapisane.");
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Wybierz ponownie.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór. Wybierz ponownie.");
            }
        }
    }
}
