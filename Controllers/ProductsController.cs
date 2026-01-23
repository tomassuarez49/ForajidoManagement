using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly InventoryService _service;

    public ProductsController(InventoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create(CreateProductDto dto)
        => Ok(_service.Create(dto));

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _service.GetById(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateProductDto dto)
    {
        var updated = _service.Update(id, dto);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
