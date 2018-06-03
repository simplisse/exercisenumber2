using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankaccount
{
    class Program //BankDetails
    {
        static void Main(string[] args)
        {
            app();
        }

            public static void app ()
            {
                BankDetails bankdet = new BankDetails();

                ShowMenu();

            while (true)
                {
                    Console.WriteLine("");

                    string userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "a":
                            Console.WriteLine("'Citire din fisier' selected");
                            break;
                        case "b":
                            Console.WriteLine("'Creare cont' selected");
                            Console.WriteLine("");
                            bankdet.CreateAccount("Name 1");
                            bankdet.CreateAccount("Name 2");
                            bankdet.CreateAccount("");
                            bankdet.CreateAccount("Name 4");
                            bankdet.CreateAccount("Name 5");
                            break;
                        case "c":
                            Console.WriteLine("'Depunere bancara' selected");
                            bankdet.Deposit();
                            break;
                        case "d":
                            Console.WriteLine("'Retragere bancara' selected");
                            bankdet.Withdraw();
                            break;
                        case "e":
                            Console.WriteLine("'Afisare sold' selected");
                            bankdet.Balance();
                            break;
                        case "f":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Please select a valid option");
                            break;
                    }
                }
            }

            public static void ShowMenu()
            {
                Console.WriteLine("a. Citire din fisier");
                Console.WriteLine("b. Creare cont");
                Console.WriteLine("c. Depunere bancara");
                Console.WriteLine("d. Retragere bancara");
                Console.WriteLine("e. Afisare sold");
                Console.WriteLine("f. Iesire");
            }

       
    }
//BAnk detail
class BankDetails : IBankDetails
{
    ReturnedVal rv = new ReturnedVal();
    List<BankAccount> _accounts;

    public BankDetails()
    {
        _accounts = new List<BankAccount>();
    }

    public List<BankAccount> Account
    {
        get { return _accounts; }
    }


    public void CreateAccount(string name)
    {
        BankAccount account = new BankAccount();
        CalculateIBAN calculateIban = new CalculateIBAN();

        try
        {
            if (!String.IsNullOrEmpty(name))
            {
                account.Name = name;
                account.IBAN = calculateIban.IBAN();
                _accounts.Add(account);
                Console.WriteLine("Account created - Name: {0}, IBAN: {1}", account.Name, account.IBAN);
            }
            else
            {
                Console.WriteLine("Account name is null or empty.");
            }
        }
        catch (NullReferenceException ne)
        {
            Console.WriteLine(ne.StackTrace);
        }
    }

    public float Deposit()
    {
        string iban = rv.EnterIban();
        BankAccount account = rv.GetAccountByName(iban, _accounts);

        while (account == null)
        {
            Console.WriteLine("Account doesn't exist");
            iban = rv.EnterIban();
            account = rv.GetAccountByName(iban, _accounts);
        }

        float sum = rv.AmountToDeposit();
        while (sum <= 0)
        {
            Console.WriteLine("Amount cannot be less or equal than 0.");
            sum = 0;
            sum = rv.AmountToDeposit();
        }

        account.Sum += sum;
        Console.WriteLine("Added {0} to account {1}", sum, iban);

        return account.Sum;
    }

    public float Withdraw()
    {
        BankDetails details = new BankDetails();
        BankAccount.Comision c = details.Account;

        string iban = rv.EnterIban();
        BankAccount account = rv.GetAccountByName(iban, _accounts);

        while (account == null)
        {
            Console.WriteLine("Account doesn't exist");
            iban = rv.EnterIban();
            account = rv.GetAccountByName(iban, _accounts);
        }

        float sum = rv.AmountToDeposit();
        while (sum <= 0)
        {
            Console.WriteLine("Amount cannot be less or equal than 0.");
            sum = 0;
            sum = rv.AmountToDeposit();
        }

        account.Sum -= sum;

        Console.Write("Withdrawn {0} from account {1}.", sum, iban);
        Console.WriteLine("Comision {0}", Math.Round(c(account.Sum), 2));
        account.Sum -= c(account.Sum);
        Console.WriteLine("Remaining: {0}", Math.Round(account.Sum, 2));

        return account.Sum;
    }

    public float Balance()
    {
        string iban = rv.EnterIban();
        BankAccount account = rv.GetAccountByName(iban, _accounts);

        Console.WriteLine("IBAN: {0} has {1} left", iban, account.Sum);

        return account.Sum;
    }
}

//BANK DETAIL
interface IBankDetails
            {
                float Balance();
                void CreateAccount(string name);
                float Deposit();
                float Withdraw();
           }


//Returned vall

class ReturnedVal
{
    internal string EnterIban()
    {
        Console.WriteLine("IBAN:");
        string iban = Console.ReadLine();

        return iban;
    }

    internal float AmountToDeposit()
    {
        Console.WriteLine("Type in the amount ");
        float sum = float.Parse(Console.ReadLine());

        return sum;
    }

    internal BankAccount GetAccountByName(string iban, List<BankAccount> _conturi)
    {
        BankAccount ba = _conturi.Find(c => c.IBAN == iban);

        if (ba != null)
        {
            return ba;
        }
        else
        {
            return null;
        }
    }
}

//Calculate BAN

public class CalculateIBAN
{
    static Random generator = new Random();
    string accountNumber = generator.Next(100, 999).ToString();

    public string IBAN()
    {
        return accountNumber;
    }
}

    //SET BANK ACCOUNT

    public class BankAccount
    {
        public string Name { get; set; }
        public string IBAN { get; set; }
        public float Sum { get; set; }

        public delegate float Comision(float comission);
    }
}