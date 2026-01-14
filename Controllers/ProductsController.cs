using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly InventoryService _inventoryService;

    public ProductsController()
    {
        _inventoryService = new InventoryService();
    }

    // GET: api/products
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_inventoryService.GetAll());
    }

    // GET: api/products/
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _inventoryService.GetById(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public IActionResult Create(Product product)
    {
        var created = _inventoryService.Create(product);
        return Ok(created);
    }

    // PUT: api/products/
    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        var updated = _inventoryService.Update(id, product);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/products/
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _inventoryService.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
