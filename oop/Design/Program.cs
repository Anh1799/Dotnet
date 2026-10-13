class Program{
    public static void Main(string[] args){

        BadBankAccount badAccount = new();
        badAccount.balance = -5;
        System.Console.WriteLine(badAccount.balance);

        BankAccount bankAccount = new BankAccount(200);
        System.Console.WriteLine(bankAccount.GetBalance);

        bankAccount.Deposit(50);
        System.Console.WriteLine(bankAccount.GetBalance);

        bankAccount.Withdraw(100);
        System.Console.WriteLine(bankAccount.GetBalance);

    }
}