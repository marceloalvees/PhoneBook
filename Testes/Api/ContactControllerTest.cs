using Api.Controllers;
using Application.Dto;
using Application.UserCases.Agenda.Commands.Save;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Testes.Api
{
    public class ContactControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private ContactController _controller;
        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ContactController(_mediatorMock.Object);
        }

        [Test]
        public async Task CreateContact_ShouldReturnCreated()
        {
            // Arrange
            var command = new SaveContactCommands();
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(new MessageDto(true, "Sucess"));

            // Act
            var result = await _controller.Post(command, CancellationToken.None);

            // Assert
            var createdResult = result as CreatedResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Test]
        public async Task CreateContact_ShouldReturnBadRequest()
        {
            // Arrange
            var command = new SaveContactCommands();
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(new MessageDto(false, "Error"));

            // Act
            var result = await _controller.Post(command, CancellationToken.None);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

    }
}
