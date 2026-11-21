namespace ThreadLockDemo;

/// <summary>
/// UNSAFE account: no lock. Many threads changing Balance at the same time
/// will lose updates (race condition).
/// </summary>
public class UnsafeAccount
{
    public decimal Balance { get; private set; }

    public void Deposit(decimal amount)
    {
        // "Balance += amount" is really 3 steps: read -> add -> write.
        // Two threads can read the same old value and one write is lost.
        var current = Balance;
        Thread.SpinWait(50);          // widen the gap so the bug shows up reliably
        Balance = current + amount;
    }
}

/// <summary>
/// SAFE account: every read/write of Balance happens inside lock(_sync),
/// so only one thread at a time can touch it.
/// </summary>
public class BankAccount
{
    private readonly object _sync = new();   // private lock object (never lock(this))
    private decimal _balance;

    public int Id { get; }
    public string Owner { get; }

    // Exposed so Bank.Transfer can lock two accounts in a fixed order.
    internal object SyncRoot => _sync;

    public BankAccount(int id, string owner, decimal initial = 0)
    {
        Id = id;
        Owner = owner;
        _balance = initial;
    }

    public decimal Balance
    {
        get { lock (_sync) return _balance; }
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be positive");
        lock (_sync)
        {
            var current = _balance;
            Thread.SpinWait(50);
            _balance = current + amount;
        }
    }

    /// <summary>Returns false instead of letting the balance go negative.</summary>
    public bool TryWithdraw(decimal amount)
    {
        lock (_sync)
        {
            // Check + update must be in the SAME lock, otherwise two threads
            // can both pass the check and overdraw the account.
            if (_balance < amount) return false;
            _balance -= amount;
            return true;
        }
    }

    // Used by Bank while it already holds this account's lock.
    internal void AddUnlocked(decimal amount) => _balance += amount;
    internal decimal BalanceUnlocked => _balance;
}
