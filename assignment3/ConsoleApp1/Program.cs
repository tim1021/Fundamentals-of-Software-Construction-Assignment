//编写面向对象程序实现长方形、正方形、三角形等形状的类。
//每个形状类都能计算面积、判断形状是否合法。
//请尝试合理使用接口/抽象类、属性来实现。

using System;
public interface Shape
{
    bool IsValid();
    double CalculateArea();
}
public class Rectangle:Shape
{
    public double width { get; set; }
    public double height { get; set; }
    
    public Rectangle(double width,double height)
    {
        this.width = width;
        this.height = height;
    }
    public bool IsValid()
    {
        return width > 0 && height > 0;
    }
    public double CalculateArea()
    {
        if (IsValid())
            return width * height;
        throw new InvalidOperationException("Invalid rectangle.");
    }
}
class Square : Shape
{
    public double side { get; set; }
    public Square(double side)
    {
        this.side = side;
    }
    public bool IsValid()
    {
        return side > 0;
    }
    public double CalculateArea()
    {
        if (IsValid())
            return side * side;
        else
            throw new InvalidOperationException("Invalid square.");

    }
}
class Tritangle : Shape
{
    public double side1 { get; set; }
    public double side2 { get; set; }
    public double side3 { get; set; }
    public Tritangle(double side1, double side2, double side3)
    {
        this.side1 = side1;
        this.side2 = side2;
        this.side3 = side3;
    }
    public bool IsValid()
    {
        return (side1 > 0) &&
               (side2 > 0) &&
               (side3 > 0) &&
               (side1 + side2 > side3) &&
               (side1 + side3 > side2) &&
               (side2 + side3 > side1);
    }
    public double CalculateArea()
    {
        if (IsValid())
        {
            double s = (side1 + side2 + side3) / 2;
            return Math.Sqrt(s * (s - side1) * (s - side2) * (s - side3));
        }
        else
            throw new InvalidOperationException("Invalid triangle.");
    }

}
class Program
{
    static void Main()
    {
        //测试长方形类
        Shape r1 = new Rectangle(2, 5);
        Console.WriteLine(r1.IsValid());
        Console.WriteLine(r1.CalculateArea());

        //测试正方形类
        Shape s1 = new Square(2);
        Console.WriteLine(s1.IsValid());
        Console.WriteLine(s1.CalculateArea());
        //测试三角形类
        Shape t1 = new Tritangle(3, 4, 5);
        Console.WriteLine(t1.IsValid());
        Console.WriteLine(t1.CalculateArea());
    }
}