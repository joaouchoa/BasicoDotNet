namespace Bernhoeft.GRT.Teste.Domain.Entities
{
    public partial class AvisoEntity
    {
        public int Id { get; private set; }
        public bool Ativo { get; set; } = true;
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }
}