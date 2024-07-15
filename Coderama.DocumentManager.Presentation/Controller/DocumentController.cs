using Coderama.DocumentManager.Application.Command.CreateDocument;
using Coderama.DocumentManager.Application.Command.UpdateCommand;
using Coderama.DocumentManager.Application.Query.GetDocument;
using Coderama.DocumentManager.Presentation.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coderama.DocumentManager.Presentation.Controller;

[Route("api/document")]
public class DocumentController(ISender sender) : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType<DocumentDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute]Guid id)
    {
        var getDocumentByIdQuery = new GetDocumentByIdQuery(id);
        var document = await sender.Send(getDocumentByIdQuery);
        if (document is null)
        {
            return NotFound($"Could not find a document with id {id}");
        }
        
        var documentDto = new DocumentDto
        {
            Id = document.Id,
            Tags = document.Tags.Select(t => t.Value).ToList(),
            Data = document.Data
        };
        
        return Ok(documentDto);
    }
    
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody]CreateDocumentRequest createDocumentRequest)
    {
        var createDocumentCommand = new CreateDocumentCommand(createDocumentRequest.Id, createDocumentRequest.Tags, createDocumentRequest.Data);
        await sender.Send(createDocumentCommand);
        return CreatedAtAction(nameof(Create), createDocumentCommand.Id);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update([FromBody]UpdateDocumentRequest updateDocumentRequest)
    {
        var updateDocumentCommand = new UpdateDocumentCommand(updateDocumentRequest.Id, updateDocumentRequest.Tags, updateDocumentRequest.Data);
        await sender.Send(updateDocumentCommand);
        return NoContent();
    }
}