/*
LISKOV SUBSTITUTION PRINCIPLE (LSP)
- Subtypes must be substitutable for their base types.
- If S is a subtype of T, then objects of type T may be replaced with objects of type S without altering any of the desirable properties of that program (correctness, task performed, etc.).
- In other words, derived classes must be substitutable for their base classes.
- This principle is about ensuring that a subclass can stand in for its superclass without causing errors or unexpected behavior. It emphasizes that subclasses should extend the functionality of their base classes without changing their expected behavior
- LSP is important for maintaining the integrity of a class hierarchy and ensuring that code remains flexible and maintainable. Violating LSP can lead to code that is difficult to understand, test, and maintain, as it can introduce unexpected behavior when subclasses are used in place of their base classes.    



*/

public class Bird
{
    public string Name { get; set; }

    public virtual void Fly()
    {
        Console.WriteLine($"{Name} is flying");
    }

    public virtual void Eat()
    {
        Console.WriteLine($"{Name} is eatingg");
    }

    public virtual void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping");
    }
}

public interface IFly
{
    void Fly();
}

public class ChimSe : Bird, IFly
{
    public void Fly()
    {
        Console.WriteLine($"{Name} dang bay cham");
    }
}

public class CanhCuc : Bird
{
    
}