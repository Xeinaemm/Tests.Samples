namespace xUnit.Samples.xUnit
{
	public class BossEnemy : Enemy
	{
		protected override double TotalSpecialPower => 1000;
		protected override double SpecialPowerUses => 6;
	}
}