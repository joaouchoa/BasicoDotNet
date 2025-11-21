using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
using Bernhoeft.GRT.Teste.Application.ValidationMessages;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bogus;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.Tests.Application.Handlers
{
    public class AtualizarAvisoHandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;
        private readonly AtualizarAvisoValidator _validator;
        private readonly Faker _faker;

        public AtualizarAvisoHandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();
            _validator = new AtualizarAvisoValidator();
            _faker = new Faker("pt_BR");
        }

        private AtualizarAvisoHandler CreateHandler()
            => new AtualizarAvisoHandler(_repoMock.Object, _validator);

        [Fact]
        public async Task Handle_DeveRetornarNotFound_QuandoRequestInvalido()
        {
            // Arrange
            var request = new AtualizarAvisoRequest { Id = 0, Mensagem = "" };
            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            var expected = OperationResult<AvisoEntity>.ReturnNotFound().StatusCode;

            // Assert
            result.IsSuccessTypeResult.Should().BeFalse();
            result.StatusCode.Should().Be(expected);
        }


        [Fact]
        public async Task Handle_DeveRetornarNotFound_QuandoAvisoNaoExiste()
        {
            // Arrange
            var request = new AtualizarAvisoRequest
            {
                Id = 1,
                Mensagem = "Teste atualizado"
            };

            _repoMock
                .Setup(x => x.ObterAvisoAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity)null);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            var expected = OperationResult<AvisoEntity>.ReturnNotFound().StatusCode;


            // Assert
            result.IsSuccessTypeResult.Should().BeFalse();
            result.StatusCode.Should().Be(expected);
        }

        [Fact]
        public async Task Handle_DeveRetornarBadRequest_QuandoNaoHaMudancas()
        {
            // Arrange
            var aviso = new AvisoEntity("Titulo", "Mensagem original");

            var request = new AtualizarAvisoRequest
            {
                Id = 1,
                Mensagem = aviso.Mensagem // mesma mensagem
            };

            _repoMock
                .Setup(x => x.ObterAvisoAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            var expected = OperationResult<AvisoEntity>.ReturnBadRequest().StatusCode;

            // Assert
            result.IsSuccessTypeResult.Should().BeFalse();
            result.StatusCode.Should().Be(expected);
            result.Messages.Should().Contain(AvisoValidationMessages.AVISO_SEM_MUDANCAS);
        }


        [Fact]
        public async Task Handle_DeveRetornarNotFound_QuandoAvisoInativo()
        {
            // Arrange
            var aviso = new AvisoEntity("Titulo", "Mensagem");
            aviso.Desativar();

            var request = new AtualizarAvisoRequest
            {
                Id = 1,
                Mensagem = "Nova mensagem"
            };

            _repoMock
                .Setup(x => x.ObterAvisoAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

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
        public async Task Handle_DeveAtualizarComSucesso()
        {
            // Arrange
            var aviso = new AvisoEntity("Titulo", "Mensagem antiga");

            var request = new AtualizarAvisoRequest
            {
                Id = 1,
                Mensagem = "Mensagem nova"
            };

            _repoMock
                .Setup(x => x.ObterAvisoAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            _repoMock
                .Setup(x => x.AtualizarAvisoAsync(It.IsAny<AvisoEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity a, CancellationToken _) =>
                   OperationResult<AvisoEntity>.ReturnOk(a)
                );

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            var expected = OperationResult<AvisoEntity>.ReturnOk(aviso).StatusCode;

            // Assert
            result.IsSuccessTypeResult.Should().BeTrue();
            result.StatusCode.Should().Be(expected);
            aviso.Mensagem.Should().Be("Mensagem nova");
            aviso.DataAtualizacao.Should().NotBeNull();
        }
    }
}
