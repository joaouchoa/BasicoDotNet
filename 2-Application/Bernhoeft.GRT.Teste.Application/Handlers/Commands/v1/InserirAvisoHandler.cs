using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Core.Models;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{

    public class InserirAvisoHandler : IRequestHandler<InserirAvisoRequest, IOperationResult<AvisoEntity>>
    {
        private readonly IAvisoRepository _repository;
        private readonly InserirAvisoValidator _validator;

        public InserirAvisoHandler(IAvisoRepository repository, InserirAvisoValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IOperationResult<AvisoEntity>> Handle(InserirAvisoRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
                return OperationResult<AvisoEntity>.ReturnNotFound().AddMessage(validationResult.Errors.First().ErrorMessage);

            var Aviso = new AvisoEntity(request.Titulo, request.Mensagem);
            return await _repository.InserirAvisoAsync(Aviso);
        }
    }
}
