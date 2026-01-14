using System.ComponentModel.DataAnnotations.Schema;


public class Expense
{
    [Column("id")]

    public int Id { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("category")]
    public string Category { get; set; } = string.Empty;
    // Insumos, Arriendo, Servicios

    [Column("description")]
    public string Description { get; set; } = string.Empty;
}
