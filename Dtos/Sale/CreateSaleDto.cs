public class CreateSaleDto
{
    public string PaymentMethod { get; set; }
    public List<CreateSaleItemDto> Items { get; set; } = new();
}
