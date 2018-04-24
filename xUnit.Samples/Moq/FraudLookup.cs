using xUnit.Samples.Moq.Models;

namespace xUnit.Samples.Moq
{
	public abstract class FraudLookup
	{
		public bool IsFraudRisk(CreditCardApplication application) => CheckApplication(application);

		protected virtual bool CheckApplication(CreditCardApplication application) => application.LastName == "Smith";
	}
}