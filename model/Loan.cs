namespace LoanManagement.model
{
    public class Loan
    {
        public int LoanId { get; set; }
        public Customer Customer { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }

        public Loan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanId = loanId;
            Customer = customer;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }

        public Loan() { }
        public override string ToString()
        {
            return $"Loan ID: {LoanId}, Customer ID: {Customer.CustomerID}, " +
                   $"Principal Amount: {PrincipalAmount}, Interest Rate: {InterestRate}, " +
                   $"Loan Term: {LoanTerm} months, Loan Type: {LoanType}, Loan Status: {LoanStatus}";
        }
    }
}
