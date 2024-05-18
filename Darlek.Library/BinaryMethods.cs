using Darlek.Core.RuntimeLibrary;
using System.IO;

namespace Darlek.Library;

[RuntimeType("binary")]
public class BinaryMethods
{
    [RuntimeMethod("make-reader")]
    public static BinaryReader MakeReader(byte[] raw)
    {
        return new BinaryReader(new MemoryStream(raw));
    }

    [RuntimeMethod("read-string")]
    public string ReadString(BinaryReader reader)
    {
        return reader.ReadString();
    }

    [RuntimeMethod("read32")]
    public int Read32(BinaryReader reader)
    {
        return reader.ReadInt32();
    }

    [RuntimeMethod("read64")]
    public static long Read64(BinaryReader reader) {
        return reader.ReadInt64();
    }

    [RuntimeMethod("read16")]
    public short Read16(BinaryReader reader)
    {
        return reader.ReadInt16();
    }

    [RuntimeMethod("read8")]
    public byte Read8(BinaryReader reader)
    {
        return reader.ReadByte();
    }
}