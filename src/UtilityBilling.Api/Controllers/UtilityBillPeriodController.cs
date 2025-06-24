using MediatR;
using Microsoft.AspNetCore.Mvc;
using UtilityBilling.Application.Commands.UtilityBill;
using UtilityBilling.Application.Commands.UtilityBillPeriod;
using UtilityBilling.Application.Queries.UtilityBillPeriod;
using UtilityBilling.Contracts.Requests.UtilityBillPeriod;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;

namespace UtilityBilling.Api.Controllers;

// [Authorize]
[Route("api/utility-bill-periods")]
public class UtilityBillPeriodController : BaseController
{
    private readonly ILogger<UtilityBillPeriodController> _logger;
    
    public UtilityBillPeriodController(IMediator mediator, ILogger<UtilityBillPeriodController> logger) : base(mediator)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GetUtilityBillPeriodResult>>> GetUtilityBillPeriodsAsync(CancellationToken cancellationToken)
    {
        var getUtilityBillPeriodsQuery = new GetUtilityBillPeriodsQuery();
        
        _logger.LogInformation("Getting utility bill periods.");

        var result = await _mediator.Send(getUtilityBillPeriodsQuery, cancellationToken);

        if (result.Count == 0)
        {
            return NoContent();
        }

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<GetUtilityBillPeriodResult>> AddUtilityBillPeriodAsync([FromBody] AddUtilityBillPeriodRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var addUtilityBillPeriodCommand = new AddUtilityBillPeriodCommand(request.Name, request.StartDate, request.EndDate);

        var result = await _mediator.Send(addUtilityBillPeriodCommand, cancellationToken);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUtilityBillPeriodResult>> GetUtilityBillPeriodByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var getUtilityBillPeriodByIdQuery = new GetUtilityBillPeriodQuery(id);
        
        var result = await _mediator.Send(getUtilityBillPeriodByIdQuery, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveUtilityBillPeriodByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var removeUtilityBillPeriodCommand = new RemoveUtilityBillPeriodCommand(id);
        
        var result = await _mediator.Send(removeUtilityBillPeriodCommand, cancellationToken);

        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }
    
    [HttpPost("{id}/utility-bills")]
    public async Task<ActionResult<GetUtilityBillPeriodResult>> AddUtilityBillAsync(Guid id, [FromBody] AddUtilityBillRequest request, CancellationToken cancellationToken)
    {
        var addUtilityBillCommand = new AddUtilityBillCommand(id, request);
        
        var result = await _mediator.Send(addUtilityBillCommand, cancellationToken);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpPatch("{id}/utility-bills/{utilityBillId}")]
    public async Task<ActionResult<GetUtilityBillPeriodResult>> UpdateUtilityBillAsync(Guid id, Guid utilityBillId, [FromBody] UpdateUtilityBillRequest request, CancellationToken cancellationToken)
    {
        var updateUtilityBillCommand = new UpdateUtilityBillCommand(id, utilityBillId, request.Usage, request.Cost);
        
        var result = await _mediator.Send(updateUtilityBillCommand, cancellationToken);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpDelete("{id}/utility-bills/{utilityBillId}")]
    public async Task<ActionResult> RemoveUtilityBillAsync(Guid id, Guid utilityBillId, CancellationToken cancellationToken)
    {
        var removeUtilityBillCommand = new RemoveUtilityBillCommand(id, utilityBillId);
        
        var result = await _mediator.Send(removeUtilityBillCommand, cancellationToken);

        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }
}