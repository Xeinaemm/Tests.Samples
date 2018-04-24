namespace xUnit.Samples.AutoFixture
{
	public class Calculator
	{
		public int Value { get; set; }

		public void Add(int a, int b)
		{
			Value = a + b;
		}
	}
}