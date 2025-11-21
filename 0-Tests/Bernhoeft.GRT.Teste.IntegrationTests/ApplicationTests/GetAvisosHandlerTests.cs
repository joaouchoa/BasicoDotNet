using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.Tests.Application.Handlers
{
    public class GetAvisosHandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;

        public GetAvisosHandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();
        }

        private GetAvisosHandler CreateHandler()
            => new GetAvisosHandler(_repoMock.Object);

        [Fact]
        public async Task Handle_DeveRetornarNoContent_QuandoNaoExistiremAvisos()
        {
            // Arrange
            _repoMock
                .Setup(x => x.ObterTodosAvisosAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<AvisoEntity>());

            var handler = CreateHandler();
            var request = new GetAvisosRequest();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            var expected = OperationResult<IEnumerable<GetAvisosResponse>>.ReturnNoContent().StatusCode;

            // Assert
            result.IsSuccessTypeResult.Should().BeTrue();
            result.StatusCode.Should().Be(expected);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task Handle_DeveRetornarOk_QuandoExistiremAvisos()
        {
            // Arrange
            var avisos = new List<AvisoEntity>
            {
                new AvisoEntity("Título 1", "Mensagem 1"),
                new AvisoEntity("Título 2", "Mensagem 2")
            };

            _repoMock
                .Setup(x => x.ObterTodosAvisosAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(avisos);

            var handler = CreateHandler();
            var request = new GetAvisosRequest();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            var expected = OperationResult<IEnumerable<GetAvisosResponse>>.ReturnOk().StatusCode;

            // Assert
            result.IsSuccessTypeResult.Should().BeTrue();
            result.StatusCode.Should().Be(expected);

            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCount(2);

            result.Data.First().Titulo.Should().Be("Título 1");
            result.Data.First().Mensagem.Should().Be("Mensagem 1");
        }
    }
}
