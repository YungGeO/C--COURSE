public interface IAnimal
{
    void Eat();
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Animal> animals = new List<Animal>();
        animals.Add(new Dog());
        animals.Add(new Cat());

        foreach (Animal animal in animals)
        {
            animal.MakeSound();
            animal.Eat();
        }
    }
}
public class Animal : IAnimal
{
    public virtual void Eat()
    {
        Console.WriteLine("The animal is eating.");
    }
    public virtual void MakeSound()
    {
        Console.WriteLine("The animal makes a sound.");
    }
}
public class Dog : Animal
{
    public override void Eat()
    {
        Console.WriteLine("Kibble");
    }
    public override void MakeSound()
    {
        Console.WriteLine("The dog barks.");
    }
}
public class Cat : Animal
{

    public override void MakeSound()
    {
        Console.WriteLine("The cat meows.");
    }
}


