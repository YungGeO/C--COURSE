using System.Dynamic;
public class Program
{
            public static void Main(string[] args)
        {
            Person friend = new Person();
            Person colleague = new Person();
            friend.Name="Alice";
            friend.Age = 30;
            colleague.Name = "Jane Smith";
            colleague.Age = 25;
            
            friend.Greet();
            colleague.Greet();
        }
    }

public class Person
{
    public String Name { get; set; }
    public int Age { get; set; }
    
    public void Greet()
    {
        Console.WriteLine($"Hello,  {Name} aged {Age} years old.");
    }
}