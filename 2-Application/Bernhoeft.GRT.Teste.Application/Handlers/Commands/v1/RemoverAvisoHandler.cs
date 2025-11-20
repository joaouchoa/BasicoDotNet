using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class RemoverAvisoHandler : IRequestHandler<RemoverAvisoRequest, IOperationResult<AvisoEntity>>
    {
        private readonly IAvisoRepository _repository;

        public RemoverAvisoHandler(IAvisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IOperationResult<AvisoEntity>> Handle(RemoverAvisoRequest request, CancellationToken cancellationToken)
        {
            var avisoEntity = await _repository.ObterAvisoAsync(request.Id, cancellationToken);

            if (avisoEntity == null)
                return OperationResult<AvisoEntity>.ReturnNotFound().AddMessage("O aviso não existe.");

            return await _repository.RemoverAvisoAsync(avisoEntity);
        }
    }
}
