using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stock")]
public class StockController : ControllerBase
{
    private readonly StockMovementService _service = new();

    [HttpPost]
    public IActionResult AddMovement(StockMovement movement)
    {
        _service.AddMovement(movement);
        return Ok();
    }

    [HttpGet("{productId}")]
    public IActionResult GetStock(int productId)
        => Ok(_service.GetStock(productId));
}
