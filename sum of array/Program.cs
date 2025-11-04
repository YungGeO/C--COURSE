using System.Linq.Expressions;
using System.Reflection;
using System;
public class Program
{
    public static int SumOfArray(int[] numbers)
    {
        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }
        return sum;
    }
    public static void Main(string[] args)
    {
        int[] numbers = new int[10];
        Console.WriteLine("dwse 10 arithmous");
        for (int i = 0; i < 10; i++)
        {
            int input;
            bool validInput = false;
            while (!validInput)
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out input))
                {
                    numbers[i] = input;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("invalid integer");
                }
            }


        }
        int result = SumOfArray(numbers);
        Console.WriteLine($"the result is {result}");
    }
}
