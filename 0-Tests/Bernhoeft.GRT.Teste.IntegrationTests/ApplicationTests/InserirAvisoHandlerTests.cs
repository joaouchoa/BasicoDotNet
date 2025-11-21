using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bogus;
using FluentAssertions;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.Tests.Application.Handlers
{
    public class InserirAvisoHandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;
        private readonly InserirAvisoValidator _validator;
        private readonly Faker _faker;

        public InserirAvisoHandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();
            _validator = new InserirAvisoValidator();
            _faker = new Faker("pt_BR");
        }

        private InserirAvisoHandler CreateHandler()
            => new InserirAvisoHandler(_repoMock.Object, _validator);

        [Fact]
        public async Task Handle_DeveRetornarNotFound_QuandoRequestInvalido()
        {
            // Arrange
            var request = new InserirAvisoRequest { Titulo = "", Mensagem = "" };
            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);
            var expectedStatusCode = OperationResult<AvisoEntity>.ReturnNotFound().StatusCode;

            // Assert
            result.IsSuccessTypeResult.Should().BeFalse();
            result.StatusCode.Should().Be(expectedStatusCode);
            result.Messages.Should().NotBeEmpty();
            _repoMock.Verify(x => x.InserirAvisoAsync(It.IsAny<AvisoEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_DeveInserirComSucesso()
        {
            // Arrange
            var titulo = _faker.Lorem.Sentence(3);
            var mensagem = "Mensagem teste";

            var request = new InserirAvisoRequest
            {
                Titulo = titulo,
                Mensagem = mensagem
            };

            AvisoEntity capturado = null;

            _repoMock
                .Setup(x => x.InserirAvisoAsync(It.IsAny<AvisoEntity>()))
                .ReturnsAsync((AvisoEntity aviso) =>
                {
                    capturado = aviso;
                    return OperationResult<AvisoEntity>.ReturnOk(aviso);
                });

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            var expected = OperationResult<AvisoEntity>.ReturnOk(capturado).StatusCode;

            // Assert
            result.IsSuccessTypeResult.Should().BeTrue();
            result.StatusCode.Should().Be(expected);

            capturado.Should().NotBeNull();
            capturado.Titulo.Should().Be(request.Titulo);
            capturado.Mensagem.Should().Be(request.Mensagem);
            capturado.Ativo.Should().BeTrue();
            capturado.DataCriacao.Should().NotBe(default);

            result.Data.Should().Be(capturado);
        }
    }
}