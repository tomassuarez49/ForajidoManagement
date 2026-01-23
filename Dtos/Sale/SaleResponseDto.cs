public class SaleResponseDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public List<SaleItemResponseDto> Items { get; set; } = new();
}
