using Karim.CRUD.BLL.ModelDtos.EmailDtos;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Karim.CRUD.BLL.ThirdPartyServices.EmailSettings
{
	public class EmailSettings(IOptions<MailSettings> options) : IEmailSettings
	{
		public void SendEmail(Email email)
		{
			var Mail = new MimeMessage()
			{
				Sender = MailboxAddress.Parse(options.Value.Email),
				Subject = email.Subject,
			};
			Mail.To.Add(MailboxAddress.Parse(email.To));
			Mail.From.Add(new MailboxAddress(options.Value.DisplayName, options.Value.Email));
			var Builder = new BodyBuilder();
			Builder.TextBody = email.Body;
			Mail.Body = Builder.ToMessageBody();
			using var smtp = new SmtpClient();
			smtp.Connect(options.Value.Host, options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
			smtp.Authenticate(options.Value.Email, options.Value.Password);
			smtp.Send(Mail);
			smtp.Disconnect(true);
		}
	}
}
