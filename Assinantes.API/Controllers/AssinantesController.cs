using Assinantes.Application.DTOs;
using Assinantes.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assinantes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssinantesController : ControllerBase
{
    private readonly AssinanteService _service;

    public AssinantesController(AssinanteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var assinantes = await _service.ListarAsync();
        return Ok(assinantes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var assinante = await _service.ObterPorIdAsync(id);
        return Ok(assinante);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarAssinanteDto dto)
    {
        var assinante = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = assinante.Id }, assinante);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] CriarAssinanteDto dto)
    {
        await _service.AtualizarAsync(id, dto);
        return NoContent();
    }

    [HttpPatch("{id}/desativar")]
    public async Task<IActionResult> Desativar(Guid id)
    {
        await _service.DesativarAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        await _service.ExcluirAsync(id);
        return NoContent();
    }
}
