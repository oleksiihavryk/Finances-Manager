using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using OleksiiHavryk.Finances.API.Data.Exceptions;
using OleksiiHavryk.Finances.API.Data.Interfaces;
using OleksiiHavryk.Finances.API.Domain;
using OleksiiHavryk.Finances.API.DTO;

namespace OleksiiHavryk.Finances.API.Controllers;
/// <summary>
///     API controller for a record type entities.
/// </summary>
[ApiController]
[Route("[controller]")]
public class RecordController : ControllerBase
{
    private readonly IRecordRepository _repository;
    private readonly IMapper _mapper;

    public RecordController(
        IRecordRepository repository, 
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var records = await _repository.GetAllAsync();
        var recordsDto = _mapper.Map<IEnumerable<RecordDto>>(records);
        return recordsDto.Any() ? Ok(recordsDto) : NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> SaveAsync([FromBody] RecordDto recordDto)
    {
        var record = _mapper.Map<Record>(recordDto);
        var saved = await _repository.SaveAsync(record);
        var savedDto = _mapper.Map<RecordDto>(saved);
        return Created(Request.GetDisplayUrl(), savedDto);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] RecordDto recordDto)
    {
        var record = _mapper.Map<Record>(recordDto);
        var updated = await _repository.UpdateAsync(record);
        var updatedDto = _mapper.Map<RecordDto>(updated);
        return Ok(updatedDto);
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
            return NotFound(nameof(Record.Id));
        }
    }
}