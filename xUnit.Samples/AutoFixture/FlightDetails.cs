using System;
using System.Collections.Generic;

namespace xUnit.Samples.AutoFixture
{
	public class FlightDetails
	{
		private string _arrivalAirportCode;
		private string _departureAirportCode;

		public FlightDetails() => MealOptions = new List<string>();

		public string DepartureAirportCode
		{
			get => _departureAirportCode;
			set
			{
				EnsureValidAirportCode(value);
				_departureAirportCode = value;
			}
		}

		public string ArrivalAirportCode
		{
			get => _arrivalAirportCode;
			set
			{
				EnsureValidAirportCode(value);
				_arrivalAirportCode = value;
			}
		}

		public TimeSpan FlightDuration { get; set; }
		public string AirlineName { get; set; }
		public IList<string> MealOptions { get; set; }

		private static void EnsureValidAirportCode(string airportCode)
		{
			var isWrongLength = airportCode.Length != 3;

			var isWrongCase =
				airportCode != airportCode.ToUpperInvariant();

			if (isWrongLength || isWrongCase)
				throw new ApplicationException(
					airportCode + " is an invalid airport");
		}
	}
}