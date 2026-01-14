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
    public IActionResult Create(Product product)
        => Ok(_service.Create(product));

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());

    // ✅ UPDATE
    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        var updated = _service.Update(id, product);
        if (!updated)
            return NotFound();

        return NoContent(); // 204
    }

    // ✅ DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent(); // 204
    }
}
