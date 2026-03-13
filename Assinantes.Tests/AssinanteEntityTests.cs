using Assinantes.Domain.Entities;
using Assinantes.Domain.Enums;
using Assinantes.Domain.Exceptions;
using FluentAssertions;

namespace Assinantes.Tests;

public class AssinanteEntityTests
{
    [Fact]
    public void DeveCriarAssinanteValido()
    {
        var assinante = new Assinante("Lucas Silva", "lucas@email.com", DateTime.Today.AddMonths(-3), Plano.Basico, 29.90m);

        assinante.NomeCompleto.Should().Be("Lucas Silva");
        assinante.Ativo.Should().BeTrue();
        assinante.TempoAssinaturaEmMeses.Should().BeGreaterThan(0);
    }

    [Fact]
    public void DeveLancarExcecaoQuandoNomeVazio()
    {
        var acao = () => new Assinante("", "lucas@email.com", DateTime.Today.AddMonths(-3), Plano.Basico, 29.90m);

        acao.Should().Throw<DomainException>().WithMessage("Nome completo é obrigatório.");
    }

    [Fact]
    public void DeveLancarExcecaoQuandoEmailInvalido()
    {
        var acao = () => new Assinante("Lucas Silva", "emailinvalido", DateTime.Today.AddMonths(-3), Plano.Basico, 29.90m);

        acao.Should().Throw<DomainException>().WithMessage("E-mail inválido.");
    }

    [Fact]
    public void DeveLancarExcecaoQuandoDataFutura()
    {
        var acao = () => new Assinante("Lucas Silva", "lucas@email.com", DateTime.Today.AddDays(1), Plano.Basico, 29.90m);

        acao.Should().Throw<DomainException>().WithMessage("A data de início da assinatura não pode ser maior que a data atual.");
    }

    [Fact]
    public void DeveLancarExcecaoQuandoValorMensalZero()
    {
        var acao = () => new Assinante("Lucas Silva", "lucas@email.com", DateTime.Today.AddMonths(-3), Plano.Basico, 0);

        acao.Should().Throw<DomainException>().WithMessage("O valor mensal deve ser maior que 0.");
    }

    [Fact]
    public void DeveDesativarAssinante()
    {
        var assinante = new Assinante("Lucas Silva", "lucas@email.com", DateTime.Today.AddMonths(-3), Plano.Basico, 29.90m);

        assinante.Desativar();

        assinante.Ativo.Should().BeFalse();
    }
}
