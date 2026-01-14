public class BalanceSummary
{
    public decimal TotalIncome { get; set; }

    public decimal TotalExpenses { get; set; }

    public decimal Profit => TotalIncome - TotalExpenses;
}
