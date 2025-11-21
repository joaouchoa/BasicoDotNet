using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisoHandler : IRequestHandler<GetAvisoRequest, IOperationResult<GetAvisosResponse>>
    {
        private readonly IAvisoRepository _repository;
        private readonly GetAvisoValidator _validator;

        public GetAvisoHandler(IAvisoRepository repository, GetAvisoValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IOperationResult<GetAvisosResponse>> Handle(GetAvisoRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
                return OperationResult<GetAvisosResponse>.ReturnNotFound().AddMessage(validationResult.Errors.First().ErrorMessage);

            var avisoEntity = await _repository.ObterAvisoAsync(request.Id, cancellationToken);

            if (avisoEntity == null)
                return OperationResult<GetAvisosResponse>.ReturnNotFound().AddMessage(AvisoValidationMessages.AVISO_NAO_EXISTE);

            var response = (GetAvisosResponse)avisoEntity;

            return OperationResult<GetAvisosResponse>.ReturnOk(response);
        }
    }
}
