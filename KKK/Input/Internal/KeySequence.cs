using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KKK.Util;
using KKK.Extension;

namespace KKK.Input
{
    internal class KeySequence : SequenceBase<Keys>
    {
        public int Count
        {
            get { return Length; }
        }

        public KeySequence(IEnumerable<Keys> keys)
        {
            m_elements = keys.Select(k => k.Normalize()).OrderBy(k => k).ToArray();
        }

        public static KeySequence FromString(string chord)
        {
            var parts = chord
                .Split('+')
                .Select(p => Enum.Parse(typeof(Keys), p)) // "A" -> Keys.A
                .Cast<Keys>();

            Stack<Keys> stack = new Stack<Keys>(parts);

            return new KeySequence(stack);
        }

        public override string ToString()
        {
            // Control+A+B
            return string.Join("+", m_elements);
        }
    }
}
