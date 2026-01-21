public class CashMovement
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string Type { get; set; } // IN | OUT
    public string Description { get; set; }
}
