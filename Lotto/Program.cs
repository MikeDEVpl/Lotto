using System;
using System.Collections.Generic;
using System.Linq;

namespace Lotto
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> liczby;
            List<int> porzadaneLiczby = new List<int>(){1, 2, 3, 4, 5, 6};
            int liczbaProb = 100000;
            List<int> liczbaProbWynik = new List<int>(liczbaProb);
            

            for (int i = 1;  i < liczbaProb; i++)
            {
                liczby = new HashSet<int>();
                int liczbaLosowan = Losowanie(liczby, porzadaneLiczby);
                Wyniki(liczby, liczbaLosowan);                
                liczbaProbWynik.Add(liczbaLosowan);
            }
            Console.WriteLine("Wynik po " + liczbaProb + " losowan");
            double srednia = liczbaProbWynik.Average();
            Console.WriteLine("Srednia: " + srednia);            

        }

        private static int Losowanie(HashSet<int> wylosowane, List<int> porzadane)
        {
            Random rand = new Random();
            int numer;
            int liczbaLosowan = 0;

            while (wylosowane.Count != 6)
            {
                numer = rand.Next(1, 49);

                if (wylosowane.Contains(numer))
                {
                    continue;
                }
                else if (porzadane.Contains(numer))
                {
                    wylosowane.Add(numer);
                }
                liczbaLosowan++;
            }
            return liczbaLosowan;
        }

        private static void Wyniki(HashSet<int> liczby,int liczbaLosowan)
        {
            Console.WriteLine("Lista: ");
            foreach (var item in liczby)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Losowanie numer: " + liczbaLosowan);
        }
    }
}
