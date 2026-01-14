using System.ComponentModel.DataAnnotations.Schema;

[Table("Sales")]
public class Sale
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("total")]
    public decimal Total { get; set; }

    [Column("payment_method")]
    public string PaymentMethod { get; set; } = string.Empty;
    // Efectivo, Nequi, Daviplata

    public List<SaleItem> Items { get; set; } = new();
}
