namespace xUnit.Samples.xUnit
{
	public abstract class Enemy
	{
		public string Name { get; set; }
		protected abstract double TotalSpecialPower { get; }
		protected abstract double SpecialPowerUses { get; }
		public double SpecialAttackPower => TotalSpecialPower / SpecialPowerUses;
	}
}