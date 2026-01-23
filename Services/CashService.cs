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

    // ➕ Agregar movimiento de caja (con DTO)
    public CashMovementResponseDto Add(CreateCashMovementDto dto)
    {
        var movement = new CashMovement
        {
            Id = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = dto.Amount,
            Type = dto.Type,
            Description = dto.Description
        };

        _context.CashMovements.Add(movement);
        _context.SaveChanges();

        return new CashMovementResponseDto
        {
            Date = movement.Date,
            Amount = movement.Amount,
            Type = movement.Type,
            Description = movement.Description
        };
    }
}
