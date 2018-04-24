using System;
using xUnit.Samples.Moq.Enums;

namespace xUnit.Samples.Moq.Contracts
{
	public interface IFrequentFlyerNumberValidator
	{
		IServiceInformation ServiceInformation { get; }
		ValidationMode ValidationMode { get; set; }
		bool IsValid(string frequentFlyerNumber);
		void IsValid(string frequentFlyerNumber, out bool isValid);

		event EventHandler ValidatorLookupPerformed;
	}
}