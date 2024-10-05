namespace LoanManagement.model
{
    public class CarLoan : Loan
    {
        public string CarModel { get; set; }
        public decimal CarValue { get; set; }

        public CarLoan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus, string carModel, decimal carValue)
            : base(loanId, customer, principalAmount, interestRate, loanTerm, loanType, loanStatus)
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        public CarLoan() { }
    }
}
