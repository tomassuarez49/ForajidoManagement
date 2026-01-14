using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/balance")]
public class BalanceController : ControllerBase
{
    private readonly BalanceService _balanceService;

    //  Inyección de dependencias
    public BalanceController(BalanceService balanceService)
    {
        _balanceService = balanceService;
    }

    //  Balance básico (ingresos - egresos)
    [HttpGet]
    public IActionResult GetBalance()
    {
        return Ok(_balanceService.GetBalance());
    }

    //  Balance real (ingresos - costo productos - gastos)
    [HttpGet("real")]
    public IActionResult GetRealBalance()
    {
        return Ok(_balanceService.GetRealBalance());
    }
}
