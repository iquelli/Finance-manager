﻿using Finance;

namespace FinanceProgram
{
    class Program
    {
        public static List<Transaction> transactions = new List <Transaction> ();
        public static FinanceTracker financeTracker = new FinanceTracker();
        public static JsonFinanceStorage JsonStorage = new JsonFinanceStorage();
        static void Main()
        {
            if (File.Exists("transactions.json"))
            {
                transactions = JsonStorage.LoadTransactionData();
            }
            else
            {
                File.WriteAllText("transactions.json", string.Empty);
                financeTracker.income_ = 0;
                financeTracker.expenses_ = 0; 
            }

            bool applicationRunning = true;

            while (applicationRunning)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add transaction.");
                Console.WriteLine("2. View transaction history.");
                Console.WriteLine("3. View financial summaries.");
                Console.WriteLine("4. Exit");
                Console.Write("> ");
                string? userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":   
                    // ADD TRANSACTION
                    Console.WriteLine("");
                    AddTransactionHandler();
                    break;

                    case "2":
                    // VIEW TRANSACTIONS
                    Console.WriteLine("");
                    financeTracker.ViewTransactions();
                    ViewTransactionHandler();
                    break;

                    case "3":
                    // SEE FINANCIAL SUMMARIES
                    Console.WriteLine("");
                    FinancialSummariesHandler();
                    break;

                    case "4":
                    applicationRunning = false;
                    break;

                    default:
                    Console.WriteLine("Please select an option.");
                    break;
                }
            }
        }


        /*---------------------------------------------------------------------------
            Handles user commands
        ----------------------------------------------------------------------------*/


        private static void AddTransactionHandler()
        {
            Console.Write("Type 'B' to return");
            Console.Write("Category: ");
            string? category = Console.ReadLine();
            category = InputValidation(category, "a category");

            Console.Write("Type 'B' to return");
            Console.Write("Description: ");
            string? description = Console.ReadLine();
            description = InputValidation(description, "a description");
            
            Console.Write("Type 'B' to return");
            Console.Write("Amount: ");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("Please enter a decimal: ");
            }

            financeTracker.AddTransaction(description, amount, category);
            Console.WriteLine("Transaction added successfully.");
            Console.WriteLine("");
        }

        private static void ViewTransactionHandler()
        {
            Console.WriteLine("Type 'C' to view transactions by category.");
            Console.WriteLine("Type 'B' to go back.");
            Console.Write("> ");
            string? userInput = Console.ReadLine();
            InputValidation(userInput, "an option");

            if (userInput == "B")
            {
                return;
            }
            if (userInput == "C")
            {
                financeTracker.CategorizeTransaction();
            }
            Console.WriteLine("Please type an option.");
        }

        private static void FinancialSummariesHandler()
        {
            financeTracker.Summary();
            Console.WriteLine("");
            Console.WriteLine($"Total Income: {financeTracker.income_}");
            Console.WriteLine($"Total Expenses: {financeTracker.expenses_}");
            Console.WriteLine($"Total Balance:{financeTracker.income_ - financeTracker.expenses_} ");
            Console.WriteLine("");
        }


        /*------------------------------------------------------------------------------
            UTIL - To avoid code repetition
        -------------------------------------------------------------------------------*/


        private static string InputValidation(string? userInput, string option)
        {
            while (string.IsNullOrEmpty(userInput))
            {
                Console.Write($"Invalid Input. Please enter {option}: ");
                userInput = Console.ReadLine();
            }
            return userInput;
        }
    }
}
