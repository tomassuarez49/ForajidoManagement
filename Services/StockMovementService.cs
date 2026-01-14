public class StockMovementService
{
    private static List<StockMovement> movements = new();
    private static int nextId = 1;

    public void AddMovement(StockMovement movement)
    {
        movement.Id = nextId++;
        movement.Date = DateTime.UtcNow;
        movements.Add(movement);
    }

    public int GetStock(int productId)
    {
        var entradas = movements
            .Where(m => m.ProductId == productId && m.Type == "IN")
            .Sum(m => m.Quantity);

        var salidas = movements
            .Where(m => m.ProductId == productId && m.Type == "OUT")
            .Sum(m => m.Quantity);

        return entradas - salidas;
    }

    public List<StockMovement> GetByProduct(int productId)
        => movements.Where(m => m.ProductId == productId).ToList();
}
