using System;
using System.Threading.Tasks;

public class Program
{
    public async Task DownloadDataAsync()
    {
        try
        {
            Console.WriteLine("Starting download1...");
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
        await Task.WhenAll(program.DownloadDataAsync(), program.DownloadDataAsync2());
    }
}