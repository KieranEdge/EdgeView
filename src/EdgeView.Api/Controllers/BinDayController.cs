using EdgeView.Application.Interfaces;
using EdgeView.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EdgeView.Api.Controllers;

[ApiController]
[Route("api/bin-day")]
public class BinDayController : ControllerBase
{
    private readonly IBinDayService _service;

    public BinDayController(IBinDayService service)
    {
        _service = service;
    }

    [HttpGet("next")]
    public async Task<ActionResult<BinCollectionDto>> GetNextBin(
        [FromQuery] string houseNumber,
        [FromQuery] string postcode)
    {
        var result = await _service.GetNextBinAsync(houseNumber, postcode);

        return Ok(new BinCollectionDto
        {
            Date = result.Date,
            BinType = result.BinType
        });
    }
}
