public class StockMovementService
{

    private readonly AppDbContext _context;

    public StockMovementService(AppDbContext context)
    {
        _context = context;
    }

    public void AddMovement(StockMovement movement)
    {
        movement.Date = DateTime.UtcNow;
        _context.StockMovements.Add(movement);
        _context.SaveChanges();
        
    }

     public int GetStock(int productId)
    {
        var inQty = _context.StockMovements
            .Where(m => m.ProductId == productId && m.Type == "IN")
            .Sum(m => m.Quantity);

        var outQty = _context.StockMovements
            .Where(m => m.ProductId == productId && m.Type == "OUT")
            .Sum(m => m.Quantity);

        return inQty - outQty;
    }

    public List<StockMovement> GetByProduct(int productId)
    {
        return _context.StockMovements
            .Where(m => m.ProductId == productId)
            .OrderByDescending(m => m.Date)
            .ToList();
    }
}
