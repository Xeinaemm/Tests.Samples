using System.Diagnostics;

namespace xUnit.Samples.AutoFixture
{
	public class EmailGateway : IEmailGateway
	{
		public void Send(EmailMessage message)
		{
			Debug.WriteLine("Sending email to: " + message.ToAddress);
		}
	}
}