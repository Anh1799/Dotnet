class Program{
    public static void Main(string[] args){

        // BadBankAccount badAccount = new();
        // badAccount.balance = -5;
        // System.Console.WriteLine(badAccount.balance);

        BankAccount bankAccount = new BankAccount(200);
        Console.WriteLine(bankAccount.GetBalance());

        bankAccount.Deposit(50);
        Console.WriteLine(bankAccount.GetBalance());

        bankAccount.Withdraw(100);
        Console.WriteLine(bankAccount.GetBalance());

        EmailService emailService = new();
        // emailService.Connect();
        // emailService.Authenticate();
        emailService.SendEmail();
        // emailService.Disconnect();

        // Inheritance involves creating new classes (subclass or derived classes) base on existing classes (superclass or base classes). Subclasses inherit properties and behaviors from their superclasses and can also add new features or ovveride existing oneo. Inheritance is often described in terms of an "is-a" relationship.

        var car = new Bike();

        //Shared
        car.Brand = "Canyon";
        car.Start();
        car.Stop();

        // Unique
        car.NumberOfWheels = 4;
    }
}