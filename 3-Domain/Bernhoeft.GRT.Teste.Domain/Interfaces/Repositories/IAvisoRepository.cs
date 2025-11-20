using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Domain.Entities;

namespace Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories
{
    public interface IAvisoRepository : IRepository<AvisoEntity>
    {
        Task<List<AvisoEntity>> ObterTodosAvisosAsync(CancellationToken cancellationToken = default);
        Task<IOperationResult<AvisoEntity>> InserirAvisoAsync(AvisoEntity aviso);
        Task<AvisoEntity> ObterAvisoAsync(int Id, CancellationToken cancellationToken = default);
    }
}