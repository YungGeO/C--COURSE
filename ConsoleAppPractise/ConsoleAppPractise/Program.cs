using System;

public class Program
{

    public static void Main()
    {

        do
        {
            Console.WriteLine("give me a number");
            int number = int.Parse(Console.ReadLine());
            if (number >= 0 && number <= 10 && number % 2 == 0)
            {
                Console.WriteLine("the number " + number + " is even");
                break;
            }
            else
            {
                Console.WriteLine("invalid number add a new one ");
            }
        } while (true);
    }
}
