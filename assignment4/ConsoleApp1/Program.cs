using System;

public class Node<T>
{
    public Node<T> Next { get; set; }
    public T Data { get; set; }
    public Node(T t)
    {
        Next = null;
        Data = t;
    }
}

public class GenericList<T>
{
    private Node<T> head;
    private Node<T> tail;
    public GenericList()
    {
        tail = head = null;
    }
    public Node<T> Head
    {
        get => head;
    }
    public void Add(T t)
    {
        Node<T> n = new Node<T>(t);
        if (tail == null)
        {
            head = tail = n;
        }
        else
        {
            tail.Next = n;
            tail = n;
        }
    }
    public void ForEach(Action<T> action)
    {
        for (Node<T> node = head; node != null; node = node.Next)
        {
            action(node.Data);
        }
    }
    public T GetMax()
    {
        if (head == null)
            throw new InvalidOperationException("List is empty.");

        T max = head.Data;
        ForEach(item =>
        {
            if (Comparer<T>.Default.Compare(item, max) > 0)
            {
                max = item;
            }
        });
        return max;
    }
    public T GetMin()
    {
        if (head == null)
            throw new InvalidOperationException("List is empty.");

        T min = head.Data;
        ForEach(item =>
        {
            if (Comparer<T>.Default.Compare(item, min) < 0)
            {
                min = item;
            }
        });
        return min;
    }
    public T GetSum()
    {
        dynamic sum = 0;
        ForEach(item =>
        {
            sum += item;
        });
        return sum;
    }
}

class Program
{
    static void Main()
    {
        // 整型list
        GenericList<int> intlist = new GenericList<int>();
        for (int x = 0; x < 10; x++)
        {
            intlist.Add(x);
        }

        // 打印元素
        Console.WriteLine("intlist elements:");
        intlist.ForEach(item => Console.WriteLine(item));

        // 求最大值、最小值、求和
        Console.WriteLine($"Max: {intlist.GetMax()}");
        Console.WriteLine($"Min: {intlist.GetMin()}");
        Console.WriteLine($"Sum: {intlist.GetSum()}");

        // 字符串型list
        GenericList<string> stringlist = new GenericList<string>();
        for (int x = 0; x < 10; x++)
        {
            stringlist.Add("str" + x);
        }

        // 打印字符串元素
        Console.WriteLine("stringlist elements:");
        stringlist.ForEach(item => Console.WriteLine(item));
    }
}