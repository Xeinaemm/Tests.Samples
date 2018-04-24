using AutoFixture.Xunit2;
using Moq;
using xUnit.Samples.AutoFixture;
using xUnit.Tests.Helpers;
using Xunit;

namespace xUnit.Tests.AutoFixture
{
	public class AutoMoqData
	{
		[Theory]
		[AutoMoqData]
		public void ShouldSendEmailToGateway_AutoMoqData_With_Freeze(
			EmailMessage message,
			[Frozen] Mock<IEmailGateway> mock,
			EmailMessageBuffer sut)
		{
			sut.Add(message);
			sut.SendAll();
			mock.Verify(x => x.Send(It.IsAny<EmailMessage>()), Times.Once);
		}
	}
}