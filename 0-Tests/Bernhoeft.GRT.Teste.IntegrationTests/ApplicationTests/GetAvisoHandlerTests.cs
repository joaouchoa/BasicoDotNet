using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.Tests.Application.Handlers
{
    public class GetAvisoHandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;
        private readonly GetAvisoValidator _validator;

        public GetAvisoHandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();
            _validator = new GetAvisoValidator();
        }

        private GetAvisoHandler CreateHandler()
            => new GetAvisoHandler(_repoMock.Object, _validator);

        [Fact]
        public async Task Handle_DeveRetornarNotFound_QuandoIdInvalido()
        {
            // Arrange
            var request = new GetAvisoRequest(Id: 0);
            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);
            var expectedStatus = OperationResult<GetAvisosResponse>.ReturnNotFound().StatusCode;

            var expectedMessage =
                AvisoValidationMessages.ID_MATCHES_ERROR_MESSAGE.Replace("{PropertyName}", "Id");

            // Assert
            result.IsSuccessTypeResult.Should().BeFalse();
            result.StatusCode.Should().Be(expectedStatus);
            result.Messages.Should().Contain(expectedMessage);

            _repoMock.Verify(
                x => x.ObterAvisoAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()),
                Times.Never
            );
        }


        [Fact]
        public async Task Handle_DeveRetornarNotFound_QuandoAvisoNaoExiste()
        {
            // Arrange
            var request = new GetAvisoRequest(Id: 10);

            _repoMock
                .Setup(x => x.ObterAvisoAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity)null);

            var handler = CreateHandler();
            var expectedStatus = OperationResult<GetAvisosResponse>.ReturnNotFound().StatusCode;

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccessTypeResult.Should().BeFalse();
            result.StatusCode.Should().Be(expectedStatus);
            result.Messages.Should().Contain(AvisoValidationMessages.AVISO_NAO_EXISTE);

            _repoMock.Verify(
                x => x.ObterAvisoAsync(10, It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Handle_DeveRetornarOk_QuandoAvisoExiste()
        {
            // Arrange
            var request = new GetAvisoRequest(Id: 10);

            var aviso = new AvisoEntity("Título Teste", "Mensagem Teste");
            aviso.GetType().GetProperty("Id")!.SetValue(aviso, 10);

            _repoMock
                .Setup(x => x.ObterAvisoAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            var expectedResponse = (GetAvisosResponse)aviso;
            var expectedStatus = OperationResult<GetAvisosResponse>.ReturnOk(expectedResponse).StatusCode;

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccessTypeResult.Should().BeTrue();
            result.StatusCode.Should().Be(expectedStatus);

            result.Data.Should().NotBeNull();
            result.Data.Should().BeEquivalentTo(expectedResponse);
        }
    }
}