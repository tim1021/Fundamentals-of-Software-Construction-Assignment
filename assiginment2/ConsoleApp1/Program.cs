// 输出质因数
using System;
using System.Security.Cryptography.X509Certificates;
namespace ConsoleApp1
{
    class Program
    {
        static bool IsPrime(int num)
        {
            if (num <= 1)
                return false;
            if (num == 2)
                return true;
            if (num % 2 == 0)
                return false;
            for (int i=3;i * i <= num ; i += 2)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }
        static void FindPrimeFactors(int num)
        {
            Console.WriteLine($"prime factors of {num} are:");

            for (int i = 2;i <= num ;i++)
            {
                while (num % i == 0 && IsPrime(i))
                {
                    Console.WriteLine(i);
                    num /= i;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("please enter a number for print prime factor:");
            int number = int.Parse(Console.ReadLine());
            FindPrimeFactors(number);
        }
    }
}