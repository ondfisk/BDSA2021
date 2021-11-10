namespace AsyncAndParallel;

public class Account
{
    public string Name { get; private set; }

    public decimal Balance { get; private set; }

    public Account(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        Balance -= amount;
    }
}

public class Bank
{
    public void Transfer(Account from, Account to, decimal amount)
    {
        from.Withdraw(amount);
        to.Deposit(amount);
    }
}

public class Deadlock
{
    public static void Run()
    {
        var bank = new Bank();
        var a = new Account("a", 100.0m);
        var b = new Account("b", 500.0m);

        var t1 = new Thread(() =>
        {
            lock (a)
            {
                Thread.Sleep(500);
                lock (b)
                {
                    Thread.Sleep(500);
                    bank.Transfer(a, b, 100.0m);
                }
            }
        });
        var t2 = new Thread(() =>
        {
            lock (b)
            {
                Thread.Sleep(1000);
                lock (a)
                {
                    Thread.Sleep(1000);
                    bank.Transfer(b, a, 100.0m);
                }
            }
        });

        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
    }

    public static void RunWithComments()
    {
        var bank = new Bank();
        var a = new Account("a", 100.0m);
        var b = new Account("b", 500.0m);

        var t1 = new Thread(() =>
        {
            Console.WriteLine($"t1 acquiring lock on account {a.Name}");
            lock (a)
            {
                Thread.Sleep(500);
                Console.WriteLine($"t1 lock acquired on account {a.Name}");
                Console.WriteLine($"t1 acquiring lock on account {b.Name}");
                lock (b)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"t1 lock acquired on account {b.Name}");
                    bank.Transfer(a, b, 100.0m);
                    Console.WriteLine("transfered 100.00 from a to b");
                }
                Console.WriteLine($"t1 lock released on account {b.Name}");
            }
            Console.WriteLine($"t1 lock released on account {a.Name}");
        });
        var t2 = new Thread(() =>
        {
            Console.WriteLine($"t2 acquiring lock on account {b.Name}");
            lock (b)
            {
                Thread.Sleep(500);
                Console.WriteLine($"t2 lock acquired on account {b.Name}");
                Console.WriteLine($"t2 acquiring lock on account {a.Name}");
                lock (a)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"t2 lock acquired on account {a.Name}");
                    bank.Transfer(b, a, 100.0m);
                    Console.WriteLine("transfered 100.00 from b to a");
                }
                Console.WriteLine($"t2 lock released on account {a.Name}");
            }
            Console.WriteLine($"t2 lock released on account {a.Name}");
        });

        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();

        Console.WriteLine($"Program terminated");
    }

    public static void RunWithCommentsAndOrder()
    {
        var bank = new Bank();
        var a = new Account("a", 100.0m);
        var b = new Account("b", 500.0m);
        var c = new Account("c", 250.0m);

        var t1 = new Thread(() =>
        {
            var accounts = new[] { a, b }.OrderBy(c => c.Name).ToArray();

            Console.WriteLine($"t1 acquiring lock on account {accounts[0].Name}");
            lock (accounts[0])
            {
                Thread.Sleep(500);
                Console.WriteLine($"t1 lock acquired on account {accounts[0].Name}");
                Console.WriteLine($"t1 acquiring lock on account {accounts[1].Name}");
                lock (accounts[1])
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"t1 lock acquired on account {accounts[1].Name}");
                    bank.Transfer(a, b, 100.0m);
                    Console.WriteLine("transfered 100.00 from a to b");
                }
                Console.WriteLine($"t1 lock released on account {accounts[1].Name}");
            }
            Console.WriteLine($"t1 lock released on account {accounts[0].Name}");
        });
        var t2 = new Thread(() =>
        {
            var accounts = new[] { b, a }.OrderBy(c => c.Name).ToArray();

            Console.WriteLine($"t2 acquiring lock on account {accounts[0]}");
            lock (accounts[0])
            {
                Thread.Sleep(500);
                Console.WriteLine($"t2 lock acquired on account {accounts[1].Name}");
                Console.WriteLine($"t2 acquiring lock on account {accounts[1].Name}");
                lock (accounts[1])
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"t2 lock acquired on account {accounts[1].Name}");
                    bank.Transfer(b, a, 100.0m);
                    Console.WriteLine("transfered 100.00 from b to a");
                }
                Console.WriteLine($"t2 lock released on account {accounts[1].Name}");
            }
            Console.WriteLine($"t2 lock released on account {accounts[0].Name}");
        });
        var t3 = new Thread(() =>
        {
            var accounts = new[] { b, c }.OrderBy(c => c.Name).ToArray();

            Console.WriteLine($"t3 acquiring lock on account {accounts[0].Name}");
            lock (accounts[0])
            {
                Thread.Sleep(500);
                Console.WriteLine($"t3 lock acquired on account {accounts[1].Name}");
                Console.WriteLine($"t3 acquiring lock on account {accounts[1].Name}");
                lock (accounts[1])
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"t3 lock acquired on account {accounts[1].Name}");
                    bank.Transfer(b, c, 100.0m);
                    Console.WriteLine("transfered 100.00 from b to c");
                }
                Console.WriteLine($"t3 lock released on account {accounts[1].Name}");
            }
            Console.WriteLine($"t3 lock released on account {accounts[0].Name}");
        });


        t1.Start();
        t2.Start();
        t3.Start();
        t1.Join();
        t2.Join();
        t3.Join();

        Console.WriteLine($"Program terminated");
    }
}
