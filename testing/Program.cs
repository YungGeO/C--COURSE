using System.Security.Cryptography.X509Certificates;

public class Program
{

    public static string[] tasks = new string[10];
    public static int taskCount = 0;

    public static void AddTask()
    {
        Console.Write("Enter the task:");
        tasks[taskCount] = Console.ReadLine();
        taskCount++;
    }
    public static void ViewTasks()
    {
        Console.WriteLine("Tasks:");
        if (taskCount == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }
        for (int i = 0; i < taskCount; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i]}");
        }
    }
    public static void CompleteTask()
    {
        Console.WriteLine("Enter the task number to mark complete:");
        if (int.TryParse(Console.ReadLine(), out int taskNumber))
        {
            if (taskNumber > 0 && taskNumber <= taskCount)
            {
                tasks[taskNumber - 1] += " [Complete]";
                Console.WriteLine("Task marked as complete.");
            }
            else
            {
                Console.WriteLine("Invalid task number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid task number.");
        }


    }
    public static void Main(string[] args)
    {
        bool stopInput = true;
        while (stopInput)
        {
            Console.WriteLine("1. View Tasks");
            Console.WriteLine("2. Add Task");
            Console.WriteLine("3. Mark Task Complete");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    ViewTasks();
                    break;
                case 2:
                    AddTask();
                    break;
                case 3:
                    CompleteTask();
                    break;
                case 4:
                    stopInput = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }


}