using Assinantes.Application.DTOs;
using Assinantes.Application.Services;
using Assinantes.Domain.Entities;
using Assinantes.Domain.Enums;
using Assinantes.Domain.Exceptions;
using Assinantes.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Assinantes.Tests;

public class AssinanteServiceTests
{
    private readonly Mock<IAssinanteRepository> _repositoryMock;
    private readonly AssinanteService _service;

    public AssinanteServiceTests()
    {
        _repositoryMock = new Mock<IAssinanteRepository>();
        _service = new AssinanteService(_repositoryMock.Object);
    }

    [Fact]
    public async Task DeveCriarAssinanteComSucesso()
    {
        _repositoryMock.Setup(r => r.ObterPorEmailAsync(It.IsAny<string>())).ReturnsAsync((Assinante?)null);

        var dto = new CriarAssinanteDto
        {
            NomeCompleto = "Lucas Silva",
            Email = "lucas@email.com",
            DataInicioAssinatura = DateTime.Today.AddMonths(-3),
            Plano = Plano.Basico,
            ValorMensal = 29.90m
        };

        var resultado = await _service.CriarAsync(dto);

        resultado.NomeCompleto.Should().Be("Lucas Silva");
        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Assinante>()), Times.Once);
    }

    [Fact]
    public async Task DeveLancarExcecaoQuandoEmailJaExiste()
    {
        var assinanteExistente = new Assinante("Outro", "lucas@email.com", DateTime.Today.AddMonths(-3), Plano.Basico, 29.90m);
        _repositoryMock.Setup(r => r.ObterPorEmailAsync(It.IsAny<string>())).ReturnsAsync(assinanteExistente);

        var dto = new CriarAssinanteDto { Email = "lucas@email.com", NomeCompleto = "Lucas", DataInicioAssinatura = DateTime.Today.AddMonths(-3), Plano = Plano.Basico, ValorMensal = 29.90m };

        var acao = async () => await _service.CriarAsync(dto);

        await acao.Should().ThrowAsync<DomainException>().WithMessage("Já existe um assinante com este e-mail.");
    }

    [Fact]
    public async Task DeveLancarExcecaoQuandoAssinanteNaoEncontrado()
    {
        _repositoryMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync((Assinante?)null);

        var acao = async () => await _service.ObterPorIdAsync(Guid.NewGuid());

        await acao.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task DeveDesativarAssinanteComSucesso()
    {
        var assinante = new Assinante("Lucas Silva", "lucas@email.com", DateTime.Today.AddMonths(-3), Plano.Basico, 29.90m);
        _repositoryMock.Setup(r => r.ObterPorIdAsync(assinante.Id)).ReturnsAsync(assinante);

        await _service.DesativarAsync(assinante.Id);

        assinante.Ativo.Should().BeFalse();
        _repositoryMock.Verify(r => r.AtualizarAsync(assinante), Times.Once);
    }
}
