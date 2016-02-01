using System.Collections.Generic;

namespace twinkfrag.Timepiece.Utils
{
	public static class EnumerableEx
	{
		public static string JoinToString<T>(this IEnumerable<T> values, string separator)
			=> string.Join(separator, values);

		public static string JoinToString(this IEnumerable<string> values, string separator)
			=> string.Join(separator, values);
	}
}