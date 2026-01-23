public class AddSaleItemDto
{
    public string SaleGroup { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
