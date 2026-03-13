using Assinantes.Domain.Enums;

namespace Assinantes.Application.DTOs;

public class AssinanteResponseDto
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataInicioAssinatura { get; set; }
    public Plano Plano { get; set; }
    public decimal ValorMensal { get; set; }
    public bool Ativo { get; set; }
    public int TempoAssinaturaEmMeses { get; set; }
}
