namespace ThreadLockDemo;

public static class Bank
{
    /// <summary>
    /// DEADLOCK-PRONE transfer: locks "from" first, then "to".
    /// Thread 1: A -> B locks A, waits for B
    /// Thread 2: B -> A locks B, waits for A   => both wait forever.
    /// Monitor.TryEnter with a timeout lets us detect it instead of hanging.
    /// </summary>
    public static bool TransferNaive(BankAccount from, BankAccount to, decimal amount, int timeoutMs = 1000)
    {
        lock (from.SyncRoot)
        {
            Thread.Sleep(50); // give the other thread time to grab its first lock

            if (!Monitor.TryEnter(to.SyncRoot, timeoutMs))
                return false; // could not get the 2nd lock -> deadlock detected

            try
            {
                if (from.BalanceUnlocked < amount) return false;
                from.AddUnlocked(-amount);
                to.AddUnlocked(amount);
                return true;
            }
            finally
            {
                Monitor.Exit(to.SyncRoot);
            }
        }
    }

    /// <summary>
    /// SAFE transfer: always lock the account with the smaller Id first.
    /// Every thread takes locks in the same order, so a deadlock cannot happen.
    /// </summary>
    public static bool Transfer(BankAccount from, BankAccount to, decimal amount)
    {
        if (from == to) return false;

        var first = from.Id < to.Id ? from : to;
        var second = from.Id < to.Id ? to : from;

        lock (first.SyncRoot)
        {
            Thread.Sleep(50);
            lock (second.SyncRoot)
            {
                if (from.BalanceUnlocked < amount) return false;
                from.AddUnlocked(-amount);
                to.AddUnlocked(amount);
                return true;
            }
        }
    }
}
