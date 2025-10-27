using Xunit;

namespace RPCApp0.Tests
{
	public class LevenshteinTests
	{
		[Theory]
		[InlineData("", "", 0)]
		[InlineData("", "abc", 3)]
		[InlineData("abc", "", 3)]
		[InlineData("a", "a", 0)]
		[InlineData("a", "b", 1)]
		[InlineData("kitten", "sitting", 3)]
		[InlineData("flaw", "lawn", 2)]
		[InlineData("intention", "execution", 5)]
		[InlineData("Saturday", "Sunday", 3)]
		[InlineData("1234", "1224533324", 6)]
		public void Distance_ReturnsExpected(string s1, string s2, int expected)
		{
			int actual = Program.LevenshteinDistance2(s1, s2);
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(null, "a")]
		[InlineData("a", null)]
		public void Distance_ThrowsOnNull(string s1, string s2)
		{
			Assert.Throws<System.ArgumentNullException>(() => Program.LevenshteinDistance2(s1, s2));
		}
	}
}

