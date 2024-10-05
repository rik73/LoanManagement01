namespace LoanManagement.model
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }

        // Constructor, getters, setters
        public Customer() { }

        public Customer(int customerId, string name, string email, string phone, string address, int creditScore)
        {
            CustomerID = customerId;
            Name = name;
            EmailAddress = email;
            PhoneNumber = phone;
            Address = address;
            CreditScore = creditScore;
        }
    }
}
