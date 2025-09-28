using System;

abstract class Shape
{
    public abstract bool Contains(Point p);
}

class Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double X, double Y)
    {
        this.X = X;
        this.Y = Y;
    }
}

class Rectangle : Shape
{
    public Point TopLeft { get; set; }
    public Point BottomRight { get; set; }

    public Rectangle(Point topLeft, Point bottomRight)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
    }

    public override bool Contains(Point p)
    {
        return p.X >= TopLeft.X && p.X <= BottomRight.X && p.Y >= TopLeft.Y && p.Y <= BottomRight.Y;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter points: TopLeftX TopLeftY BottomRightX BottomRightY");
        string[] rectanglePoints = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        double topLeftX = double.Parse(rectanglePoints[0]);
        double topLeftY = double.Parse(rectanglePoints[1]);
        double bottomRightX = double.Parse(rectanglePoints[2]);
        double bottomRightY = double.Parse(rectanglePoints[3]);

        Rectangle rectangle = new Rectangle(new Point(topLeftX, topLeftY), new Point(bottomRightX, bottomRightY));

        Console.WriteLine("Enter number of points:");
        int n = int.Parse(Console.ReadLine()!);

        List<bool> results = new List<bool>();

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter point {i + 1}:");
            string[] points = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            double pX = double.Parse(points[0]);
            double pY = double.Parse(points[1]);

            Point p = new Point(pX, pY);

            results.Add(rectangle.Contains(p));
        }

        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine(results[i]);
        }
    }
}