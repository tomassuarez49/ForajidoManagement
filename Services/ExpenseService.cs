public class ExpenseService
{
    private readonly AppDbContext _context;
 
    public ExpenseService(AppDbContext context)
    {
        _context = context;
    }

    // ➕ CREATE
    public ExpenseResponseDto Create(CreateExpenseDto dto)
    {
        var expense = new Expense
        {
            Date = DateTime.UtcNow,
            Amount = dto.Amount,
            Category = dto.Category,
            Description = dto.Description
        };

        _context.Expenses.Add(expense);

        _context.CashMovements.Add(new CashMovement
        {
            Id = Guid.NewGuid(),
            Amount = expense.Amount,
            Type = "OUT",
            Description = $"Gasto: {expense.Category}"
        });

        _context.SaveChanges();

        return new ExpenseResponseDto
        {
            Id = expense.Id,
            Date = expense.Date,
            Amount = expense.Amount,
            Category = expense.Category,
            Description = expense.Description
        };
    }

    // 📄 GET ALL
    public List<ExpenseResponseDto> GetAll()
    {
        return _context.Expenses
            .OrderByDescending(e => e.Date)
            .Select(e => new ExpenseResponseDto
            {
                Id = e.Id,
                Date = e.Date,
                Amount = e.Amount,
                Category = e.Category,
                Description = e.Description
            })
            .ToList();
    }

    // 🔍 GET BY ID
    public ExpenseResponseDto? GetById(int id)
    {
        return _context.Expenses
            .Where(e => e.Id == id)
            .Select(e => new ExpenseResponseDto
            {
                Id = e.Id,
                Date = e.Date,
                Amount = e.Amount,
                Category = e.Category,
                Description = e.Description
            })
            .FirstOrDefault();
    }

    // ✏️ UPDATE
    public bool Update(int id, UpdateExpenseDto dto)
    {
        var expense = _context.Expenses.FirstOrDefault(e => e.Id == id);
        if (expense == null)
            return false;

        expense.Amount = dto.Amount;
        expense.Category = dto.Category;
        expense.Description = dto.Description;

        _context.SaveChanges();
        return true;
    }

    // 🗑️ DELETE
    public bool Delete(int id)
    {
        var expense = _context.Expenses.FirstOrDefault(e => e.Id == id);
        if (expense == null)
            return false;

        _context.Expenses.Remove(expense);
        _context.SaveChanges();
        return true;
    }
}
