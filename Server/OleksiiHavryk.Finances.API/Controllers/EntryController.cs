using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OleksiiHavryk.Finances.API.Data.Exceptions;
using OleksiiHavryk.Finances.API.Data.Interfaces;
using OleksiiHavryk.Finances.API.Domain;
using OleksiiHavryk.Finances.API.DTO;

namespace OleksiiHavryk.Finances.API.Controllers;
/// <summary>
///     API controller for a entry type entities.
/// </summary>
[ApiController]
[Route("[controller]")]
public class EntryController : ControllerBase
{
    private readonly IEntryRepository _repository;
    private readonly IMapper _mapper;

    public EntryController(
        IEntryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost] 
    public async Task<IActionResult> SaveAsync([FromBody] EntryDto currencyDto)
    {
        var entry = _mapper.Map<Entry>(currencyDto);
        var saved = await _repository.SaveAsync(entry);
        var savedDto = _mapper.Map<EntryDto>(saved);
        return Created(Request.GetDisplayUrl(), savedDto);
    }
    [HttpDelete("id/{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
        catch (IdentifierNotFoundException)
        {
            return NotFound(nameof(Currency.Id));
        }
    }
}