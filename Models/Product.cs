using System.ComponentModel.DataAnnotations.Schema;

[Table("Products")]
public class Product
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("category")]
    public string Category { get; set; } = string.Empty;

    [Column("purchase_price")]
    public decimal PurchasePrice { get; set; }
    [Column("sale_price")]
    public decimal SalePrice { get; set; }
}
