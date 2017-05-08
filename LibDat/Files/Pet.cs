using System;
using System.IO;

namespace LibDat.Files
{
	public class Pet : BaseDat
	{
        [Hidden]
        public int MetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MetadataStringData { get; set; }
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        public int HexU03 { get; set; }

		public Pet(BinaryReader inStream)
		{
            MetadataStringOffset = inStream.ReadInt32();
			ItemKey = inStream.ReadInt64();
            HexU03 = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(MetadataStringOffset);
			outStream.Write(ItemKey);
            outStream.Write(HexU03);
		}

		public override int GetSize()
		{
			return 0x10;
		}
	}
}