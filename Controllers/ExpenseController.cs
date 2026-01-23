using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/expenses")]
public class ExpensesController : ControllerBase
{
    private readonly ExpenseService _service;

    public ExpensesController(ExpenseService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create(CreateExpenseDto dto)
        => Ok(_service.Create(dto));

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateExpenseDto dto)
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
