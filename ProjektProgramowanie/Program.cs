using ProjektProgramowanie;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        BazaDanych bazaDanych = new BazaDanych();
        List<Pojazd> pojazdy = bazaDanych.WczytajDane();

        // Pseudointerfejs konsolowy
        while (true)
        {
            Console.WriteLine("Wybierz operację:");
            Console.WriteLine("1. Dodaj samochod");
            Console.WriteLine("2. Dodaj motocykl");
            Console.WriteLine("3. Dodaj pojazd elektryczny");
            Console.WriteLine("4. Wyświetl pojazdy");
            Console.WriteLine("5. Usun pojazd");
            Console.WriteLine("6. Aktualizuj pojazd");
            Console.WriteLine("7. Zapisz dane");
            Console.WriteLine("8. Wyjscie");

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
                        Console.WriteLine("Podaj ID pojazdu do usunięcia:");
                        int idDoUsuniecia = int.Parse(Console.ReadLine());
                        bazaDanych.UsunPojazd(idDoUsuniecia, pojazdy); 
                        break;
                    case 6: 
                        Console.WriteLine("Podaj ID pojazdu do zaktualizowania:");
                        int idDoAktualizacji = int.Parse(Console.ReadLine());

                       
                        Pojazd pojazdDoAktualizacji = pojazdy.FirstOrDefault(p => p.ID == idDoAktualizacji);
                        if (pojazdDoAktualizacji != null)
                        {
                            
                            Console.WriteLine("Podaj nową markę:");
                            string nowaMarka = Console.ReadLine();
                            Console.WriteLine("Podaj nowy model:");
                            string nowyModel = Console.ReadLine();

                          
                            pojazdDoAktualizacji.Marka = nowaMarka;
                            pojazdDoAktualizacji.Model = nowyModel;
                            bazaDanych.AktualizujPojazd(pojazdDoAktualizacji, pojazdy);
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono pojazdu o podanym ID.");
                        }
                        break;
                    case 7:
                        bazaDanych.ZapiszPojazdy(pojazdy);
                        Console.WriteLine("Dane zostały zapisane.");
                        break;
                    case 8:
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
