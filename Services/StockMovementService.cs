public class StockMovementService
{
    private readonly AppDbContext _context;

    public StockMovementService(AppDbContext context)
    {
        _context = context;
    }

    // =========================
    // ADD MOVEMENT
    // =========================
    public StockMovementResponseDto AddMovement(CreateStockMovementDto dto)
    {
        var movement = new StockMovement
        {
            Id = Guid.NewGuid(),
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Type = dto.Type,
            Reason = dto.Reason,
            Date = DateTime.UtcNow
        };

        _context.StockMovements.Add(movement);
        _context.SaveChanges();

        return new StockMovementResponseDto
        {
            Date = movement.Date,
            ProductId = movement.ProductId,
            Quantity = movement.Quantity,
            Type = movement.Type,
            Reason = movement.Reason
        };
    }

    // =========================
    // GET STOCK BY PRODUCT
    // =========================
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

    // =========================
    // GET MOVEMENTS BY PRODUCT
    // =========================
    public List<StockMovementResponseDto> GetByProduct(int productId)
    {
        return _context.StockMovements
            .Where(m => m.ProductId == productId)
            .OrderByDescending(m => m.Date)
            .Select(m => new StockMovementResponseDto
            {
                Date = m.Date,
                ProductId = m.ProductId,
                Quantity = m.Quantity,
                Type = m.Type,
                Reason = m.Reason
            })
            .ToList();
    }
}
