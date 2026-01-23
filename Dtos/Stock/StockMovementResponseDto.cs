public class StockMovementResponseDto
{
    public DateTime Date { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}
