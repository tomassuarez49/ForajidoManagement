public class Expense
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public string Category { get; set; } = string.Empty;
    // Insumos, Arriendo, Servicios

    public string Description { get; set; } = string.Empty;
}
