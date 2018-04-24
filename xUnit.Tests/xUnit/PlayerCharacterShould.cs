using System;
using xUnit.Samples.xUnit;
using xUnit.Tests.Helpers;
using Xunit;

namespace xUnit.Tests.xUnit
{
	public class PlayerCharacterShould
	{
		[Theory]
		[AutoMoqData]
		public void BeInexperiencedWhenNew(PlayerCharacter sut)
		{
			Assert.True(sut.IsNoob);
		}

		[Theory]
		[AutoMoqData]
		public void CalculateFullName(PlayerCharacter sut)
		{
			Assert.Equal($"{sut.FirstName} {sut.LastName}", sut.FullName);
		}

		[Theory]
		[HealthDamageData]
		public void TakeDamage(int damage, int expectedHealth)
		{
			var sut = new PlayerCharacter();
			sut.TakeDamage(damage);

			Assert.Equal(expectedHealth, sut.Health);
		}

		[Fact]
		public void CalculateFullNameWithTitleCase()
		{
			var sut = new PlayerCharacter
			{
				FirstName = "Sarah",
				LastName = "Smith"
			};

			Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", sut.FullName);
		}

		[Fact]
		public void HaveAllExpectedWeapons()
		{
			var sut = new PlayerCharacter();

			var expectedWeapons = new[]
			{
				"Long Bow",
				"Short Bow",
				"Short Sword"
			};

			Assert.Equal(expectedWeapons, sut.Weapons);
		}

		[Fact]
		public void HaveALongBow()
		{
			var sut = new PlayerCharacter();

			Assert.Contains("Long Bow", sut.Weapons);
		}

		[Fact]
		public void HaveAtLeastOneKindOfSword()
		{
			var sut = new PlayerCharacter();

			Assert.Contains(sut.Weapons, x => x.Contains("Sword"));
		}

		[Fact]
		public void HaveFullNameEndingWithLastName()
		{
			var sut = new PlayerCharacter {LastName = "Smith"};

			Assert.EndsWith("Smith", sut.FullName);
		}

		[Fact]
		public void HaveFullNameStartingWithFirstName()
		{
			var sut = new PlayerCharacter
			{
				FirstName = "Sarah",
				LastName = "Smith"
			};

			Assert.StartsWith("Sarah", sut.FullName);
		}

		[Fact]
		public void HaveNoEmptyDefaultWeapons()
		{
			var sut = new PlayerCharacter();

			Assert.All(sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
		}

		[Fact]
		public void IncreaseHealthAfterSleeping()
		{
			var sut = new PlayerCharacter();

			sut.Sleep();

			Assert.InRange(sut.Health, 101, 200);
		}

		[Fact]
		public void NotHaveAStaffOfWonder()
		{
			var sut = new PlayerCharacter();

			Assert.DoesNotContain("Staff Of Wonder", sut.Weapons);
		}

		[Fact]
		public void NotHaveNickNameByDefault()
		{
			var sut = new PlayerCharacter();

			Assert.Null(sut.Nickname);
		}

		[Fact]
		public void RaisePropertyChangedEvent()
		{
			var sut = new PlayerCharacter();

			Assert.PropertyChanged(sut, "Health", () => sut.TakeDamage(10));
		}

		[Fact]
		public void RaiseSleptEevnt()
		{
			var sut = new PlayerCharacter();

			Assert.Raises<EventArgs>(
				attach => sut.PlayerSlept += attach,
				detach => sut.PlayerSlept -= detach,
				() => sut.Sleep());
		}

		[Fact]
		public void StartWithDefaultHealth()
		{
			var sut = new PlayerCharacter();

			Assert.Equal(100, sut.Health);
		}

		[Fact]
		public void StartWithDefaultHealth_NotEqualExample()
		{
			var sut = new PlayerCharacter();

			Assert.NotEqual(0, sut.Health);
		}
	}
}