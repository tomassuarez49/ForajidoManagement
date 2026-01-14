using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/balance")]
public class BalanceController : ControllerBase
{
    private readonly BalanceService _balanceService = new();

    // Balance simple (ingresos - egresos)
    [HttpGet]
    public IActionResult GetBalance()
    {
        return Ok(_balanceService.GetBalance());
    }

    // 🔥 Balance real (incluye costo de productos vendidos)
    [HttpGet("real")]
    public IActionResult GetRealBalance()
    {
        return Ok(_balanceService.GetRealProfit());
    }
}
