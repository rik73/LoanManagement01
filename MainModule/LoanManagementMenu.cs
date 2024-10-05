using System;
using LoanManagement.dao;
using LoanManagement.model;

namespace LoanManagement.MainModule
{
    public class LoanManagementMenu
    {
        private readonly ILoanRepository loanRepository;

        public LoanManagementMenu()
        {
            loanRepository = new LoanRepositoryImpl();
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("Loan Management System");
                Console.WriteLine("1. Apply for Loan");
                Console.WriteLine("2. Get All Loans");
                Console.WriteLine("3. Get Loan by ID");
                Console.WriteLine("4. Calculate EMI");
                Console.WriteLine("5. Loan Repayment");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ApplyForLoan();
                        break;
                    case 2:
                        GetAllLoans();
                        break;
                    case 3:
                        GetLoanById();
                        break;
                    case 4:
                        CalculateEMI();
                        break;
                    case 5:
                        LoanRepayment();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void ApplyForLoan()
        {
            // Get customer and loan details from user
            Console.WriteLine("Enter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Principal Amount: ");
            decimal principalAmount = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter Interest Rate: ");
            decimal interestRate = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter Loan Term (months): ");
            int loanTerm = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Loan Type (CarLoan or HomeLoan): ");
            string loanType = Console.ReadLine();
            Console.WriteLine("Enter Loan Status: ");
            string loanStatus = Console.ReadLine();

            Loan loan;
            if (loanType == "HomeLoan")
            {
                Console.WriteLine("Enter Property Address: ");
                string propertyAddress = Console.ReadLine();
                Console.WriteLine("Enter Property Value: ");
                decimal propertyValue = decimal.Parse(Console.ReadLine());

                loan = new HomeLoan
                {
                    Customer = new Customer { CustomerID = customerId },
                    PrincipalAmount = principalAmount,
                    InterestRate = interestRate,
                    LoanTerm = loanTerm,
                    LoanType = loanType,
                    LoanStatus = loanStatus,
                    PropertyAddress = propertyAddress,
                    PropertyValue = propertyValue
                };
            }
            else if (loanType == "CarLoan")
            {
                Console.WriteLine("Enter Car Model: ");
                string carModel = Console.ReadLine();
                Console.WriteLine("Enter Car Value: ");
                decimal carValue = decimal.Parse(Console.ReadLine());

                loan = new CarLoan
                {
                    Customer = new Customer { CustomerID = customerId },
                    PrincipalAmount = principalAmount,
                    InterestRate = interestRate,
                    LoanTerm = loanTerm,
                    LoanType = loanType,
                    LoanStatus = loanStatus,
                    CarModel = carModel,
                    CarValue = carValue
                };
            }
            else
            {
                Console.WriteLine("Invalid Loan Type.");
                return;
            }

            loanRepository.ApplyLoan(loan);
        }

        private void GetAllLoans()
        {
            var loans = loanRepository.GetAllLoans();
            foreach (var loan in loans)
            {
                Console.WriteLine(loan);
            }
        }

        private void GetLoanById()
        {
            Console.WriteLine("Enter Loan ID: ");
            int loanId = int.Parse(Console.ReadLine());
            var loan = loanRepository.GetLoanById(loanId);
            Console.WriteLine(loan);
        }

        private void CalculateEMI()
        {
            Console.WriteLine("Enter Loan ID: ");
            int loanId = int.Parse(Console.ReadLine());
            var emi = loanRepository.CalculateEMI(loanId);
            Console.WriteLine($"EMI: {emi}");
        }

        private void LoanRepayment()
        {
            Console.WriteLine("Enter Loan ID: ");
            int loanId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Amount to Repay: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            loanRepository.LoanRepayment(loanId, amount);
        }
    }
}
