using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class RemoverAvisoValidator : AbstractValidator<RemoverAvisoRequest>
    {
        public RemoverAvisoValidator()
        {
            RuleFor(request => request.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0)
                .WithMessage(AvisoValidationMessages.ID_MATCHES_ERROR_MESSAGE);
        }
    }
}
