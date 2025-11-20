using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{

    public class InserirAvisoHandler : IRequestHandler<InserirAvisoRequest, IOperationResult<AvisoEntity>>
    {
        private readonly IAvisoRepository _repository;

        public InserirAvisoHandler(IAvisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IOperationResult<AvisoEntity>> Handle(InserirAvisoRequest request, CancellationToken cancellationToken)
        {
            var Aviso = new AvisoEntity(request.Titulo, request.Mensagem);
            return await _repository.InserirAvisoAsync(Aviso);
        }
    }
}
