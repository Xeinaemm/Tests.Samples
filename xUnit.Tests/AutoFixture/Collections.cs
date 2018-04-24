using AutoFixture;
using xUnit.Samples.AutoFixture;
using xUnit.Tests.Helpers;
using Xunit;

namespace xUnit.Tests.AutoFixture
{
	public class Collections
	{
		[Theory]
		[AutoMoqData]
		public void QueueShouldBeEmpty(Fixture fixture, EmailMessageBuffer sut)
		{
			var collection = fixture.CreateMany<EmailMessage>(10);
			foreach (var emailMessage in collection) sut.Add(emailMessage);

			sut.SendAll();

			Assert.Empty(sut.Emails);
		}
	}
}