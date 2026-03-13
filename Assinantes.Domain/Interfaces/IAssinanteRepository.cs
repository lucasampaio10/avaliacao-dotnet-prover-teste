using Assinantes.Domain.Entities;

namespace Assinantes.Domain.Interfaces;

public interface IAssinanteRepository
{
    Task<Assinante?> ObterPorIdAsync(Guid id);
    Task<Assinante?> ObterPorEmailAsync(string email);
    Task<IEnumerable<Assinante>> ListarAtivosAsync();
    Task AdicionarAsync(Assinante assinante);
    Task AtualizarAsync(Assinante assinante);
    Task RemoverAsync(Assinante assinante);
}
