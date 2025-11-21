namespace Bernhoeft.GRT.Teste.Application.ValidationMessages
{
    public static class AvisoValidationMessages
    {
        public const string NOT_EMPTY_ERROR_MESSAGE = "{PropertyName} não pode ser nulo ou vazio.";
        public const string ID_MATCHES_ERROR_MESSAGE = "{PropertyName} deve ser um número inteiro positivo válido.";
        public const string AVISO_NAO_EXISTE = "O aviso informado não existe.";
        public const string AVISO_SEM_MUDANCAS = "O aviso informado não contem mudanças.";
    }
}
