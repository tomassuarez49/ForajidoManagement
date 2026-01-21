public class ExpenseService
{
    private readonly AppDbContext _context;

    //  Inyección de dependencias
    public ExpenseService(AppDbContext context)
    {
        _context = context;
    }

    //  CREATE
    public Expense Create(Expense expense)
    {
        expense.Date = DateTime.UtcNow;

        _context.Expenses.Add(expense);

        _context.CashMovements.Add(new CashMovement
        {
            Id = Guid.NewGuid(),
            Amount = expense.Amount,
            Type = "OUT",
            Description = $"Gasto: {expense.Category}"
        });


        _context.SaveChanges();

        return expense;
    }

    //  GET ALL
    public List<Expense> GetAll()
    {
        return _context.Expenses
            .OrderByDescending(e => e.Date)
            .ToList();
    }

    //  GET BY ID (útil para update/delete)
    public Expense? GetById(int id)
    {
        return _context.Expenses.FirstOrDefault(e => e.Id == id);
    }

    //  UPDATE
    public bool Update(int id, Expense updated)
    {
        var expense = GetById(id);
        if (expense == null)
            return false;

        expense.Amount = updated.Amount;
        expense.Category = updated.Category;
        expense.Description = updated.Description;

        _context.SaveChanges();
        return true;
    }

    //  DELETE
    public bool Delete(int id)
    {
        var expense = GetById(id);
        if (expense == null)
            return false;

        _context.Expenses.Remove(expense);
        _context.SaveChanges();
        return true;
    }
}
