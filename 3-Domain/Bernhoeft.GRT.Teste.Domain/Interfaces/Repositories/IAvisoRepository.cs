using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Teste.Domain.Entities;

namespace Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories
{
    public interface IAvisoRepository : IRepository<AvisoEntity>
    {
        Task<List<AvisoEntity>> ObterTodosAvisosAsync(CancellationToken cancellationToken = default);
    }
}