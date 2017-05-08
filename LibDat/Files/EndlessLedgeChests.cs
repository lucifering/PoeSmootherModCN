using System;
using System.IO;

namespace LibDat.Files
{
	public class EndlessLedgeChests : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public Int64 U01 { get; set; }
        [Hidden]
        public int ItemListLength { get; set; }
        [Hidden]
        public int ItemListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List ItemListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> ItemListRef { get; set; }
        [Hidden]
        public int SocketsStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString SocketsStringData { get; set; }

		public EndlessLedgeChests()
		{
			
		}
		public EndlessLedgeChests(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			U01 = inStream.ReadInt64();
			ItemListLength = inStream.ReadInt32();
			ItemListOffset = inStream.ReadInt32();
            SocketsStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(U01);
			outStream.Write(ItemListLength);
			outStream.Write(ItemListOffset);
            outStream.Write(SocketsStringOffset);
		}

		public override int GetSize()
		{
			return 0x18;
		}
	}
}