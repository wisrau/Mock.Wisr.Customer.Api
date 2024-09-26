namespace Wisr.Customer.Api.Controllers;

using Database;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Note: The Fee table contains the following data...
//
// Threshold | Amount
// 30000     | 5
// 45000     | 10
// 70000     | 15
// 90000     | 20
// 110000    | 30

[Route("api/customer")]
[ApiController]
[Authorize("customer:read-personal-details")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;

    private readonly CustomerDbContext _db;

    public CustomersController(ILogger<CustomersController> logger, CustomerDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpPost("{customerId:int}/total-fees")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetFees(int customerId)
    {
        var customerIncome = await _db
            .Customers.Where(c => c.Id == customerId)
            .Select(c => c.Income)
            .FirstOrDefaultAsync();

        var totalFee = 0;
        List<Fee> fees = new List<Fee>
        {
            new Fee { Amount = 750, Threshold = 90000 },
            new Fee { Amount = 500, Threshold = 30000 },
            new Fee { Amount = 1000, Threshold = 120000 }
        };

        foreach (var fee in fees)
        {
            if (customerIncome >= fee.Threshold)
            {
                totalFee += fee.Amount;
            }
        }

        return Ok(new { totalFee, customerIncome });
    }
}
