using AutoFixture;
using AutoFixture.Xunit2;
using xUnit.Samples.AutoFixture;
using Xunit;

namespace xUnit.Tests.Helpers.FixturePipeline
{
	public class CustomizingAutoFixturePipeline
	{
		[Theory]
		[AutoData]
		public void CustomizedPipeline(Fixture fixture)
		{
			fixture.Customizations.Add(new AirportCodeSpecimenBuilder());
			var flight = fixture.Create<FlightDetails>();
		}
	}
}