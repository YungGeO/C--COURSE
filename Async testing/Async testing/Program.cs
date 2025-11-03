using System;
using System.Threading.Tasks;

public class Program
{
    public async Task DownloadDataAsync()
    {
        try
        {
            Console.WriteLine("Starting download1...");
            throw new InvalidOperationException("Simulated download error.");
            await Task.Delay(3000);
            Console.WriteLine("Download1 complete.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public async Task DownloadDataAsync2()
    {
        try
        {
            Console.WriteLine("Starting download2...");

            await Task.Delay(3000);
            Console.WriteLine("Download2 complete.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static async Task Main(string[] args)
    {
        Program program = new Program();
        //Task task1 = program.DownloadDataAsync();
        //Task task2 = program.DownloadDataAsync2();
        await Task.WhenAll(program.DownloadDataAsync(), program.DownloadDataAsync2());
        Console.WriteLine("All downloads completed.");
    }
}