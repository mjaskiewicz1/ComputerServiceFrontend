using Common.Enums;
using Common.Helppers;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Common.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly SendGridClient _client;
    private readonly EmailAddress _from;
    private readonly UriBuilder _urlDetailsLink;

    public EmailSenderService(IConfiguration configuration)
    {
        _client = new SendGridClient(configuration["Application:ApiKeyForSendGrid"]);
        _from = new EmailAddress(configuration["Application:EmailForSendGrid"], configuration["Application:NameForSendGrid"]);
        _urlDetailsLink = new UriBuilder();
        _urlDetailsLink.Scheme = "https";
        _urlDetailsLink.Host = configuration["Application:ClientWebHost"];
        _urlDetailsLink.Path = "Request/Details";
    }


    public async Task SendUrlNewRequest(string email, string name, string rma, string url)
    {
        _urlDetailsLink.Query = $"Url={url}&Rma={rma}";

        var subject = "Zgłoszenie przyjęte";
        var to = new EmailAddress(email);
        var plainTextContent = $"Zgłoszenie zostało przyjęte postępy można śledzić pod adresem {_urlDetailsLink.Uri}";
        var htmlContent = $@"<!DOCTYPE html>
<html lang='pl'>
<head>
    <meta charset='UTF-8'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body>
    <p>Zostało przyjete zgłoszenie numer: {rma} mozesz je śledzić pod adresem  <a href='{_urlDetailsLink.Uri}'>Link</a></p>
</body>
</html>";
        var msg = MailHelper.CreateSingleEmail(_from, to, subject, plainTextContent, htmlContent);
        var response = await _client.SendEmailAsync(msg);
        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
        ;
    }

    public async Task SendUrlUpdateRequest(string email, string name, string rma, string url,
        RequestStatus requestStatus)
    {
        _urlDetailsLink.Query = $"Url={url}&Rma={rma}";

        var subject = "Zmiana statusu zgłoszenia";
        var to = new EmailAddress(email);
        string plainTextContent;
        string htmlContent;
        plainTextContent =
            $"Zmieniono status zgłoszenia numer: {rma}. Obecny status zgłoszenia {requestStatus.ToString()}.Zgłoszenie można śledzić pod adresem {_urlDetailsLink.Uri} ";
        htmlContent = $@"<!DOCTYPE html> 
                <html lang='pl'>
                <head>
                    <meta charset='UTF-8'>
                    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                </head>
                <body>
                    <p>Zmieniono status zgłoszenia numer: <b>{rma}</b> . Obecny status zgłoszenia<b>{requestStatus.ToString()}</b>.</p>
                   
                    <p> zgłoszenie numer {rma} mozesz je śledzić pod adresem  <a href='{_urlDetailsLink.Uri}'>Link</a></p>
                </body>
                </html>";

        var msg = MailHelper.CreateSingleEmail(_from, to, subject, plainTextContent, htmlContent);
        var response = await _client.SendEmailAsync(msg);
        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
        ;
    }

    public async Task SendInvoice(string email, FileHelper file)
    {
        ///https://github.com/sendgrid/sendgrid-csharp/blob/main/USE_CASES.md#attachments
        var subject = "Faktura ";
        var to = new EmailAddress(email);
        var plainTextContent = "Faktura";
        var htmlContent = @"<!DOCTYPE html>
                <html lang='pl'>
                <head>
                    <meta charset='UTF-8'>
                    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Faktura</title>
                </head>
                <body>
                    W załączniku przesyłamy fakturę
                </body>
                </html>";

        var msg = MailHelper.CreateSingleEmail(_from, to, subject, plainTextContent, htmlContent);
        await msg.AddAttachmentAsync(file.Name.Replace("/", "_"), file.FileStream, file.ContentType);

        var response = await _client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
        ;
    }

    public async Task SendUrlNewRequest(string email, string name, string rma, string url, ConfigViewModel config)
    {
        _urlDetailsLink.Query = $"Url={url}&Rma={rma}";

        var subject = "Zgłoszenie przyjęte";
        var to = new EmailAddress(email);
        var plainTextContent = $"Zgłoszenie zostało przyjęte postępy można śledzić pod adresem {_urlDetailsLink.Uri} ";


        var htmlContent = $@"<!DOCTYPE html>
                    <html lang='pl'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    </head>
                    <body>
                        <p>Zostało przyjete zgłoszenie numer: {rma} mozesz je śledzić pod adresem  <a href='{_urlDetailsLink.Uri}'>Link</a></p>
                    Adres na który należy wysłać urządzenie :<b>

                    {config.AddressViewModel.City}   
                    {config.AddressViewModel.Street}
                    {config.AddressViewModel.PostcodeWithDash}
                    {config.PostalTown}
</b>
                    </body>
                    </html>";
        var msg = MailHelper.CreateSingleEmail(_from, to, subject, plainTextContent, htmlContent);
        var response = await _client.SendEmailAsync(msg);
        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
    }
}