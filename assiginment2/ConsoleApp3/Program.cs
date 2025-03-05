using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int limit = 100;
        List<int> primes = SieveOfEratosthenes(limit);

        Console.WriteLine("2~" +100+ " 以内的素数为：");
        foreach (int prime in primes)
        {
            Console.Write(prime + " ");
        }
        Console.WriteLine();
    }

    static List<int> SieveOfEratosthenes(int limit)
    {
        bool[] isPrime = new bool[limit + 1];
        for (int i = 2; i <= limit; i++)
        {
            isPrime[i] = true;
        }

        for (int p = 2; p * p <= limit; p++)
        {
            if (isPrime[p])
            {
                for (int i = p * p; i <= limit; i += p)
                {
                    isPrime[i] = false;
                }
            }
        }

        List<int> primes = new List<int>();
        for (int i = 2; i <= limit; i++)
        {
            if (isPrime[i])
            {
                primes.Add(i);
            }
        }

        return primes;
    }
}
