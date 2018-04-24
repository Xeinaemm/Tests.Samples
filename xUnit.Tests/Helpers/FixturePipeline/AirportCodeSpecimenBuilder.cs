using System;
using System.Reflection;
using AutoFixture.Kernel;

namespace xUnit.Tests.Helpers.FixturePipeline
{
	public class AirportCodeSpecimenBuilder : ISpecimenBuilder
	{
		public object Create(object request, ISpecimenContext context)
		{
			var propertyInfo = request as PropertyInfo;

			if (propertyInfo == null) return new NoSpecimen();

			var isAirportCodeProperty =
				propertyInfo.Name.Contains("AirportCode") &&
				propertyInfo.PropertyType == typeof(string);
			if (isAirportCodeProperty) return RandomAirportCode();
			return new NoSpecimen();
		}

		private static string RandomAirportCode() => DateTime.Now.Ticks % 2 == 0 ? "AAA" : "BBB";
	}
}