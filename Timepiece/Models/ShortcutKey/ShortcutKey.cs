using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace twinkfrag.Timepiece.Models.ShortcutKey
{
	public struct ShortcutKey
	{
		public Key Key { get; set; }
		public ICollection<Key> Modifiers { get; set; }

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
	}
}
