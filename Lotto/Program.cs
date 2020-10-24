using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;

namespace Lotto
{
    class Program
    {
        static void Main(string[] args)
        {
            CryptoRandom random = new CryptoRandom();
            HashSet<int> liczby;
            List<int> porzadaneLiczby = new List<int>() { 1, 2, 3, 4, 5, 6 };
            int liczbaProb = 1000;
            List<int> liczbaProbWynik = new List<int>(liczbaProb);


            for (int i = 1; i < liczbaProb; i++)
            {
                liczby = new HashSet<int>();
                int liczbaLosowan = Losowanie(liczby, porzadaneLiczby, random);
                Wyniki(liczby, liczbaLosowan);
                liczbaProbWynik.Add(liczbaLosowan);
            }
            Console.WriteLine("Wynik po " + liczbaProb + " losowan");
            double srednia = liczbaProbWynik.Average();
            Console.WriteLine("Srednia: " + srednia);

        }

        private static int Losowanie(HashSet<int> wylosowane, List<int> porzadane, CryptoRandom random)
        {
            CryptoRandom rand = random;
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

        private static void Wyniki(HashSet<int> liczby, int liczbaLosowan)
        {
            Console.WriteLine("Lista: ");
            foreach (var item in liczby)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Losowanie numer: " + liczbaLosowan);
        }
    }
    public class CryptoRandom : Random
    {
        private RNGCryptoServiceProvider _rng =
            new RNGCryptoServiceProvider();
        private byte[] _uint32Buffer = new byte[4];

        public CryptoRandom() { }
        public CryptoRandom(Int32 ignoredSeed) { }

        public override Int32 Next()
        {
            _rng.GetBytes(_uint32Buffer);
            return BitConverter.ToInt32(_uint32Buffer, 0) & 0x7FFFFFFF;
        }

        public override Int32 Next(Int32 maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue");
            return Next(0, maxValue);
        }

        public override Int32 Next(Int32 minValue, Int32 maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            while (true)
            {
                _rng.GetBytes(_uint32Buffer);
                UInt32 rand = BitConverter.ToUInt32(_uint32Buffer, 0);

                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }
    }
}
