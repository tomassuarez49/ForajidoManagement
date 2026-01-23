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
    public IActionResult Create(CreateSaleDto dto)
    {
        try
        {
            return Ok(_service.Create(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var sale = _service.GetById(id);
        if (sale == null)
            return NotFound();

        return Ok(sale);
    }

    [HttpPost("item")]
    public IActionResult AddItem(AddSaleItemDto dto)
    {
        try
        {
            return Ok(_service.AddItem(
                dto.SaleGroup,
                dto.PaymentMethod,
                dto.ProductId,
                dto.Quantity
            ));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
