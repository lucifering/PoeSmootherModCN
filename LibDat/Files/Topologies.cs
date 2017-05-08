using System;
using System.IO;

namespace LibDat.Files
{
	public class Topologies : BaseDat
	{
		[Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
		public int GraphStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString GraphStringData { get; set; }
        public int U02 { get; set; }
		public int U03 { get; set; }
		public int U04 { get; set; }
        [Hidden]
        public Int64 EnvironmentKey { get; set; }
        [ExternalReference]
        public Environments EnvironmentRef { get; set; }

		public Topologies(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			GraphStringOffset = inStream.ReadInt32();
			U02 = inStream.ReadInt32();
			U03 = inStream.ReadInt32();
			U04 = inStream.ReadInt32();
			EnvironmentKey = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(GraphStringOffset);
			outStream.Write(U02);
			outStream.Write(U03);
			outStream.Write(U04);
			outStream.Write(EnvironmentKey);
		}

		public override int GetSize()
		{
			return 0x1C;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}