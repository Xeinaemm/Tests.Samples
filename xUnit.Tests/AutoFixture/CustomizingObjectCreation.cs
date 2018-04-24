using System;
using AutoFixture;
using AutoFixture.Xunit2;
using xUnit.Samples.AutoFixture;
using Xunit;

namespace xUnit.Tests.AutoFixture
{
	public class CustomizingObjectCreation
	{
		[Theory]
		[AutoData]
		public void ShouldInjectEverywere(Fixture fixture)
		{
			fixture.Inject("LHR");

			var flight = fixture.Create<FlightDetails>();

			Assert.Equal("LHR", flight.AirlineName);
			Assert.Equal("LHR", flight.ArrivalAirportCode);
			Assert.Equal("LHR", flight.DepartureAirportCode);
		}

		[Theory]
		[AutoData]
		public void CustomInject(Fixture fixture)
		{
			fixture.Inject(new FlightDetails
			{
				DepartureAirportCode = "PER",
				ArrivalAirportCode = "LHR",
				FlightDuration = TimeSpan.FromHours(10),
				AirlineName = "Awesome Aero"
			});

			var flight = fixture.Create<FlightDetails>();


			Assert.Equal("Awesome Aero", flight.AirlineName);
			Assert.Equal("LHR", flight.ArrivalAirportCode);
			Assert.Equal("PER", flight.DepartureAirportCode);
		}

		[Theory]
		[AutoData]
		public void OmitSpecificProperties(Fixture fixture)
		{
			var flight = fixture.Build<FlightDetails>()
				.Without(x => x.ArrivalAirportCode)
				.Without(x => x.DepartureAirportCode)
				.Create();

			Assert.Null(flight.ArrivalAirportCode);
			Assert.Null(flight.DepartureAirportCode);
		}

		[Theory]
		[AutoData]
		public void OmitProperties(Fixture fixture)
		{
			var flight = fixture.Build<FlightDetails>()
				.OmitAutoProperties()
				.Create();

			Assert.Null(flight.ArrivalAirportCode);
			Assert.Null(flight.DepartureAirportCode);
			Assert.Null(flight.AirlineName);
		}

		[Theory]
		[AutoData]
		public void PropertiesShouldHaveValidValues(Fixture fixture)
		{
			var flight = fixture.Build<FlightDetails>()
				.With(x => x.ArrivalAirportCode, "LAX")
				.With(x => x.DepartureAirportCode, "LHR")
				.Create();

			Assert.Equal("LAX", flight.ArrivalAirportCode);
			Assert.Equal("LHR", flight.DepartureAirportCode);
		}

		[Theory]
		[AutoData]
		public void ActionsShouldBeCorrect(Fixture fixture)
		{
			var flight = fixture.Build<FlightDetails>()
				.With(x => x.ArrivalAirportCode, "LAX")
				.With(x => x.DepartureAirportCode, "LHR")
				.Without(x => x.MealOptions)
				.Do(x => x.MealOptions.Add("Chicken"))
				.Do(x => x.MealOptions.Add("Fish"))
				.Create();

			Assert.Equal("Chicken", flight.MealOptions[0]);
			Assert.Equal("Fish", flight.MealOptions[1]);
		}

		[Theory]
		[AutoData]
		public void CustomizeMethodShouldReturnCorrectData(Fixture fixture)
		{
			fixture.Customize<FlightDetails>(fd =>
				fd.With(x => x.DepartureAirportCode, "LHR")
					.With(x => x.ArrivalAirportCode, "LAX")
					.With(x => x.AirlineName, "Fly Fly Premium Air")
					.Without(x => x.MealOptions)
					.Do(x => x.MealOptions.Add("Chicken"))
					.Do(x => x.MealOptions.Add("Fish")));

			var flight = fixture.Create<FlightDetails>();

			Assert.Equal("Fly Fly Premium Air", flight.AirlineName);
			Assert.Equal("LAX", flight.ArrivalAirportCode);
			Assert.Equal("LHR", flight.DepartureAirportCode);
			Assert.Equal("Chicken", flight.MealOptions[0]);
			Assert.Equal("Fish", flight.MealOptions[1]);
		}
	}
}