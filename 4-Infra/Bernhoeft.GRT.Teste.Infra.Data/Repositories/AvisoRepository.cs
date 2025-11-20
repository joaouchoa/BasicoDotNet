using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

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
            return DbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IOperationResult<AvisoEntity>> InserirAvisoAsync(AvisoEntity aviso)
        {
                await DbSet.AddAsync(aviso);
                await _context.SaveChangesAsync();
                return OperationResult<AvisoEntity>.ReturnCreated();
        }
    }
}