namespace LoanManagement.model
{
    public class HomeLoan : Loan
    {
        public string PropertyAddress { get; set; }
        public decimal PropertyValue { get; set; }

        public HomeLoan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus, string propertyAddress, decimal propertyValue)
            : base(loanId, customer, principalAmount, interestRate, loanTerm, loanType, loanStatus)
        {
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

        public HomeLoan() { }
    }
}
