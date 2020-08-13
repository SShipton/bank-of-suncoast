using System;
using System.Collections.Generic;
using System.Linq;

namespace bank_of_suncoast
{
    class Transaction
    {
        public string Account { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
    }
    class Program
    {
        public static int PromptForInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var userInput = 0;

                var isThisGoodInput = int.TryParse(Console.ReadLine(), out userInput);

                if (isThisGoodInput && userInput >= 0)
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("That isn't a valid input.");
                }
            }
        }
        private static int BalanceForSavings(List<Transaction> transactions)
        {
            var savingsDepositTransactionAmountsTotal = TransactionsTotal(transactions, "Savings", "Desposit");
            var savingsWithdrawTransactionAmountsTotal = TransactionsTotal(transactions, "Savings", "Withdraw");
            var balanceForSavings = savingsDepositTransactionAmountsTotal - savingsWithdrawTransactionAmountsTotal;

            return balanceForSavings;
        }

        private static int BalanceForChecking(List<Transaction> transactions)
        {
            var checkingDepositTransactionAmountsTotal = TransactionsTotal(transactions, "Checking", "Desposit");
            var checkingWithdrawTransactionAmountsTotal = TransactionsTotal(transactions, "Checking", "Withdraw");
            var balanceForChecking = checkingDepositTransactionAmountsTotal - checkingWithdrawTransactionAmountsTotal;

            return balanceForChecking;
        }

        static int TransactionsTotal(List<Transaction> transactions, string account, string type)
        {
            var total = transactions.
            Where(transaction => transaction.Account == account && transaction.Type == type).
            Sum(transaction => transaction.Amount);

            return total;
        }
        static void Main(string[] args)
        {
            var transactions = new List<Transaction>();

            var firstSavingsDeposit = new Transaction
            {
                Amount = 100,
                Type = "Deposit",
                Account = "Savings",
            };
            transactions.Add(firstSavingsDeposit);

            var firstCheckingDeposit = new Transaction
            {
                Amount = 100,
                Type = "Deposit",
                Account = "Checking",
            };
            transactions.Add(firstCheckingDeposit);

            var firstSavingsWithdraw = new Transaction
            {
                Amount = 20,
                Type = "Withdraw",
                Account = "Savings",
            };
            transactions.Add(firstSavingsWithdraw);

            var firstCheckingWithdraw = new Transaction
            {
                Amount = 30,
                Type = "Withdraw",
                Account = "Checking",
            };
            transactions.Add(firstCheckingWithdraw);

            var option = "";

            while (option != "Q")
            {
                Console.WriteLine();
                Console.WriteLine("DC - Deposit to Checking");
                Console.WriteLine("DS - Desposit to Savings");
                Console.WriteLine("WC - Withdraw from Checking");
                Console.WriteLine("WS - Withdraw from Savings");
                Console.WriteLine("B - Show my Balance");
                Console.WriteLine("Q - Quit");
                Console.WriteLine();
                Console.Write("What do you want to do?  ");
                option = Console.ReadLine();

                if (option == "B")
                {
                    int balanceForSavings = BalanceForSavings(transactions);
                    var balanceForChecking = BalanceForChecking(transactions);

                    Console.WriteLine($"Your checking balance is ${balanceForChecking} and your savings balance is ${balanceForSavings}");
                }

                if (option == "DC")
                {
                    var amount = PromptForInt("How much to deposit in Checking? ");

                    var newTransaction = new Transaction { Type = "Deposit", Account = "Checking", Amount = amount };
                    transactions.Add(newTransaction);
                }

                if (option == "DS")
                {
                    var amount = PromptForInt("How much to deposit in Savings? ");

                    var newTransaction = new Transaction { Type = "Deposit", Account = "Savings", Amount = amount };
                    transactions.Add(newTransaction);
                }

                if (option == "WC")
                {
                    var amount = PromptForInt("How much to withdraw from Checking? ");

                    if (amount > BalanceForChecking(transactions))
                    {
                        Console.WriteLine("No overdrafts allowed");
                    }
                    else
                    {
                        var newTransaction = new Transaction { Type = "Withdraw", Account = "Checking", Amount = amount };
                        transactions.Add(newTransaction);
                    }
                }

                if (option == "WS")
                {
                    var amount = PromptForInt("How much to withdraw from Savings? ");

                    if (amount > BalanceForSavings(transactions))
                    {
                        Console.WriteLine("No overdrafts allowed");
                    }
                    else
                    {
                        var newTransaction = new Transaction { Type = "Withdraw", Account = "Savings", Amount = amount };
                        transactions.Add(newTransaction);
                    }
                }
            }
        }
    }
}
