using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    private readonly SaleService _service = new();

    [HttpPost]
    public IActionResult Create(Sale sale)
    {
        try
        {
            return Ok(_service.Create(sale));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());
}
