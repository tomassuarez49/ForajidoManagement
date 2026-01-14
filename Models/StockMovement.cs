using System.ComponentModel.DataAnnotations.Schema;

public class StockMovement
{
    [Column("id")]
    public Guid Id { get; set; } 

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("type")]
    public string Type { get; set; } = string.Empty;
    // IN / OUT

    [Column("reason")]
    public string Reason { get; set; } = string.Empty;
    // Venta, Compra, Ajuste

    [Column("date")]
    public DateTime Date { get; set; }
}
