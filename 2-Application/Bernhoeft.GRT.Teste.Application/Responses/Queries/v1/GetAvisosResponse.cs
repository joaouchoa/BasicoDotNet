using Bernhoeft.GRT.Teste.Domain.Entities;

namespace Bernhoeft.GRT.Teste.Application.Responses.Queries.v1
{
    public class GetAvisosResponse
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Titulo { get; set; }

        public static implicit operator GetAvisosResponse(AvisoEntity entity) => new()
        {
            Id = entity.Id,
            Ativo = entity.Ativo,
            Titulo = entity.Titulo
        };
    }
}