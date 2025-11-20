using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bernhoeft.GRT.Teste.Application.ValidationMessages
{
    public static class AvisoValidationMessages
    {
        public const string NOT_EMPTY_ERROR_MESSAGE = "{PropertyName} não pode ser nulo ou vazio.";
        public const string ID_MATCHES_ERROR_MESSAGE = "{PropertyName} deve ser um número inteiro positivo válido.";
    }
}
