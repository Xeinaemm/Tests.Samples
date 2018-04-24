namespace xUnit.Samples.AutoFixture
{
	public class EmailMessage
	{
		public EmailMessage(string toAddress, string messageBody, bool isImportant)
		{
			ToAddress = toAddress;
			MessageBody = messageBody;
			IsImportant = isImportant;
		}


		public string ToAddress { get; }
		public string MessageBody { get; }
		public string Subject { get; set; }
		public bool IsImportant { get; }
	}
}