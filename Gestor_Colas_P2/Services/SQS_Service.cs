using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using SOA_Models;

namespace Gestor_Colas_P2.Services;

public class SQS_Service
{
    private readonly IAmazonSQS _sqs;
    private readonly string _queueUrl = "https://sqs.us-east-1.amazonaws.com/093130468022/MailQueue";
    
    public SQS_Service(IAmazonSQS sqs)
    {
        _sqs = sqs;
    }
    
    public async Task SendMessageAsync(string subject, string body, string recipient)
    {
        var emailMessage = new EmailMessage()
        {
            Subject = subject,
            Body = body,
            Recipient = recipient
        };

        var messageBody = JsonConvert.SerializeObject(emailMessage);

        var request = new SendMessageRequest()
        {
            QueueUrl = _queueUrl,
            MessageBody = messageBody
        };

        await _sqs.SendMessageAsync(request);
    }

}