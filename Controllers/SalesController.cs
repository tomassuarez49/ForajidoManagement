using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    private readonly SaleService _service;

    public SalesController(SaleService service)
    {
        _service = service;
    }

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

    // 🔥 AQUÍ ESTABA EL ERROR
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)   // ✅ Guid
    {
        var sale = _service.GetById(id);
        if (sale == null)
            return NotFound();

        return Ok(sale);
    }

    [HttpPost("item")]
    public IActionResult AddItem(dynamic body)
    {
        try
        {
            return Ok(_service.AddItem(
                body.saleGroup,
                body.paymentMethod,
                (int)body.productId,
                (int)body.quantity
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

