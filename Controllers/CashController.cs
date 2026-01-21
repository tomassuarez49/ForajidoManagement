using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/cash")]
public class CashController : ControllerBase
{
    private readonly CashService _service;

    public CashController(CashService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetCash()
        => Ok(_service.GetCash());

    [HttpPost]
    public IActionResult Add(CashMovement movement)
    {
        _service.Add(movement);
        return Ok();
    }
}

