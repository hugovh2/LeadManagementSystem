using LeadsManager.Application.Commands;
using LeadsManager.Application.DTOs;
using LeadsManager.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeadsManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeadDto>>> GetAllLeads()
    {
        var query = new GetAllLeadsQuery();
        var leads = await _mediator.Send(query);
        return Ok(leads);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<LeadDto>>> GetLeadsByStatus(string status)
    {
        try
        {
            var query = new GetLeadsByStatusQuery(status);
            var leads = await _mediator.Send(query);
            return Ok(leads);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateLead([FromBody] CreateLeadCommand command)
    {
        try
        {
            var leadId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllLeads), new { id = leadId }, leadId);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}/accept")]
    public async Task<ActionResult> AcceptLead(Guid id)
    {
        try
        {
            var command = new AcceptLeadCommand(id);
            await _mediator.Send(command);
            return Ok(new { message = "Lead accepted successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}/decline")]
    public async Task<ActionResult> DeclineLead(Guid id)
    {
        try
        {
            var command = new DeclineLeadCommand(id);
            await _mediator.Send(command);
            return Ok(new { message = "Lead declined successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
