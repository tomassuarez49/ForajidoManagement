public class Sale
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public decimal Total { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;
    // Efectivo, Nequi, Daviplata

    public List<SaleItem> Items { get; set; } = new();
}
