using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class AtualizarAvisoValidator : AbstractValidator<AtualizarAvisoRequest>
    {
        public AtualizarAvisoValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0)
                .WithMessage(AvisoValidationMessages.ID_MATCHES_ERROR_MESSAGE);

            RuleFor(request => request.Mensagem)
                .NotEmpty()
                .WithMessage(AvisoValidationMessages.NOT_EMPTY_ERROR_MESSAGE);
        }
    }
}
