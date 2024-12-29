using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace ETicket.Utility.Utilities
{
    
		public class EmailSender : IEmailSender
		{
			public Task SendEmailAsync(string email, string subject, string message)
			{
			var client = new SmtpClient("smtp.gmail.com", 587)
			{
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("mohamedhamadamohamed71.7@gmail.com", "vpxypvobqnhpvedy")
			};

				return client.SendMailAsync(
					new MailMessage(from: "mohamedhamadamohamed71.7@gmail.com",
									to: email,
									subject,
									message
									)
					{
						IsBodyHtml = true
					}
					);
			}
		}


	
}
