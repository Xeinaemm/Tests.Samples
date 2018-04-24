using System;
using xUnit.Samples.xUnit;
using xUnit.Tests.Helpers;
using Xunit;

namespace xUnit.Tests.xUnit
{
	[Trait("Category", "Enemy")]
	public class EnemyFactoryShould
	{
		[Theory]
		[AutoMoqData]
		public void CreateBossEnemy(EnemyFactory sut)
		{
			Assert.IsType<BossEnemy>(sut.Create("Zombie King", true));
		}

		[Theory]
		[AutoMoqData]
		public void CreateBossEnemy_AssertAssignableTypes(EnemyFactory sut)
		{
			Assert.IsAssignableFrom<Enemy>(sut.Create("Zombie King", true));
		}

		[Theory]
		[AutoMoqData]
		public void CreateBossEnemy_CastReturnedTypeExample(EnemyFactory sut)
		{
			var enemy = sut.Create("Zombie King", true);
			var boss = Assert.IsType<BossEnemy>(enemy);
			Assert.Equal("Zombie King", boss.Name);
		}

		[Theory]
		[AutoMoqData]
		public void CreateNormalEnemyByDefault(EnemyFactory sut)
		{
			Assert.IsType<NormalEnemy>(sut.Create("Zombie"));
		}

		[Theory]
		[AutoMoqData]
		public void CreateSeparateInstances(EnemyFactory sut)
		{
			var enemy1 = sut.Create("Zombie");
			var enemy2 = sut.Create("Zombie");

			Assert.NotSame(enemy1, enemy2);
		}

		[Theory]
		[AutoMoqData]
		public void NotAllowNullName(EnemyFactory sut)
		{
			Assert.Throws<ArgumentNullException>("name", () => sut.Create(null));
		}

		[Theory]
		[AutoMoqData]
		public void OnlyAllowKingOrQueenBossEnemies(EnemyFactory sut)
		{
			var ex =
				Assert.Throws<EnemyCreationException>(() => sut.Create("Zombie", true));

			Assert.Equal("Zombie", ex.RequestedEnemyName);
		}
	}
}