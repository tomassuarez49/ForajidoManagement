using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stock")]
public class StockController : ControllerBase
{
    private readonly StockMovementService _service;

    // ✅ Inyección de dependencias
    public StockController(StockMovementService service)
    {
        _service = service;
    }

    // ✅ Registrar movimiento de stock (IN / OUT)
    [HttpPost]
    public IActionResult AddMovement(StockMovement movement)
    {
        _service.AddMovement(movement);
        return Ok();
    }

    // ✅ Stock actual por producto
    [HttpGet("{productId}")]
    public IActionResult GetStock(int productId)
    {
        return Ok(_service.GetStock(productId));
    }

    // ✅ Movimientos de stock por producto (auditoría)
    [HttpGet("product/{productId}")]
    public IActionResult GetMovementsByProduct(int productId)
    {
        return Ok(_service.GetByProduct(productId));
    }
}
