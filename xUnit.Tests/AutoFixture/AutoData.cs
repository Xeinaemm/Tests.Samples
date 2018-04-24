using AutoFixture.Xunit2;
using xUnit.Samples.AutoFixture;
using Xunit;

namespace xUnit.Tests.AutoFixture
{
	public class AutoData
	{
		[Theory]
		[AutoData]
		public void ShouldAdd_AutoData(int a, int b, Calculator sut)
		{
			sut.Add(a, b);
			Assert.Equal(a + b, sut.Value);
		}

		[Theory]
		[InlineAutoData] // AddTwoPositiveNumbers
		[InlineAutoData(0)] // AddZeroAndPositiveNumber
		[InlineAutoData(-5)] // AddNegativeAndPositiveNumber
		public void ShouldAdd_InlineAutoData(int a, int b, Calculator sut)
		{
			sut.Add(a, b);
			Assert.Equal(a + b, sut.Value);
		}
	}
}