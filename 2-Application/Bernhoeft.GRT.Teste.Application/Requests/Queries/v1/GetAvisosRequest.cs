using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Core.Interfaces.Results;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1
{
    public record GetAvisosRequest : IRequest<IOperationResult<IEnumerable<GetAvisosResponse>>>;
}