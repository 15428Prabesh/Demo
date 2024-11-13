using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Application.Services
{
	public class EmailServices : IEmailService
	{
		private readonly SmtpClient _smtpClient;

		public EmailServices(string smtpServer, int port, string username, string password)
		{
			_smtpClient = new SmtpClient(smtpServer)
			{
				Port = port,
				Credentials = new NetworkCredential(username, password),
				EnableSsl = true,
			};
		}
		public async Task SendEmailAsync(string to, string subject, string body)
		{
			var mailMessage = new MailMessage
			{
				From = new MailAddress("your-email@example.com"),
				Subject = subject,
				Body = body,
				IsBodyHtml = true,
			};
			mailMessage.To.Add(to);

			await _smtpClient.SendMailAsync(mailMessage);
		}
	}
}
