using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations
{
    public class GetAvisoValidator : AbstractValidator<GetAvisoRequest>
    {
        public GetAvisoValidator()
        {
            RuleFor(request => request.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0)
                .WithMessage(AvisoValidationMessages.ID_MATCHES_ERROR_MESSAGE);
        }
    }
}
