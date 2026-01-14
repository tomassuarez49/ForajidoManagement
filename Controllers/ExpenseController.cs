using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/expenses")]
public class ExpensesController : ControllerBase
{
    private readonly ExpenseService _service = new();

    [HttpPost]
    public IActionResult Create(Expense expense)
        => Ok(_service.Create(expense));

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());
}
