using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Teste.Infra.Data.Context;
using Bernhoeft.GRT.Teste.Domain.Entities;
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
            return DbSet.ToListAsync(cancellationToken);
        }
    }
}