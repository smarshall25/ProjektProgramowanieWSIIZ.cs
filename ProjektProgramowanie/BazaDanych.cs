using ProjektProgramowanie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class BazaDanych
{
    private const string sciezkaDoPlikuSamochodow = "C:\\Users\\sposs\\source\\repos\\ProjektProgramowanie\\ProjektProgramowanie\\samochody.txt";
    private const string sciezkaDoPlikuMotocykli = "C:\\Users\\sposs\\source\\repos\\ProjektProgramowanie\\ProjektProgramowanie\\motocykle.txt";
    private const string sciezkaDoPlikuElektrycznych = "C:\\Users\\sposs\\source\\repos\\ProjektProgramowanie\\ProjektProgramowanie\\elektryczne.txt";

    public List<Pojazd> WczytajDane()
    {
        List<Pojazd> pojazdy = new List<Pojazd>();

        pojazdy.AddRange(WczytajPojazdyZPliku(sciezkaDoPlikuSamochodow, TypPojazdu.Samochod));
        pojazdy.AddRange(WczytajPojazdyZPliku(sciezkaDoPlikuMotocykli, TypPojazdu.Motocykl));
        pojazdy.AddRange(WczytajPojazdyZPliku(sciezkaDoPlikuElektrycznych, TypPojazdu.PojazdElektryczny));

        return pojazdy;
    }

    public void ZapiszPojazdy(List<Pojazd> pojazdy)
    {
        List<Pojazd> samochody = pojazdy.Where(p => p is Samochod).ToList();
        List<Pojazd> motocykle = pojazdy.Where(p => p is Motocykl).ToList();
        List<Pojazd> pojazdyElektryczne = pojazdy.Where(p => p is PojazdElektryczny).ToList();

        ZapiszPojazdyDoPliku(samochody, sciezkaDoPlikuSamochodow);
        ZapiszPojazdyDoPliku(motocykle, sciezkaDoPlikuMotocykli);
        ZapiszPojazdyDoPliku(pojazdyElektryczne, sciezkaDoPlikuElektrycznych);
    }

    private List<Pojazd> WczytajPojazdyZPliku(string sciezkaDoPliku, TypPojazdu typPojazdu)
    {
        List<Pojazd> pojazdy = new List<Pojazd>();

        if (File.Exists(sciezkaDoPliku))
        {
            string[] linie = File.ReadAllLines(sciezkaDoPliku);
            foreach (string linia in linie)
            {
                string[] dane = linia.Split(',');

                if (dane.Length >= 3)
                {
                    int id;
                    if (int.TryParse(dane[0], out id))
                    {
                        string marka = dane[1];
                        string model = dane[2];

                        Pojazd pojazd = null;

                        if (typPojazdu == TypPojazdu.Samochod && dane.Length == 5)
                        {
                            int rokProdukcji, liczbaDrzwi;
                            if (int.TryParse(dane[3], out rokProdukcji) && int.TryParse(dane[4], out liczbaDrzwi))
                            {
                                pojazd = new Samochod
                                {
                                    ID = id,
                                    Marka = marka,
                                    Model = model,
                                    RokProdukcji = rokProdukcji,
                                    LiczbaDrzwi = liczbaDrzwi
                                };
                            }
                        }
                        else if (typPojazdu == TypPojazdu.Motocykl && dane.Length == 4)
                        {
                            int rokProdukcji;
                            if (int.TryParse(dane[3], out rokProdukcji))
                            {
                                pojazd = new Motocykl
                                {
                                    ID = id,
                                    Marka = marka,
                                    Model = model,
                                    RokProdukcji = rokProdukcji
                                };
                            }
                        }
                        else if (typPojazdu == TypPojazdu.PojazdElektryczny && dane.Length == 5)
                        {
                            int rokProdukcji, pojemnoscBaterii;
                            if (int.TryParse(dane[3], out rokProdukcji) && int.TryParse(dane[4], out pojemnoscBaterii))
                            {
                                pojazd = new PojazdElektryczny
                                {
                                    ID = id,
                                    Marka = marka,
                                    Model = model,
                                    RokProdukcji = rokProdukcji,
                                    PojemnoscBaterii = pojemnoscBaterii
                                };
                            }
                        }

                        if (pojazd != null)
                        {
                            pojazdy.Add(pojazd);
                        }
                    }
                }
            }
        }

        return pojazdy;
    }

    private void ZapiszPojazdyDoPliku(List<Pojazd> pojazdy, string sciezkaDoPliku)
    {
        List<string> liniePojazdow = new List<string>();

        foreach (Pojazd pojazd in pojazdy)
        {
            string linia = $"{pojazd.ID},{pojazd.Marka},{pojazd.Model}";

            if (pojazd is Samochod samochod)
            {
                linia += $",{samochod.RokProdukcji},{samochod.LiczbaDrzwi}";
            }
            else if (pojazd is Motocykl motocykl)
            {
                linia += $",{motocykl.RokProdukcji},{motocykl.Typ}";
            }
            else if (pojazd is PojazdElektryczny pojazdElektryczny)
            {
                linia += $",{pojazdElektryczny.RokProdukcji},{pojazdElektryczny.PojemnoscBaterii}";
            }

            liniePojazdow.Add(linia);
        }

        File.WriteAllLines(sciezkaDoPliku, liniePojazdow);
    }

    public void DodajSamochod(List<Pojazd> pojazdy)
    {
        Console.WriteLine("Dodawanie nowego samochodu:");
        Samochod samochod = new Samochod();

        Console.Write("Podaj markę: ");
        samochod.Marka = Console.ReadLine();    

        Console.Write("Podaj model: ");
        samochod.Model = Console.ReadLine();

        Console.Write("Podaj rok produkcji: ");
        if (int.TryParse(Console.ReadLine(), out int rokProdukcji))
        {
            samochod.RokProdukcji = rokProdukcji;
        }
        else
        {
            Console.WriteLine("Nieprawidłowy rok produkcji. Samochód nie został dodany.");
            return;
        }

        Console.Write("Podaj liczbę drzwi: ");
        if (int.TryParse(Console.ReadLine(), out int liczbaDrzwi))
        {
            samochod.LiczbaDrzwi = liczbaDrzwi;
        }
        else
        {
            Console.WriteLine("Nieprawidłowa liczba drzwi. Samochód nie został dodany.");
            return;
        }

        samochod.ID = GenerujNoweID(pojazdy);
        pojazdy.Add(samochod);
        Console.WriteLine("Samochód został dodany.");
    }

    public void DodajMotocykl(List<Pojazd> pojazdy)
    {
        Console.WriteLine("Dodawanie nowego motocykla:");
        Motocykl motocykl = new Motocykl();

        Console.Write("Podaj markę: ");
        motocykl.Marka = Console.ReadLine();

        Console.Write("Podaj model: ");
        motocykl.Model = Console.ReadLine();

        Console.Write("Podaj rok produkcji: ");
        if (int.TryParse(Console.ReadLine(), out int rokProdukcji))
        {
            motocykl.RokProdukcji = rokProdukcji;
        }
        else
        {
            Console.WriteLine("Nieprawidłowy rok produkcji. Motocykl nie został dodany.");
            return;
        }

        Console.Write("Podaj typ motocykla: ");
        motocykl.Typ = Console.ReadLine();

        motocykl.ID = GenerujNoweID(pojazdy);
        pojazdy.Add(motocykl);
        Console.WriteLine("Motocykl został dodany.");
    }

    public void DodajPojazdElektryczny(List<Pojazd> pojazdy)
    {
        Console.WriteLine("Dodawanie nowego pojazdu elektrycznego:");
        PojazdElektryczny pojazdElektryczny = new PojazdElektryczny();

        Console.Write("Podaj markę: ");
        pojazdElektryczny.Marka = Console.ReadLine();

        Console.Write("Podaj model: ");
        pojazdElektryczny.Model = Console.ReadLine();

        Console.Write("Podaj rok produkcji: ");
        if (int.TryParse(Console.ReadLine(), out int rokProdukcji))
        {
            pojazdElektryczny.RokProdukcji = rokProdukcji;
        }
        else
        {
            Console.WriteLine("Nieprawidłowy rok produkcji. Pojazd elektryczny nie został dodany.");
            return;
        }

        Console.Write("Podaj pojemność baterii: ");
        if (int.TryParse(Console.ReadLine(), out int pojemnoscBaterii))
        {
            pojazdElektryczny.PojemnoscBaterii = pojemnoscBaterii;
        }
        else
        {
            Console.WriteLine("Nieprawidłowa pojemność baterii. Pojazd elektryczny nie został dodany.");
            return;
        }

        pojazdElektryczny.ID = GenerujNoweID(pojazdy);
        pojazdy.Add(pojazdElektryczny);
        Console.WriteLine("Pojazd elektryczny został dodany.");
    }

    public void WyswietlPojazdy(List<Pojazd> pojazdy)
    {
        foreach (Pojazd pojazd in pojazdy)
        {
            Console.WriteLine($"{pojazd.ID}. {pojazd.Marka} {pojazd.Model}");

            if (pojazd is Samochod samochod)
            {
                Console.WriteLine($"   - Rok produkcji: {samochod.RokProdukcji}");
                Console.WriteLine($"   - Liczba drzwi: {samochod.LiczbaDrzwi}");
            }
            else if (pojazd is Motocykl motocykl)
            {
                
                Console.WriteLine($"   - Rok produkcji: {motocykl.RokProdukcji}");
                Console.WriteLine($"   - Typ motocykla: {motocykl.Typ}");
            }
            else if (pojazd is PojazdElektryczny pojazdElektryczny)
            {
                
                Console.WriteLine($"   - Rok produkcji: {pojazdElektryczny.RokProdukcji}");
                Console.WriteLine($"   - Pojemność baterii: {pojazdElektryczny.PojemnoscBaterii}");
            }
        }
    }

    private int GenerujNoweID(List<Pojazd> pojazdy)
    {
        int maxID = 0;
        foreach (Pojazd pojazd in pojazdy)
        {
            if (pojazd.ID > maxID)
            {
                maxID = pojazd.ID;
            }
        }
        return maxID + 1;
    }
}
