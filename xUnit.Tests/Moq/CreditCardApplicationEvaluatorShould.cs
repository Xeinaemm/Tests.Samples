using System;
using AutoFixture.Xunit2;
using Moq;
using Moq.Protected;
using xUnit.Samples.Moq;
using xUnit.Samples.Moq.Contracts;
using xUnit.Samples.Moq.Enums;
using xUnit.Samples.Moq.Models;
using xUnit.Tests.Helpers;
using Xunit;

namespace xUnit.Tests.Moq
{
	public class CreditCardApplicationEvaluatorShould
	{
		private static void SetupDefaultValuesFor(Mock<IFrequentFlyerNumberValidator> mock)
		{
			mock.SetupAllProperties();
			mock.Setup(x => x.ServiceInformation.License.LicenseKey)
				.Returns("OK");
			mock.Setup(x => x.IsValid(It.IsAny<string>()))
				.Returns(true);
		}

		[Theory]
		[AutoMoqData]
		public void AcceptHighIncomeApplications(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);

			application.GrossAnnualIncome = 100_000;
			var decision = sut.Evaluate(application);

			Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
		}

		[Theory]
		[AutoMoqData]
		public void CheckLicenseKeyForLowIncomeApplications(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);

			application.GrossAnnualIncome = 99_000;
			sut.Evaluate(application);
			mock.VerifyGet(x => x.ServiceInformation.License.LicenseKey, Times.Once);
		}

		[Theory]
		[AutoMoqData]
		public void DeclineLowIncomeApplications(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);

			application.GrossAnnualIncome = 19_999;
			application.Age = 42;
			application.FrequentFlyerNumber = "a";

			var decision = sut.Evaluate(application);

			Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
		}

		[Theory]
		[AutoMoqData]
		public void IncrementLookupCount(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);

			mock.Setup(x => x.IsValid(It.IsAny<string>()))
				.Returns(true)
				.Raises(x => x.ValidatorLookupPerformed += null, EventArgs.Empty);

			application.Age = 25;
			application.FrequentFlyerNumber = "x";

			sut.Evaluate(application);
			Assert.Equal(1, sut.ValidatorLookupCount);
		}

		[Theory]
		[AutoMoqData]
		public void NotValidateFrequentFlyerNumberForHighIncomeApplications(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);

			application.GrossAnnualIncome = 100_000;
			sut.Evaluate(application);
			mock.Verify(x => x.IsValid(It.IsAny<string>()), Times.Never);
		}

		[Theory]
		[AutoMoqData]
		public void ReferFraudRisk(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			[Frozen] Mock<FraudLookup> mockFraud,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);

			mockFraud.Protected()
				.Setup<bool>("CheckApplication", ItExpr.IsAny<CreditCardApplication>())
				.Returns(true);

			var decision = sut.Evaluate(application);
			Assert.Equal(CreditCardApplicationDecision.ReferredToHumanFraudRisk, decision);
		}

		[Theory]
		[AutoMoqData]
		public void ReferInvalidFrequentFlyerApplications(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);
			mock.Setup(x => x.IsValid(It.IsAny<string>()))
				.Returns(false);

			var decision = sut.Evaluate(application);
			Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
		}

		[Theory]
		[AutoMoqData]
		public void ReferInvalidFrequentFlyerApplications_Sequence(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);
			mock.SetupSequence(x => x.IsValid(It.IsAny<string>()))
				.Returns(false)
				.Returns(true);
			application.Age = 25;

			var firstDecision = sut.Evaluate(application);
			Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, firstDecision);

			var secondDecision = sut.Evaluate(application);
			Assert.Equal(CreditCardApplicationDecision.AutoDeclined, secondDecision);
		}

		[Theory]
		[AutoMoqData]
		public void ReferWhenFrequentFlyerValidationError(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);
			mock.Setup(x => x.IsValid(It.IsAny<string>()))
				.Throws(new Exception("Custom message"));

			application.Age = 42;

			var decision = sut.Evaluate(application);
			Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
		}

		[Theory]
		[AutoMoqData]
		public void ReferWhenLicenseKeyExpired(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);
			mock.Setup(x => x.ServiceInformation.License.LicenseKey)
				.Returns("EXPIRED");

			application.Age = 42;

			var decision = sut.Evaluate(application);
			Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
		}

		[Theory]
		[AutoMoqData]
		public void ReferYoungApplications(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);
			application.Age = 19;

			var decision = sut.Evaluate(application);
			Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
		}

		[Theory]
		[AutoMoqData]
		public void SetDetailedLookupForOlderApplications(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);
			application.Age = 30;

			sut.Evaluate(application);
			mock.VerifySet(x => x.ValidationMode = It.IsAny<ValidationMode>(),
				Times.Once);
		}

		[Theory]
		[AutoMoqData]
		public void UseDetailedLookupForOlderApplications(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);
			application.Age = 30;

			sut.Evaluate(application);
			Assert.Equal(ValidationMode.Detailed, mock.Object.ValidationMode);
		}

		[Theory]
		[AutoMoqData]
		public void ValidateFrequentFlyerNumberForLowIncomeApplications(
			[Frozen] Mock<IFrequentFlyerNumberValidator> mock,
			CreditCardApplication application,
			CreditCardApplicationEvaluator sut)
		{
			SetupDefaultValuesFor(mock);
			application.FrequentFlyerNumber = "q";

			sut.Evaluate(application);
			mock.Verify(x => x.IsValid(It.IsAny<string>()), Times.Once);
		}
	}
}