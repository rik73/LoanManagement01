namespace LoanManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            LoanManagement.MainModule.LoanManagementMenu menu = new LoanManagement.MainModule.LoanManagementMenu();
            menu.ShowMenu();
        }
    }
} 