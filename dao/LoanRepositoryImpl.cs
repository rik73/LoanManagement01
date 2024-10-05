using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LoanManagement.exception;
using LoanManagement.model;
using LoanManagement.util;

namespace LoanManagement.dao
{
    public class LoanRepositoryImpl : ILoanRepository
    {
        private static SqlConnection conn;

        public void ApplyLoan(Loan loan)
        {
            
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                string query = "INSERT INTO Loans (CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) " +
                               "VALUES (@CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus); " +
                               "SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerID);
                    cmd.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                    cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                    cmd.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                    cmd.Parameters.AddWithValue("@LoanType", loan.LoanType);
                    cmd.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus);

                    int loanId = Convert.ToInt32(cmd.ExecuteScalar());

                    if (loan is HomeLoan homeLoan)
                    {
                        ApplyHomeLoan(homeLoan, loanId, conn);
                    }
                    else if (loan is CarLoan carLoan)
                    {
                        ApplyCarLoan(carLoan, loanId, conn);
                    }

                    Console.WriteLine("Loan applied successfully.");
                }
            }
        }

        private void ApplyHomeLoan(HomeLoan loan, int loanId, SqlConnection conn)
        {
            string query = "INSERT INTO HomeLoans (LoanId, PropertyAddress, PropertyValue) " +
                           "VALUES (@LoanId, @PropertyAddress, @PropertyValue)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LoanId", loanId);
                cmd.Parameters.AddWithValue("@PropertyAddress", loan.PropertyAddress);
                cmd.Parameters.AddWithValue("@PropertyValue", loan.PropertyValue);
                cmd.ExecuteNonQuery();
            }
        }

        private void ApplyCarLoan(CarLoan loan, int loanId, SqlConnection conn)
        {
            string query = "INSERT INTO CarLoans (LoanId, CarModel, CarValue) " +
                           "VALUES (@LoanId, @CarModel, @CarValue)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LoanId", loanId);
                cmd.Parameters.AddWithValue("@CarModel", loan.CarModel);
                cmd.Parameters.AddWithValue("@CarValue", loan.CarValue);
                cmd.ExecuteNonQuery();
            }
        }

        public decimal CalculateInterest(int loanId)
        {
            Loan loan = GetLoanById(loanId);

            if (loan == null)
                throw new InvalidLoanException("Loan not found!");

            decimal interest = (loan.PrincipalAmount * loan.InterestRate * loan.LoanTerm) / 12;
            return interest;
        }

        public string LoanStatus(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            return loan.LoanStatus;
        }

        public decimal CalculateEMI(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            decimal R = loan.InterestRate / 12 / 100;
            int N = loan.LoanTerm;
            decimal EMI = (loan.PrincipalAmount * R * (decimal)Math.Pow((double)(1 + R), N)) /
                          (decimal)((Math.Pow((double)(1 + R), N)) - 1);

            return EMI;
        }

        public void LoanRepayment(int loanId, decimal amount)
        {
            Loan loan = GetLoanById(loanId);
            decimal emi = CalculateEMI(loanId);

            if (amount < emi)
            {
                Console.WriteLine("Payment rejected. Amount is less than the monthly EMI.");
                return;
            }

            int numEmisPaid = (int)(amount / emi);

        }

        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();

            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                string query = "SELECT * FROM Loans";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Loan loan = new Loan
                    {
                        LoanId = reader.GetInt32(0),
                        PrincipalAmount = reader.GetDecimal(2),
                        InterestRate = reader.GetDecimal(3),
                        LoanTerm = reader.GetInt32(4),
                        LoanType = reader.GetString(5),
                        LoanStatus = reader.GetString(6),
                        Customer = new Customer { CustomerID = reader.GetInt32(1) } // Fetch customer 
                    };
                    loans.Add(loan);
                }
            }
            return loans;
        }

        public Loan GetLoanById(int loanId)
        {
            Loan loan = null;

            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                string query = "SELECT * FROM Loans WHERE LoanId = @LoanId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoanId", loanId);

                
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    loan = new Loan
                    {
                        LoanId = reader.GetInt32(0),
                        PrincipalAmount = reader.GetDecimal(2),
                        InterestRate = reader.GetDecimal(3),
                        LoanTerm = reader.GetInt32(4),
                        LoanType = reader.GetString(5),
                        LoanStatus = reader.GetString(6),
                        Customer = new Customer { CustomerID = reader.GetInt32(1) }
                    };
                }
            }
            return loan;
        }
    }
}
