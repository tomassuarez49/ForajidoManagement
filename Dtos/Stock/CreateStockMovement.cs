public class CreateStockMovementDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; } // IN / OUT
    public string Reason { get; set; } // Venta, Compra, Ajuste
}
