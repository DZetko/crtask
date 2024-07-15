using System.Reflection.Metadata;
using Coderama.DocumentManager.Application.Command.CreateDocument;
using Coderama.DocumentManager.Application.Command.UpdateCommand;
using Coderama.DocumentManager.Application.Query.GetDocument;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coderama.DocumentManager.Presentation.Controller;

[Route("api/document")]
public class DocumentController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<Document>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(GetDocumentDto getDocument)
    {
        var getDocumentByIdQuery = new GetDocumentByIdQuery(getDocument.Id);
        var document = await sender.Send(getDocumentByIdQuery);
        return Ok(document);
    }
    
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateDocumentDto createDocumentDto)
    {
        var createDocumentCommand = new CreateDocumentCommand(createDocumentDto.Id, createDocumentDto.Tags, createDocumentDto.Data);
        await sender.Send(createDocumentCommand);
        return CreatedAtAction(nameof(Create), createDocumentCommand.Id);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(UpdateDocumentDto updateDocumentDto)
    {
        var updateDocumentCommand = new UpdateDocumentCommand(updateDocumentDto.Id, updateDocumentDto.Tags, updateDocumentDto.Data);
        await sender.Send(updateDocumentCommand);
        return NoContent();
    }
}