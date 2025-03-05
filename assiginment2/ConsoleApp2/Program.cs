using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            try
            {
                int[] numbers = GetInput();
                var (max, min, sum, average) = CalStatistics(numbers);
                DisplayResults(max, min, sum, average);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"错误: {ex.Message}");
            }
        }
    }

    public static int[] GetInput()
    {
        Console.WriteLine("请输入整数数组（元素间用空格分隔）：");
        string input = Console.ReadLine();
        string[] parts = input.Split();
        int[] numbers = new int[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            numbers[i] = int.Parse(parts[i]);
        }
        return numbers;
    }

    public static (int max, int min, int sum, double average) CalStatistics(int[] numbers)
    {
        int max = numbers[0], min = numbers[0], sum = 0;
        foreach (var num in numbers)
        {
            sum += num;
            max = Math.Max(max, num);
            min = Math.Min(min, num);
        }
        double average = (double)sum / numbers.Length;
        return (max, min, sum, average);
    }

    public static void DisplayResults(int max, int min, int sum, double average)
    {
        Console.WriteLine($"最大值：{max}");
        Console.WriteLine($"最小值：{min}");
        Console.WriteLine($"总和：{sum}");
        Console.WriteLine($"平均值：{average:F2}");
    }
}
