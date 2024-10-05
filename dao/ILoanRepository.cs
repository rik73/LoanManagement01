using LoanManagement.model;

namespace LoanManagement.dao
{
    public interface ILoanRepository
    {
        void ApplyLoan(Loan loan);
        decimal CalculateInterest(int loanId);
        string LoanStatus(int loanId);
        decimal CalculateEMI(int loanId);
        void LoanRepayment(int loanId, decimal amount);
        List<Loan> GetAllLoans();
        Loan GetLoanById(int loanId);
    }
}
