using System.Text.Json;
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
    public IActionResult AddItem([FromBody] JsonElement body)
    {
        try
        {
            var saleGroup = body.GetProperty("saleGroup").GetString()!;
            var paymentMethod = body.GetProperty("paymentMethod").GetString()!;
            var productId = body.GetProperty("productId").GetInt32();
            var quantity = body.GetProperty("quantity").GetInt32();

            return Ok(_service.AddItem(
                saleGroup,
                paymentMethod,
                productId,
                quantity
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

