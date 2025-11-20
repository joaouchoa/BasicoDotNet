using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisosHandler : IRequestHandler<GetAvisosRequest, IOperationResult<IEnumerable<GetAvisosResponse>>>
    {
        private readonly IAvisoRepository _repository;

        public GetAvisosHandler(IAvisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IOperationResult<IEnumerable<GetAvisosResponse>>> Handle(GetAvisosRequest request, CancellationToken cancellationToken)
        {
            var avisos = await _repository.ObterTodosAvisosAsync(cancellationToken);
            var response = avisos.Select(a => (GetAvisosResponse)a);

            if (!response.Any())
                return OperationResult<IEnumerable<GetAvisosResponse>>.ReturnNoContent();

            return OperationResult<IEnumerable<GetAvisosResponse>>.ReturnOk(response);
        }
    }
}