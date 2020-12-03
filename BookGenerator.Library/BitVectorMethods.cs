using BookGenerator.Core.RuntimeLibrary;
using Furesoft.Core;
using Schemy;

namespace BookGenerator.Library
{
    [RuntimeType("bitvector")]
    public static class BitVectorMethods
    {
        [RuntimeMethod("bitvector-set")]
        public static object Set(BitSet set, int index)
        {
            set.Set(index);

            return None.Instance;
        }

        [RuntimeMethod("bitvector-unset")]
        public static object Unset(BitSet set, int index)
        {
            set.Unset(index);

            return None.Instance;
        }

        [RuntimeMethod("bitvector-set?")]
        public static bool IsSet(BitSet set, int index)
        {
            return set.IsSet(index);
        }
    }
}