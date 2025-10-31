using System;
using System.IO.Pipelines;

public class Program
{
    static double CalculateCircle(double r)
    {

        return Math.PI * r * r;
    }
    public static void Main()
    {
        Console.WriteLine("Enter the base length of the triangle:");
        double r = Convert.ToDouble(Console.ReadLine());
        double result = CalculateCircle(r);
        Console.WriteLine("The area of the circle is: " + result);
    }
}
