using BookGenerator.Core.RuntimeLibrary;
using System.IO;

namespace BookGenerator.Library
{
    [RuntimeType("binary")]
    public class BinaryMethods
    {
        [RuntimeMethod("make-reader")]
        public static object MakeReader(byte[] raw)
        {
            return new BinaryReader(new MemoryStream(raw));
        }

        [RuntimeMethod("read-string")]
        public object ReadString(BinaryReader reader)
        {
            return reader.ReadString();
        }

        [RuntimeMethod("read32")]
        public object Read32(BinaryReader reader)
        {
            return reader.ReadInt32();
        }

        [RuntimeMethod("read16")]
        public object Read16(BinaryReader reader)
        {
            return reader.ReadInt16();
        }

        [RuntimeMethod("read8")]
        public object Read8(BinaryReader reader)
        {
            return reader.ReadByte();
        }
    }
}