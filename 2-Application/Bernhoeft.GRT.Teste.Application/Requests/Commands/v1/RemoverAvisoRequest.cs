using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Domain.Entities;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public record RemoverAvisoRequest(int Id) : IRequest<IOperationResult<AvisoEntity>>;
}
