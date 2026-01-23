using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stock")]
public class StockController : ControllerBase
{
    private readonly StockMovementService _service;

    public StockController(StockMovementService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult AddMovement(CreateStockMovementDto dto)
        => Ok(_service.AddMovement(dto));

    [HttpGet("{productId}")]
    public IActionResult GetStock(int productId)
        => Ok(_service.GetStock(productId));

    [HttpGet("product/{productId}")]
    public IActionResult GetByProduct(int productId)
        => Ok(_service.GetByProduct(productId));
}
