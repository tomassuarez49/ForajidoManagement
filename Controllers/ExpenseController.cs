using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/expenses")]
public class ExpensesController : ControllerBase
{
    private readonly ExpenseService _service;

    //  Inyección de dependencias
    public ExpensesController(ExpenseService service)
    {
        _service = service;
    }

    //  CREATE
    [HttpPost]
    public IActionResult Create(Expense expense)
    {
        var created = _service.Create(expense);
        return Ok(created);
    }

    //  GET ALL
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    //  UPDATE
    [HttpPut("{id}")]
    public IActionResult Update(int id, Expense expense)
    {
        var updated = _service.Update(id, expense);
        if (!updated)
            return NotFound();

        return NoContent(); // 204
    }

    //  DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent(); // 204
    }
}
