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
    public IActionResult Add(CreateCashMovementDto dto)
    {
        var result = _service.Add(dto);
        return Ok(result);
    }
}
