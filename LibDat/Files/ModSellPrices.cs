using System;
using System.IO;

namespace LibDat.Files
{
	public class ModSellPrices : BaseDat
	{
        [Hidden]
        public Int64 ModKey { get; set; }
        [ExternalReference]
        public Mods ModRef { get; set; }
        [Hidden]
        public int U01ListLength { get; set; }
        [Hidden]
        public int U01ListOffset { get; set; }
        [ResourceOnly]
        public UInt64List U01ListData { get; set; }
        [Hidden]
        public int U03ListLength { get; set; }
        [Hidden]
        public int U03ListOffset { get; set; }
        [ResourceOnly]
        public UInt64List U03ListData { get; set; }

		public ModSellPrices(BinaryReader inStream)
		{
			ModKey = inStream.ReadInt64();
			U01ListLength = inStream.ReadInt32();
			U01ListOffset = inStream.ReadInt32();
			U03ListLength = inStream.ReadInt32();
			U03ListOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(ModKey);
			outStream.Write(U01ListLength);
			outStream.Write(U01ListOffset);
			outStream.Write(U03ListLength);
			outStream.Write(U03ListOffset);
		}

		public override int GetSize()
		{
			return 0x18;
		}
	}
}