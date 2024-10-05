using System;

namespace LoanManagement.exception
{
    public class InvalidLoanException : Exception
    {
        public InvalidLoanException() : base() { }

        public InvalidLoanException(string message) : base(message) { }

        public InvalidLoanException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
