public class BalanceService
{
    private readonly SaleService _saleService = new();
    private readonly ExpenseService _expenseService = new();
    private readonly InventoryService _productService = new();

    public BalanceSummary GetBalance()
    {
        var totalIncome = _saleService.GetAll()
            .Sum(s => s.Total);

        var totalExpenses = _expenseService.GetAll()
            .Sum(e => e.Amount);

        return new BalanceSummary
        {
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses
        };
    }

    // Balance con utilidad real (ingreso - costo - gastos)
    public BalanceSummary GetRealProfit()
    {
        decimal totalCost = 0;

        foreach (var sale in _saleService.GetAll())
        {
            foreach (var item in sale.Items)
            {
                var product = _productService.GetById(item.ProductId);
                if (product == null) continue;

                totalCost += item.Quantity * product.PurchasePrice;
            }
        }

        var income = _saleService.GetAll().Sum(s => s.Total);
        var expenses = _expenseService.GetAll().Sum(e => e.Amount);

        return new BalanceSummary
        {
            TotalIncome = income,
            TotalExpenses = expenses + totalCost
        };
    }
}
