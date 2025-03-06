//随机创建10个形状对象，计算这些对象的面积之和。
//尝试使用简单工厂设计模式来创建对象。
using System;
using System.Collections;
public class ShapeFactory
{
    public static Shape CreateShape(string shapeType)
    {
        Random rand = new Random();
        switch (shapeType)
        {
            case "rectangle":
                double width = rand.Next(1, 10); 
                double height = rand.Next(1, 10); 
                return new Rectangle(width, height);

            case "square":
                double side = rand.Next(1, 10); 
                return new Square(side);

            case "triangle":
                double sideA = rand.Next(1, 10);  
                double sideB = rand.Next(1, 10); 
                double sideC = rand.Next(1, 10);  
                return new Triangle(sideA, sideB, sideC);

            default:
                throw new ArgumentException("Invalid shape type.");
        }
    }
}

public interface Shape
{
    bool IsValid();
    double CalculateArea();
}
public class Rectangle : Shape
{
    public double width { get; set; }
    public double height { get; set; }

    public Rectangle(double width, double height)
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
class Triangle : Shape
{
    public double side1 { get; set; }
    public double side2 { get; set; }
    public double side3 { get; set; }
    public Triangle(double side1, double side2, double side3)
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
        Random rand = new Random();
        double totalArea = 0;

        for (int i = 0; i < 10; i++)
        {
            string[] shapeTypes = { "rectangle", "square", "triangle" };
            string randomShapeType = shapeTypes[rand.Next(0, shapeTypes.Length)];

            Shape shape = ShapeFactory.CreateShape(randomShapeType);

            if (shape.IsValid())
            {
                totalArea += shape.CalculateArea();
            }
        }

        Console.WriteLine($"Total Area of 10 Shapes: {Math.Round(totalArea,2)}");
    }
}

