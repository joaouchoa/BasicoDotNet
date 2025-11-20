using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisoHandler : IRequestHandler<GetAvisoRequest, IOperationResult<GetAvisosResponse>>
    {
        private readonly IAvisoRepository _repository;

        public GetAvisoHandler(IAvisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IOperationResult<GetAvisosResponse>> Handle(GetAvisoRequest request, CancellationToken cancellationToken)
        {
            var avisoEntity = await _repository.ObterAvisoAsync(request.Id, cancellationToken);

            if (avisoEntity == null)
                return OperationResult<GetAvisosResponse>.ReturnNotFound();

            var response = (GetAvisosResponse)avisoEntity;

            return OperationResult<GetAvisosResponse>.ReturnOk(response);
        }
    }
}
