using Microsoft.EntityFrameworkCore;

public class BalanceService
{
    private readonly AppDbContext _context;

    public BalanceService(AppDbContext context)
    {
        _context = context;
    }

    //  Balance básico (ingresos - egresos)
    public BalanceSummary GetBalance()
    {
        var totalIncome = _context.Sales
            .Sum(s => s.Total);

        var totalExpenses = _context.Expenses
            .Sum(e => e.Amount);

        return new BalanceSummary
        {
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses
        };
    }

    //  Balance real (ingresos - costo productos - gastos)
    public BalanceSummary GetRealBalance()
    {
        //  Ingresos
        var income = _context.Sales
            .Sum(s => s.Total);

        //  Gastos
        var expenses = _context.Expenses
            .Sum(e => e.Amount);

        //  Costo de productos vendidos
        var productCost = _context.SaleItems
            .Join(
                _context.Products,
                item => item.ProductId,
                product => product.Id,
                (item, product) => item.Quantity * product.PurchasePrice
            )
            .Sum();

        return new BalanceSummary
        {
            TotalIncome = income,
            TotalExpenses = expenses + productCost
        };
    }
}
