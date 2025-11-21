using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Core.Models;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class AtualizarAvisoHandler : IRequestHandler<AtualizarAvisoRequest, IOperationResult<AvisoEntity>>
    {
        private readonly IAvisoRepository _repository;
        private readonly AtualizarAvisoValidator _validator;

        public AtualizarAvisoHandler(IAvisoRepository repository, AtualizarAvisoValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IOperationResult<AvisoEntity>> Handle(AtualizarAvisoRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
                return OperationResult<AvisoEntity>.ReturnNotFound().AddMessage(validationResult.Errors.First().ErrorMessage);

            var avisoEntity = await _repository.ObterAvisoAsync(request.Id, cancellationToken);

            if (avisoEntity == null)
                return OperationResult<AvisoEntity>.ReturnNotFound().AddMessage(AvisoValidationMessages.AVISO_NAO_EXISTE);

            if(avisoEntity.Mensagem == request.Mensagem)
                return OperationResult<AvisoEntity>.ReturnBadRequest().AddMessage(AvisoValidationMessages.AVISO_SEM_MUDANCAS);

            if(avisoEntity.Ativo == false)
                return OperationResult<AvisoEntity>.ReturnNotFound().AddMessage(AvisoValidationMessages.AVISO_NAO_EXISTE);

            avisoEntity.Atualizar(request.Mensagem);

            return await _repository.AtualizarAvisoAsync(avisoEntity);
        }
    }
}
