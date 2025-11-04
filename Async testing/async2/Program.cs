using System.Reflection;

public class Program
{
    public static async Task TestTask()
    {
        try
        {
            Console.WriteLine("Task started...");
            await Task.Delay(3000);
            throw new InvalidOperationException("takis tsan");
            Console.WriteLine("Task Completed...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error at {ex.Message}");
        }


    }
    static void Main(string[] args)
    {
        Task.Run(async () => await TestTask()).Wait();
        Console.WriteLine("task completed in the main");
    }
}
