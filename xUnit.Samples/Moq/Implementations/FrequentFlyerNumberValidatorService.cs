using System;
using xUnit.Samples.Moq.Contracts;
using xUnit.Samples.Moq.Enums;

namespace xUnit.Samples.Moq.Implementations
{
	public class FrequentFlyerNumberValidatorService : IFrequentFlyerNumberValidator
	{
		public bool IsValid(string frequentFlyerNumber) => throw new NotImplementedException("For demo purposes");

		public void IsValid(string frequentFlyerNumber, out bool isValid)
		{
			throw new NotImplementedException("For demo purposes");
		}

		public IServiceInformation ServiceInformation => throw new NotImplementedException("For demo purposes");

		public ValidationMode ValidationMode
		{
			get => throw new NotImplementedException("For demo purposes");
			set => throw new NotImplementedException("For demo purposes");
		}

		public event EventHandler ValidatorLookupPerformed;
	}
}