using System.Diagnostics;
using ThreadLockDemo;

const int Threads = 10;
const int DepositsPerThread = 1000;
const decimal Expected = Threads * DepositsPerThread; // each deposit = 1

Console.WriteLine("=== Thread Lock Demo: Bank Account ===\n");

// ---------------------------------------------------------------
// 1. Race condition (no lock)
// ---------------------------------------------------------------
Console.WriteLine("1) Deposits WITHOUT lock");
var unsafeAcc = new UnsafeAccount();
RunInThreads(Threads, () =>
{
    for (int i = 0; i < DepositsPerThread; i++) unsafeAcc.Deposit(1);
});
Console.WriteLine($"   Expected: {Expected}, Actual: {unsafeAcc.Balance}  " +
                  (unsafeAcc.Balance == Expected ? "(lucky!)" : "<-- money lost!"));

// ---------------------------------------------------------------
// 2. Same thing WITH lock
// ---------------------------------------------------------------
Console.WriteLine("\n2) Deposits WITH lock");
var safeAcc = new BankAccount(1, "Anh");
RunInThreads(Threads, () =>
{
    for (int i = 0; i < DepositsPerThread; i++) safeAcc.Deposit(1);
});
Console.WriteLine($"   Expected: {Expected}, Actual: {safeAcc.Balance}  " +
                  (safeAcc.Balance == Expected ? "OK" : "BUG"));

// ---------------------------------------------------------------
// 3. Withdraw: check + update inside one lock -> never negative
// ---------------------------------------------------------------
Console.WriteLine("\n3) 20 threads try to withdraw 100 from an account with 1000");
var wallet = new BankAccount(2, "Binh", 1000);
int success = 0, failed = 0;
RunInThreads(20, () =>
{
    if (wallet.TryWithdraw(100)) Interlocked.Increment(ref success);
    else Interlocked.Increment(ref failed);
});
Console.WriteLine($"   Success: {success}, Rejected: {failed}, Final balance: {wallet.Balance}");

// ---------------------------------------------------------------
// 4. Deadlock with two transfers in opposite directions
// ---------------------------------------------------------------
Console.WriteLine("\n4) Transfer A->B and B->A at the same time (naive locking)");
var a = new BankAccount(10, "A", 500);
var b = new BankAccount(20, "B", 500);
bool r1 = false, r2 = false;
var t1 = new Thread(() => r1 = Bank.TransferNaive(a, b, 100));
var t2 = new Thread(() => r2 = Bank.TransferNaive(b, a, 50));
t1.Start(); t2.Start(); t1.Join(); t2.Join();
Console.WriteLine($"   A->B: {(r1 ? "done" : "TIMEOUT (deadlock)")}, " +
                  $"B->A: {(r2 ? "done" : "TIMEOUT (deadlock)")}");

Console.WriteLine("\n5) Same transfers with ordered locking (lock smaller Id first)");
var sw = Stopwatch.StartNew();
t1 = new Thread(() => r1 = Bank.Transfer(a, b, 100));
t2 = new Thread(() => r2 = Bank.Transfer(b, a, 50));
t1.Start(); t2.Start(); t1.Join(); t2.Join();
Console.WriteLine($"   A->B: {(r1 ? "done" : "failed")}, B->A: {(r2 ? "done" : "failed")} in {sw.ElapsedMilliseconds} ms");
Console.WriteLine($"   A = {a.Balance}, B = {b.Balance}, Total = {a.Balance + b.Balance} (should be 1000)");

Console.WriteLine("\nDone. Press Enter to exit.");
if (!Console.IsInputRedirected) Console.ReadLine();

// ---------------------------------------------------------------
static void RunInThreads(int count, Action work)
{
    var threads = Enumerable.Range(0, count).Select(_ => new Thread(() => work())).ToList();
    threads.ForEach(t => t.Start());
    threads.ForEach(t => t.Join());
}
