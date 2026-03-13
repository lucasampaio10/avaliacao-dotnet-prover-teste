using Assinantes.Domain.Enums;

namespace Assinantes.Application.DTOs;

public class CriarAssinanteDto
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataInicioAssinatura { get; set; }
    public Plano Plano { get; set; }
    public decimal ValorMensal { get; set; }
}
