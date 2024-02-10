using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OleksiiHavryk.Finances.API.Data.Exceptions;
using OleksiiHavryk.Finances.API.Data.Interfaces;
using OleksiiHavryk.Finances.API.Domain;
using OleksiiHavryk.Finances.API.DTO;

namespace OleksiiHavryk.Finances.API.Controllers;
/// <summary>
///     API controller for a currency type entities.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyRepository _repository;
    private readonly IMapper _mapper;

    public CurrencyController(
        ICurrencyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var currencies = await _repository.GetAllAsync();
        var currenciesDto = _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
        return currenciesDto.Any() ? Ok(currenciesDto) : NoContent();
    }
    [HttpPost] 
    public async Task<IActionResult> SaveAsync([FromBody] CurrencyDto currencyDto)
    {
        var currency = _mapper.Map<Currency>(currencyDto);
        var saved = await _repository.SaveAsync(currency);
        var savedDto = _mapper.Map<CurrencyDto>(saved);
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