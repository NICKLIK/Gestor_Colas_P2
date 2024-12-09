using Gestor_Colas_P2.Services;
using Microsoft.AspNetCore.Mvc;
using SOA_Models;

namespace Gestor_Colas_P2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProducerController: ControllerBase
{
    private readonly SQS_Service _sqsService;
    
    public ProducerController(SQS_Service sqsService)
    {
        _sqsService = sqsService;
    }
    
    [HttpPost]
    public async Task<IActionResult> SendEmailToQueue([FromBody] EmailMessage emailMessage)
    {
        if (emailMessage == null || string.IsNullOrEmpty(emailMessage.Subject) ||
            string.IsNullOrEmpty(emailMessage.Body) || string.IsNullOrEmpty(emailMessage.Recipient))
        {
            return BadRequest("Subject, Body, and Recipient are required");
        }

        await _sqsService.SendMessageAsync(emailMessage.Subject, emailMessage.Body, emailMessage.Recipient);
        return Ok("Message sent to queue successfully.");
    }
}