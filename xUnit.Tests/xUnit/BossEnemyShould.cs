using xUnit.Samples.xUnit;
using xUnit.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace xUnit.Tests.xUnit
{
	public class BossEnemyShould
	{
		private readonly ITestOutputHelper _output;
		public BossEnemyShould(ITestOutputHelper output) => _output = output;

		[Theory]
		[AutoMoqData]
		public void HaveCorrectPower(BossEnemy sut)
		{
			_output.WriteLine("Creating Boss Enemy");
			Assert.Equal(166.667, sut.SpecialAttackPower, 3);
		}
	}
}