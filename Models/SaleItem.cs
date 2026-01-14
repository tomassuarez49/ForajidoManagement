using System.ComponentModel.DataAnnotations.Schema;


[Table("SaleItems")]
public class SaleItem
{
    
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();         // 👈 PRIMARY KEY

    [Column("sale_id")]
    public Guid SaleId { get; set; }    // 👈 FK

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("unit_price")]
    public decimal UnitPrice { get; set; }
}
