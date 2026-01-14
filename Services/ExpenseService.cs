public class ExpenseService
{
    private static List<Expense> expenses = new();
    private static int nextId = 1;

    public Expense Create(Expense expense)
    {
        expense.Id = nextId++;
        expense.Date = DateTime.UtcNow;
        expenses.Add(expense);
        return expense;
    }

    public List<Expense> GetAll() => expenses;
}
