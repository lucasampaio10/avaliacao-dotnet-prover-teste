using Assinantes.Domain.Enums;
using Assinantes.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Assinantes.Domain.Entities;

public class Assinante
{
    public Guid Id { get; private set; }
    public string NomeCompleto { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime DataInicioAssinatura { get; private set; }
    public Plano Plano { get; private set; }
    public decimal ValorMensal { get; private set; }
    public bool Ativo { get; private set; }

    public int TempoAssinaturaEmMeses
    {
        get
        {
            var hoje = DateTime.Today;
            return ((hoje.Year - DataInicioAssinatura.Year) * 12) + hoje.Month - DataInicioAssinatura.Month;
        }
    }

    protected Assinante() { }

    public Assinante(string nomeCompleto, string email, DateTime dataInicioAssinatura, Plano plano, decimal valorMensal)
    {
        Validar(nomeCompleto, email, dataInicioAssinatura, valorMensal);

        Id = Guid.NewGuid();
        NomeCompleto = nomeCompleto;
        Email = email.ToLower().Trim();
        DataInicioAssinatura = dataInicioAssinatura;
        Plano = plano;
        ValorMensal = valorMensal;
        Ativo = true;

        if (TempoAssinaturaEmMeses == 0)
            throw new DomainException("O tempo de assinatura não pode ser igual a 0.");
    }

    public void Atualizar(string nomeCompleto, string email, DateTime dataInicioAssinatura, Plano plano, decimal valorMensal)
    {
        Validar(nomeCompleto, email, dataInicioAssinatura, valorMensal);

        NomeCompleto = nomeCompleto;
        Email = email.ToLower().Trim();
        DataInicioAssinatura = dataInicioAssinatura;
        Plano = plano;
        ValorMensal = valorMensal;

        if (TempoAssinaturaEmMeses == 0)
            throw new DomainException("O tempo de assinatura não pode ser igual a 0.");
    }

    public void Desativar() => Ativo = false;

    private static void Validar(string nomeCompleto, string email, DateTime dataInicioAssinatura, decimal valorMensal)
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            throw new DomainException("Nome completo é obrigatório.");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("E-mail é obrigatório.");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new DomainException("E-mail inválido.");

        if (dataInicioAssinatura > DateTime.Today)
            throw new DomainException("A data de início da assinatura não pode ser maior que a data atual.");

        if (valorMensal <= 0)
            throw new DomainException("O valor mensal deve ser maior que 0.");
    }
}
