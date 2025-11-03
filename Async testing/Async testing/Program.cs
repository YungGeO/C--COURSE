using System;
using System.Threading.Tasks;

public class Program
{
    public  async Task DownloadDataAsync()
    {
        Console.WriteLine("Starting download...");
        await Task.Delay(3000);
        Console.WriteLine("Download complete.");
    }
    public static async Task Main(string[] args)
    {
       Program program = new Program();
       await program.DownloadDataAsync();
    }
}