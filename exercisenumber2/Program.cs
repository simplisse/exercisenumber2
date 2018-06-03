using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercisenumber2
{
    //class Program
    //{
        //static void Main(string[] args)
      //  {
    namespace com.school.homework
    {
        public static class Menu
        {
            public static void Main(String[] args)
            {
                // Declarations
                Accounts account = new Accounts();
                Transactions transaction = new Transactions();

                // New account
                account.firstName = "Simplisse";
                account.lastName = "Eyadema";
                Console.WriteLine("First Name: {0}", account.firstName);
                Console.WriteLine("Last Name: {0}", account.lastName);
                account.GetBalance();

                // Deposit money
                account.balance = transaction.Deposit(account.balance, 10000.00);
                account.GetBalance();

                // Withdraw money
                account.balance = transaction.Withdrawal(account.balance, 302.00);
                account.GetBalance();

                // Leave window open 
                Console.ReadKey();
            }
        }

        public class Transactions
        {
            public double Withdrawal(double balance, double amount)
            {
                Console.WriteLine("Withdrew ${0}", amount);
                return balance - amount;
            }
            public double Deposit(double balance, double amount)
            {
                Console.WriteLine("Depositied ${0}", amount);
                return balance + amount;
            }
        }

        public class Accounts
        {
            public double balance { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }

            public void GetBalance()
            {
                Console.WriteLine("Balance: ${0}", balance);
            }
        }
    }
}
    
