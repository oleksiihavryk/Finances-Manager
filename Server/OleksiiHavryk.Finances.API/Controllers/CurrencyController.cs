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
}