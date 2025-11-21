using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.Tests.Application.Handlers
{
    public class RemoverAvisoHandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;
        private readonly RemoverAvisoValidator _validator;

        public RemoverAvisoHandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();
            _validator = new RemoverAvisoValidator();
        }

        private RemoverAvisoHandler CreateHandler()
            => new RemoverAvisoHandler(_repoMock.Object, _validator);

        [Fact]
        public async Task Handle_DeveRetornarNotFound_QuandoRequestInvalido()
        {
            // Arrange
            var request = new RemoverAvisoRequest(Id: 0);
            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);
            var expectedStatusCode = OperationResult<AvisoEntity>.ReturnNotFound().StatusCode;

            // Assert
            result.IsSuccessTypeResult.Should().BeFalse();
            result.StatusCode.Should().Be(expectedStatusCode);

            var expectedMessage =
                AvisoValidationMessages.ID_MATCHES_ERROR_MESSAGE.Replace("{PropertyName}", "Id");

            result.Messages.Should().Contain(expectedMessage);
        }

        [Fact]
        public async Task Handle_DeveRetornarNotFound_QuandoAvisoNaoExiste()
        {
            // Arrange
            var request = new RemoverAvisoRequest(Id: 5);

            _repoMock
                .Setup(x => x.ObterAvisoAsync(5, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity)null);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);
            var expected = OperationResult<AvisoEntity>.ReturnNotFound().StatusCode;

            // Assert
            result.IsSuccessTypeResult.Should().BeFalse();
            result.StatusCode.Should().Be(expected);
            result.Messages.Should().Contain(AvisoValidationMessages.AVISO_NAO_EXISTE);
        }

        [Fact]
        public async Task Handle_DeveRemoverComSucesso()
        {
            // Arrange
            var request = new RemoverAvisoRequest(Id: 10);
            var aviso = new AvisoEntity("Teste", "Mensagem de teste");

            // Seta ID via reflexão, pois o setter é private
            aviso.GetType()
                 .GetProperty("Id")!
                 .SetValue(aviso, 10);

            _repoMock
                .Setup(x => x.ObterAvisoAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            AvisoEntity capturado = null!;

            _repoMock
                .Setup(x => x.RemoverAvisoAsync(It.IsAny<AvisoEntity>(), It.IsAny<CancellationToken>()))
                .Returns((AvisoEntity a, CancellationToken _) =>
                {
                    capturado = a;
                    return Task.FromResult(OperationResult<AvisoEntity>.ReturnOk(a));
                });

            var handler = CreateHandler();
            var expected = OperationResult<AvisoEntity>.ReturnOk(aviso).StatusCode;

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccessTypeResult.Should().BeTrue();
            result.StatusCode.Should().Be(expected);

            capturado.Should().NotBeNull();
            capturado.Ativo.Should().BeFalse();

            result.Data.Should().Be(capturado);
        }
    }
}
