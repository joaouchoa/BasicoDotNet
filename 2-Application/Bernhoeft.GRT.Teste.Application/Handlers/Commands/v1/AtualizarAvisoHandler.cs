using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Core.Models;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class AtualizarAvisoHandler : IRequestHandler<AtualizarAvisoRequest, IOperationResult<AvisoEntity>>
    {
        private readonly IAvisoRepository _repository;

        public AtualizarAvisoHandler(IAvisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IOperationResult<AvisoEntity>> Handle(AtualizarAvisoRequest request, CancellationToken cancellationToken)
        {
            var avisoEntity = await _repository.ObterAvisoAsync(request.Id, cancellationToken);

            if (avisoEntity == null)
                return OperationResult<AvisoEntity>.ReturnNotFound().AddMessage("O aviso não existe.");

            if(avisoEntity.Mensagem == request.Mensagem && avisoEntity.Titulo == request.Titulo)
                return OperationResult<AvisoEntity>.ReturnBadRequest().AddMessage("O aviso fornecido não tem diferenças com o da base.");

            avisoEntity.Atualizar(request.Titulo, request.Mensagem);

            return await _repository.AtualizarAvisoAsync(avisoEntity);
        }
    }
}
