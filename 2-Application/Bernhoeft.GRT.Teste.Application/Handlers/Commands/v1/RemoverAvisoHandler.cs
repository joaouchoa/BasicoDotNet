using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Core.Models;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class RemoverAvisoHandler : IRequestHandler<RemoverAvisoRequest, IOperationResult<AvisoEntity>>
    {
        private readonly IAvisoRepository _repository;
        private readonly RemoverAvisoValidator _validator;

        public RemoverAvisoHandler(IAvisoRepository repository, RemoverAvisoValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IOperationResult<AvisoEntity>> Handle(RemoverAvisoRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
                return OperationResult<AvisoEntity>.ReturnNotFound().AddMessage(validationResult.Errors.First().ErrorMessage);

            var avisoEntity = await _repository.ObterAvisoAsync(request.Id, cancellationToken);

            if (avisoEntity == null)
                return OperationResult<AvisoEntity>.ReturnNotFound().AddMessage(AvisoValidationMessages.AVISO_NAO_EXISTE);

            avisoEntity.Desativar();

            return await _repository.RemoverAvisoAsync(avisoEntity);
        }
    }
}
