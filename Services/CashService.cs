public class CashService
{
    private readonly AppDbContext _context;

    public CashService(AppDbContext context)
    {
        _context = context;
    }

    public decimal GetCash()
    {
        return _context.CashMovements.Sum(m =>
            m.Type == "IN" ? m.Amount : -m.Amount
        );
    }

    public void Add(CashMovement movement)
    {
        movement.Id = Guid.NewGuid();
        _context.CashMovements.Add(movement);
        _context.SaveChanges();
    }
}
