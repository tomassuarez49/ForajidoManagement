public class StockMovement
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string Type { get; set; } = string.Empty;
    // IN / OUT

    public string Reason { get; set; } = string.Empty;
    // Venta, Compra, Ajuste

    public DateTime Date { get; set; }
}
