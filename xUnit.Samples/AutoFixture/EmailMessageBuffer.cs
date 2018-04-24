using System.Collections.Generic;
using System.Linq;

namespace xUnit.Samples.AutoFixture
{
	public class EmailMessageBuffer
	{
		public EmailMessageBuffer(IEmailGateway emailGateway)
		{
			EmailGateway = emailGateway;
			Emails = new Queue<EmailMessage>();
		}

		public Queue<EmailMessage> Emails { get; set; }

		public IEmailGateway EmailGateway { get; }

		public void SendAll()
		{
			while (Emails.Any()) EmailGateway.Send(Emails.Dequeue());
		}

		public void Add(EmailMessage message)
		{
			Emails.Enqueue(message);
		}
	}
}