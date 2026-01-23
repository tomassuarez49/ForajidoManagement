public class CreateCashMovementDto
{
    public decimal Amount { get; set; }
    public string Type { get; set; } // IN | OUT
    public string Description { get; set; }
}
