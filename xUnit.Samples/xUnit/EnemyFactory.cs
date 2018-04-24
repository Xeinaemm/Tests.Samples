using System;

namespace xUnit.Samples.xUnit
{
	public class EnemyFactory
	{
		public Enemy Create(string name, bool isBoss = false)
		{
			if (name is null) throw new ArgumentNullException(nameof(name));

			if (!isBoss) return new NormalEnemy {Name = name};
			if (!IsValidBossName(name))
				throw new EnemyCreationException(
					$"{name} is not a valid name for a Boss enemy, Boss enemy names must end with 'King' or 'Queen'",
					name);

			return new BossEnemy {Name = name};
		}

		private static bool IsValidBossName(string name) => name.EndsWith("King") ||
															name.EndsWith("Queen");
	}
}