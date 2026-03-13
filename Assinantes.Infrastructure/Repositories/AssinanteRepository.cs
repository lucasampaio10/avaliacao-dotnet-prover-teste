using Assinantes.Domain.Entities;
using Assinantes.Domain.Interfaces;
using Assinantes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assinantes.Infrastructure.Repositories;

public class AssinanteRepository : IAssinanteRepository
{
    private readonly AppDbContext _context;

    public AssinanteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Assinante?> ObterPorIdAsync(Guid id)
    {
        return await _context.Assinantes
            .Where(a => a.Ativo)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Assinante?> ObterPorEmailAsync(string email)
    {
        return await _context.Assinantes
            .FirstOrDefaultAsync(a => a.Email == email.ToLower().Trim());
    }

    public async Task<IEnumerable<Assinante>> ListarAtivosAsync()
    {
        return await _context.Assinantes
            .Where(a => a.Ativo)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Assinante assinante)
    {
        await _context.Assinantes.AddAsync(assinante);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Assinante assinante)
    {
        _context.Assinantes.Update(assinante);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Assinante assinante)
    {
        _context.Assinantes.Remove(assinante);
        await _context.SaveChangesAsync();
    }
}
