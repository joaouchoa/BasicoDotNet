using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Domain.Entities;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public class InserirAvisoRequest : IRequest<IOperationResult<AvisoEntity>>
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }
}
