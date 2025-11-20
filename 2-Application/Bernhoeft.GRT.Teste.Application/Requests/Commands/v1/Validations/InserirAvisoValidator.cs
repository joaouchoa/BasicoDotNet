using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class InserirAvisoValidator : AbstractValidator<InserirAvisoRequest>
    {
        public InserirAvisoValidator()
        {
            RuleFor(request => request.Mensagem)
                .NotEmpty()
                .WithMessage(AvisoValidationMessages.NOT_EMPTY_ERROR_MESSAGE);

            RuleFor(request => request.Titulo)
                .NotEmpty()
                .WithMessage(AvisoValidationMessages.NOT_EMPTY_ERROR_MESSAGE);
        }
    }
}
