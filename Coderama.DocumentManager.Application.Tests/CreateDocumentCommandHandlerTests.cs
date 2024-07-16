using Coderama.DocumentManager.Application.Command.CreateDocument;
using Coderama.DocumentManager.Domain;
using Coderama.DocumentManager.Domain.Repository;
using Moq;

namespace Coderama.DocumentManager.Application.Tests;

public class CreateDocumentCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IDocumentRepository> _documentRepository = new();

    [Fact]
    public async Task CreateDocumentCommand_Should_Throw_When_Data_Is_Empty()
     {
        // Arrange
        var createDocumentCommand = new CreateDocumentCommand(
            Guid.Parse("3493f7b9-09c2-4a53-958c-fb61351e50d3"),
            new List<string>{"cars", "bitcoin"},
            null
        );
        var createDocumentCommandHandler = new CreateDocumentCommandHandler(_unitOfWork.Object, _documentRepository.Object);

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => createDocumentCommandHandler.Handle(createDocumentCommand, default));
    }

    [Fact]
    public async Task SaveChanges_Called_When_Valid_Document_Supplied()
    {
        // Arrange
        var createDocumentCommand = new CreateDocumentCommand(
            Guid.Parse("3493f7b9-09c2-4a53-958c-fb61351e50d3"),
            new List<string>{"cars", "bitcoin"},
            "{\"greeting\": \"hello\"}"
        );
        var createDocumentCommandHandler = new CreateDocumentCommandHandler(_unitOfWork.Object, _documentRepository.Object);
        
        // Act
        await createDocumentCommandHandler.Handle(createDocumentCommand, default);
        
        //Assert
        _unitOfWork.Verify(u => u.SaveChangesAsync(default));
    }
}