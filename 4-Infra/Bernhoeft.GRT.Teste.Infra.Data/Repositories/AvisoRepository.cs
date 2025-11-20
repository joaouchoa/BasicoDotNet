using Bernhoeft.GRT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Infra.Data.Context;
using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;


namespace Bernhoeft.GRT.Teste.Infra.Data.Repositories
{
    public class AvisoRepository : Repository<AvisoEntity>, IAvisoRepository
    {
        protected readonly DbContext _context;
        protected DbSet<AvisoEntity> DbSet => _context.Set<AvisoEntity>();

        public AvisoRepository(AvisoDbContext context) : base(context) 
        {
            _context = context;
        }

        public Task<List<AvisoEntity>> ObterTodosAvisosAsync(CancellationToken cancellationToken = default)
        {
            return DbSet.AsNoTracking().Where(a => a.Ativo).ToListAsync(cancellationToken);
        }

        public async Task<IOperationResult<AvisoEntity>> InserirAvisoAsync(AvisoEntity aviso)
        {
            await DbSet.AddAsync(aviso);
            await _context.SaveChangesAsync();
            return OperationResult<AvisoEntity>.ReturnCreated();
        }

        public async Task<AvisoEntity> ObterAvisoAsync(int Id, CancellationToken cancellationToken = default)
        {
            return await DbSet.AsNoTracking().Where(a => a.Ativo && a.Id == Id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IOperationResult<AvisoEntity>> AtualizarAvisoAsync(AvisoEntity aviso, CancellationToken cancellationToken = default)
        {
            _context.Entry(aviso).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult<AvisoEntity>.ReturnNoContent();
        }

        public async Task<IOperationResult<AvisoEntity>> RemoverAvisoAsync(AvisoEntity aviso, CancellationToken cancellationToken = default)
        {
            _context.Entry(aviso).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult<AvisoEntity>.ReturnNoContent();
        }
    }
}