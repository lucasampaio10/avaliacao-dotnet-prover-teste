using Assinantes.Application.DTOs;
using Assinantes.Domain.Entities;
using Assinantes.Domain.Exceptions;
using Assinantes.Domain.Interfaces;

namespace Assinantes.Application.Services;

public class AssinanteService
{
    private readonly IAssinanteRepository _repository;

    public AssinanteService(IAssinanteRepository repository)
    {
        _repository = repository;
    }

    public async Task<AssinanteResponseDto> CriarAsync(CriarAssinanteDto dto)
    {
        var emailExistente = await _repository.ObterPorEmailAsync(dto.Email);
        if (emailExistente != null)
            throw new DomainException("Já existe um assinante com este e-mail.");

        var assinante = new Assinante(dto.NomeCompleto, dto.Email, dto.DataInicioAssinatura, dto.Plano, dto.ValorMensal);
        await _repository.AdicionarAsync(assinante);
        return ToDto(assinante);
    }

    public async Task<IEnumerable<AssinanteResponseDto>> ListarAsync()
    {
        var assinantes = await _repository.ListarAtivosAsync();
        return assinantes.Select(ToDto);
    }

    public async Task<AssinanteResponseDto> ObterPorIdAsync(Guid id)
    {
        var assinante = await _repository.ObterPorIdAsync(id);
        if (assinante == null)
            throw new KeyNotFoundException("Assinante não encontrado.");
        return ToDto(assinante);
    }

    public async Task AtualizarAsync(Guid id, CriarAssinanteDto dto)
    {
        var assinante = await _repository.ObterPorIdAsync(id);
        if (assinante == null)
            throw new KeyNotFoundException("Assinante não encontrado.");

        var emailExistente = await _repository.ObterPorEmailAsync(dto.Email);
        if (emailExistente != null && emailExistente.Id != assinante.Id)
            throw new DomainException("Já existe um assinante com este e-mail.");

        assinante.Atualizar(dto.NomeCompleto, dto.Email, dto.DataInicioAssinatura, dto.Plano, dto.ValorMensal);
        await _repository.AtualizarAsync(assinante);
    }

    public async Task DesativarAsync(Guid id)
    {
        var assinante = await _repository.ObterPorIdAsync(id);
        if (assinante == null)
            throw new KeyNotFoundException("Assinante não encontrado.");

        assinante.Desativar();
        await _repository.AtualizarAsync(assinante);
    }

    public async Task ExcluirAsync(Guid id)
    {
        var assinante = await _repository.ObterPorIdAsync(id);
        if (assinante == null)
            throw new KeyNotFoundException("Assinante não encontrado.");

        await _repository.RemoverAsync(assinante);
    }

    private static AssinanteResponseDto ToDto(Assinante assinante) => new()
    {
        Id = assinante.Id,
        NomeCompleto = assinante.NomeCompleto,
        Email = assinante.Email,
        DataInicioAssinatura = assinante.DataInicioAssinatura,
        Plano = assinante.Plano,
        ValorMensal = assinante.ValorMensal,
        Ativo = assinante.Ativo,
        TempoAssinaturaEmMeses = assinante.TempoAssinaturaEmMeses
    };
}
