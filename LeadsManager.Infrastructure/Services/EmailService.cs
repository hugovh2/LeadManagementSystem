using LeadsManager.Application.Common.Interfaces;

namespace LeadsManager.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly string _emailLogPath;

    public EmailService()
    {
        _emailLogPath = Path.Combine(Directory.GetCurrentDirectory(), "EmailLogs");
        if (!Directory.Exists(_emailLogPath))
        {
            Directory.CreateDirectory(_emailLogPath);
        }
    }

    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var fileName = $"Email_{timestamp}_{Guid.NewGuid()}.txt";
        var filePath = Path.Combine(_emailLogPath, fileName);

        var emailContent = $@"
=====================================
📧 SIMULATED EMAIL NOTIFICATION
=====================================
ℹ️  NOTICE: This email is simulated and saved as a text file in the EmailLogs/ folder.
    In a production environment, this would be sent to: {to}

📨 EMAIL DETAILS:
-------------------------------------
To: {to}
Subject: {subject}
Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
Status: ✅ SIMULATED (File saved successfully)

📄 EMAIL CONTENT:
-------------------------------------
{body}

🔧 TECHNICAL INFO:
-------------------------------------
Service: LeadsManager Email Service (Simulated)
Environment: Development
Log Location: EmailLogs/{fileName}
=====================================
";

        await File.WriteAllTextAsync(filePath, emailContent, cancellationToken);
        
        Console.WriteLine($"📧 [EMAIL SERVICE] Simulated email sent to {to}");
        Console.WriteLine($"📄 [EMAIL SERVICE] Subject: {subject}");
        Console.WriteLine($"💾 [EMAIL SERVICE] Email logged to: {filePath}");
        Console.WriteLine($"ℹ️  [EMAIL SERVICE] This is a simulated email for development purposes");
    }
}
