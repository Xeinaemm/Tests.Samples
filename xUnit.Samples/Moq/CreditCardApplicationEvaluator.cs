﻿using System;
using xUnit.Samples.Moq.Contracts;
using xUnit.Samples.Moq.Enums;
using xUnit.Samples.Moq.Models;

namespace xUnit.Samples.Moq
{
	public class CreditCardApplicationEvaluator
	{
		private const int AutoReferralMaxAge = 20;
		private const int HighIncomeThreshhold = 100_000;
		private const int LowIncomeThreshhold = 20_000;
		private readonly FraudLookup _fraudLookup;
		private readonly IFrequentFlyerNumberValidator _validator;

		public CreditCardApplicationEvaluator(IFrequentFlyerNumberValidator validator,
			FraudLookup fraudLookup = null)
		{
			_validator = validator ??
						throw new ArgumentNullException(nameof(validator));

			_validator.ValidatorLookupPerformed += ValidatorLookupPerformed;

			_fraudLookup = fraudLookup;
		}

		public int ValidatorLookupCount { get; private set; }

		private void ValidatorLookupPerformed(object sender, EventArgs e)
		{
			ValidatorLookupCount++;
		}

		public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
		{
			if (_fraudLookup != null && _fraudLookup.IsFraudRisk(application))
				return CreditCardApplicationDecision.ReferredToHumanFraudRisk;

			if (application.GrossAnnualIncome >= HighIncomeThreshhold) return CreditCardApplicationDecision.AutoAccepted;

			if (_validator.ServiceInformation.License.LicenseKey == "EXPIRED")
				return CreditCardApplicationDecision.ReferredToHuman;

			_validator.ValidationMode = application.Age >= 30 ? ValidationMode.Detailed : ValidationMode.Quick;

			bool isValidFrequentFlyerNumber;

			try
			{
				isValidFrequentFlyerNumber =
					_validator.IsValid(application.FrequentFlyerNumber);
			}
			catch (Exception)
			{
				return CreditCardApplicationDecision.ReferredToHuman;
			}

			if (!isValidFrequentFlyerNumber) return CreditCardApplicationDecision.ReferredToHuman;

			if (application.Age <= AutoReferralMaxAge) return CreditCardApplicationDecision.ReferredToHuman;

			return application.GrossAnnualIncome < LowIncomeThreshhold
				? CreditCardApplicationDecision.AutoDeclined
				: CreditCardApplicationDecision.ReferredToHuman;
		}
	}
}