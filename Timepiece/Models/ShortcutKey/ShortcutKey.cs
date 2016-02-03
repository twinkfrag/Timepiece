using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using twinkfrag.Timepiece.Utils;

namespace twinkfrag.Timepiece.Models.ShortcutKey
{
	public struct ShortcutKey : IEquatable<ShortcutKey>
	{
		public Key Key { get; }
		public ICollection<Key> Modifiers { get; }

		public ShortcutKey(Key key, params Key[] modifiers) : this()
		{
			this.Key = key;
			this.Modifiers = modifiers;
		}

		public ShortcutKey(Key key, ICollection<Key> modifiers) : this()
		{
			this.Key = key;
			this.Modifiers = modifiers;
		}

		public override string ToString()
		{
			return $"{Modifiers.JoinToString(", ")} + {Key}";
		}

		public bool Equals(ShortcutKey other)
		{
			if (this.Key != other.Key) return false;

			var mod1 = this.Modifiers as ISet<Key> ?? new HashSet<Key>(this.Modifiers);
			var mod2 = other.Modifiers as ISet<Key> ?? new HashSet<Key>(other.Modifiers);

			return mod1.Count == mod2.Count && !mod1.Except(mod2).Any();
		}

		public override bool Equals(object obj)
		{
			return (obj as ShortcutKey?)?.Equals(this) ?? false;
		}

		public override int GetHashCode()
		{
			return unchecked (((int)this.Key * 397) ^ (this.Modifiers?.GetHashCode() ?? 0));
		}

		public static bool operator ==(ShortcutKey a, ShortcutKey b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(ShortcutKey a, ShortcutKey b)
		{
			return !a.Equals(b);
		}
	}
}
